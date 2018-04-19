//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDev.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
#endregion

namespace MoonAntonio.MDev
{
	public class MDev : MonoBehaviour 
	{
		#region Variables Publicas
		public static MDev instance;
		[SerializeField] public MDevConfig data;
		public MDevMetodos mdevMetodos;
		private MDevInput mdevInput;
		private MDevGUI mdevGUI;
		public TouchScreenKeyboard touchScreenKeyboard;
		#endregion

		#region Propiedades
		public bool MostrarMDev { get; private set; }
		public string InputText { get; private set; }
		public string Historial { get; private set; }
		public List<string> AutoCompletar { get; private set; }
		public int AutoCompletarIndex { get; private set; }
		public string MDevLinea { get { return (data.nombreMDev + data.direccion + data.marcador + " "); } }
		#endregion

		#region Inicializadores
		private void Awake()
		{
			instance = this;
			if (data == null) data = Resources.Load<MDevConfig>("Data/Moon");
			if (data.mobileTouchCount <= 0) data.mobileTouchCount = 4;
			AutoCompletarIndex = 0;
			AutoCompletar = new List<string>();
			mdevMetodos = new MDevMetodos();
			mdevInput = new MDevInput(this);
			mdevGUI = new MDevGUI(this);
		}
		#endregion

		#region Actualizadores
		void Update()
		{
			mdevInput.Update();
		}

		private IEnumerator ClearMDevCoroutine()
		{
			yield return new WaitForEndOfFrame();
			Historial = "";
		}
		#endregion

		#region GUI
		void OnGUI()
		{
			if (!MostrarMDev) return;
			mdevGUI.OnGUI();
		}
		#endregion

		#region Funcionalidad
		private string EjecutarComando(string input)
		{
			AutoCompletar.Clear();
			bool registrado = false;
			string resultado = null;
			string parentesisValido = Regex.Match(input, @"\(([^)]*)\)").Groups[1].Value;
			List<string> args = new List<string>();
			string comando;


			if (!string.IsNullOrEmpty(parentesisValido))
			{
				args = parentesisValido.Split(new char[] { ',' }).ToList();
				comando = input.Replace(parentesisValido, "").Replace("(", "").Replace(")", "").Replace(";", "");
			}
			else comando = input.Replace("(", "").Replace(")", "").Replace(";", "");

			foreach (var met in mdevMetodos.Metodos)
			{
				foreach (object atri in met.GetCustomAttributes(true)) // Returns all 3 of my attributes.
					if (atri is MDevAttribute)
					{
						MDevAttribute com = (MDevAttribute)atri;
						if (com.nombreComando == comando)
						{
							if (registrado) Debug.LogError("Multiples comandos se definen con: " + comando);

							Type type = (met.DeclaringType);
							ParameterInfo[] parametros = met.GetParameters();
							List<object> argList = new List<object>();

							// Cast Arguments if there is any
							if (args.Count != 0)
							{
								if (parametros.Length != args.Count)
								{
									resultado = string.Format("El comando {0} necesita {1} parametros, solo paso {2}", comando, parametros.Length, args.Count);
									Debug.Log(resultado);
									return resultado;
								}
								else
								{
									// Cast string arguments to input objects types
									for (int i = 0; i < parametros.Length; i++)
									{
										try
										{
											var a = Convert.ChangeType(args[i], parametros[i].ParameterType);
											argList.Add(a);
										}
										catch
										{
											resultado = string.Format("No se pudo convertir {0} al tipo {1}", args[i], parametros[i].ParameterType);
											Debug.LogError(resultado);
											return resultado;
										}
									}
								}
							}
							if (type.IsSubclassOf(typeof(UnityEngine.Object)))
							{
								var instance_classes = GameObject.FindObjectsOfType(type);
								if (instance_classes != null)
								{
									foreach (var instance_class in instance_classes)
									{
										resultado = (string)met.Invoke(instance_class, argList.ToArray());
									}
								}
							}
							else
							{
								var instance_class = Activator.CreateInstance(type);
								resultado = (string)met.Invoke(instance_class, argList.ToArray());
							}
							registrado = true;
							break;
						}
					}
			}

			if (!string.IsNullOrEmpty(resultado)) return resultado;

			if (registrado) return null;

			return "Comando no encontrado! - Escribe \"help\" para la lista de comandos disponibles!";
		}
		#endregion

		#region Metodos Publicos
		public void PreEjecutar()
		{
			string resultado = EjecutarComando(InputText);
			Historial += MDevLinea + InputText + "\n" + (!string.IsNullOrEmpty(resultado) ? (resultado + "\n") : "");
			InputText = "";
		}

		public void MostrarTouchScreenKeyboard()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if (MostrarMDev)
				{
					touchScreenKeyboard = TouchScreenKeyboard.Open(InputText, TouchScreenKeyboardType.Default);
					TouchScreenKeyboard.hideInput = true;
				}
			}
		}

		/// <summary>
		/// <para>Para teclados de PC.</para>
		/// </summary>
		/// <param name="input"></param>
		public void UpdateInputText(string input)
		{
			InputText += input;
			InputText = InputText.Replace("\b", "");
		}
		#endregion

		#region Metodos Internos
		internal void ToggleMDev()
		{
			MostrarMDev = !MostrarMDev;
			MostrarTouchScreenKeyboard();
		}

		internal void CambiarInput(string input)
		{
			InputText = input;
		}

		/// <summary>
		/// <para>Para teclados de android.</para>
		/// </summary>
		/// <param name="inputString"></param>
		internal void SetInputText(string input)
		{
			InputText = input;
		}
		#endregion

		#region Eventos
		internal void OnBackSpacePresionado()
		{
			if (InputText.Length >= 1) InputText = InputText.Substring(0, InputText.Length - 1);
		}

		internal void OnEnterPresionado()
		{
			if (AutoCompletar.Count > 0)
			{
				InputText = AutoCompletar[AutoCompletarIndex];
				AutoCompletar.Clear();
			}
			else
			{
				PreEjecutar();
			}		
		}

		internal void OnTabPresionado()
		{
			if (AutoCompletar.Count != 0) { OnEnterPresionado(); return; }
			AutoCompletarIndex = 0;
			AutoCompletar.Clear();
			AutoCompletar.AddRange(mdevMetodos.GetComandos(InputText));
		}

		internal void OnUpArrowPresionado()
		{
			if (AutoCompletar.Count > 0) AutoCompletarIndex = (int)Mathf.Repeat(AutoCompletarIndex - 1, AutoCompletar.Count);
		}

		internal void OnDownArrowPresionado()
		{
			if (AutoCompletar.Count > 0) AutoCompletarIndex = (int)Mathf.Repeat(AutoCompletarIndex + 1, AutoCompletar.Count);
		}
		#endregion

		#region Comandos Basicos
		[MDev("help", "Shows list of available commands")]
		public string Help()
		{
			string help_string = "List of available commands:";
			foreach (var method in MDev.instance.mdevMetodos.Metodos)
			{
				foreach (var attribute in method.GetCustomAttributes(true))
				{
					if (attribute is MDevAttribute)
					{
						MDevAttribute attr = (MDevAttribute)attribute;
						help_string += "\n      " + attr.nombreComando + " --> " + attr.descripcionComando;
					}
				}
			}
			return help_string;
		}

		[MDev("hide", "Hides the terminal")]
		public void Hide()
		{
			MostrarMDev = false;
		}

		[MDev("clear", "clears the terminal screen")]
		public void Clear()
		{
			StartCoroutine(ClearMDevCoroutine());
		}
		#endregion
	}
}
