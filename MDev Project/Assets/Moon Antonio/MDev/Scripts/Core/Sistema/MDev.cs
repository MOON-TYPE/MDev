//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDev.cs (19/04/2018)															\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Sistema central de la consola.								\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
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
	/// <summary>
	/// <para>Sistema central de la consola.</para>
	/// </summary>
	public class MDev : MonoBehaviour 
	{
		#region Variables
		/// <summary>
		/// <para>Singleton de <see cref="MDev"/>.</para>
		/// </summary>
		public static MDev instance;									// Singleton de MDev
		/// <summary>
		/// <para>Configuracion de <see cref="MDev"/>.</para>
		/// </summary>
		[SerializeField] public MDevConfig data;						// Configuracion de MDev
		/// <summary>
		/// <para>Metodos de <see cref="MDev"/>.</para>
		/// </summary>
		public MDevMetodos mdevMetodos;									// Metodos de MDev
		/// <summary>
		/// <para>Inputs de <see cref="MDev"/>.</para>
		/// </summary>
		private MDevInput mdevInput;									// Inputs de MDev
		/// <summary>
		/// <para>Interfaz de <see cref="MDev"/>.</para>
		/// </summary>
		private MDevGUI mdevGUI;										// Interfaz de MDev
		/// <summary>
		/// <para>Teclado de mobile.</para>
		/// </summary>
		public TouchScreenKeyboard touchScreenKeyboard;					// Teclado de mobile
		#endregion

		#region Propiedades
		/// <summary>
		/// <para>Muestra/Oculta <see cref="MDev"/>.</para>
		/// </summary>
		public bool MostrarMDev { get; private set; }
		/// <summary>
		/// <para>Texto.</para>
		/// </summary>
		public string InputText { get; private set; }
		/// <summary>
		/// <para>Historial.</para>
		/// </summary>
		public string Historial { get; private set; }
		/// <summary>
		/// <para>Posibles autocompletar.</para>
		/// </summary>
		public List<string> AutoCompletar { get; private set; }
		/// <summary>
		/// <para>Index del autocompletar.</para>
		/// </summary>
		public int AutoCompletarIndex { get; private set; }
		/// <summary>
		/// <para>Linea de <see cref="MDev"/>.</para>
		/// </summary>
		public string MDevLinea { get { return (data.nombreMDev + data.direccion + data.marcador + " "); } }
		#endregion

		#region Inicializadores
		/// <summary>
		/// <para>Inicializador de <see cref="MDev"/>.</para>
		/// </summary>
		private void Awake()// Inicializador de MDev
		{
			// Obtener la instancia
			instance = this;

			// Cargar y inicializar las variables
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
		/// <summary>
		/// <para>Actualizador de <see cref="MDev"/>.</para>
		/// </summary>
		private void Update()// Actualizador de MDev
		{
			// Actualiza el control de inputs
			mdevInput.Update();
		}

		/// <summary>
		/// <para>Limpia el historial.</para>
		/// </summary>
		/// <returns></returns>
		private IEnumerator ClearMDevCoroutine()// Limpia el historial
		{
			yield return new WaitForEndOfFrame();
			Historial = "";
		}
		#endregion

		#region GUI
		/// <summary>
		/// <para>Interfaz de <see cref="MDev"/>.</para>
		/// </summary>
		private void OnGUI()// Interfaz de MDev
		{
			// Si no se muestra la GUI volver, sino mostrar
			if (!MostrarMDev) return;
			mdevGUI.OnGUI();
		}
		#endregion

		#region Funcionalidad
		/// <summary>
		/// <para>Ejecuta un comando.</para>
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		private string EjecutarComando(string input)// Ejecuta un comando
		{
			// Preparar la ejecucion
			AutoCompletar.Clear();
			bool registrado = false;
			string resultado = null;
			string parentesisValido = Regex.Match(input, @"\(([^)]*)\)").Groups[1].Value;
			List<string> args = new List<string>();
			string comando;

			// Reemplazar con el limitador
			if (!string.IsNullOrEmpty(parentesisValido))
			{
				args = parentesisValido.Split(new char[] { ',' }).ToList();
				comando = input.Replace(parentesisValido, "").Replace("(", "").Replace(")", "").Replace(";", "");
			}
			else comando = input.Replace("(", "").Replace(")", "").Replace(";", "");

			foreach (var met in mdevMetodos.Metodos)
			{
				// Devuelve los 3 atributos.
				foreach (object atri in met.GetCustomAttributes(true))
				{
					if (atri is MDevAttribute)
					{
						MDevAttribute com = (MDevAttribute)atri;
						if (com.nombreComando == comando)
						{
							if (registrado) Debug.LogError("Multiples comandos se definen con: " + comando);

							Type type = (met.DeclaringType);
							ParameterInfo[] parametros = met.GetParameters();
							List<object> argList = new List<object>();

							// Argumentos de lanzamiento si hay alguno
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
									// Lanzar argumentos de string para ingresar tipos de objetos
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
			}

			// Devolver resultado
			if (!string.IsNullOrEmpty(resultado)) return resultado;

			// Volver si esta registrado
			if (registrado) return null;

			// Volver un error
			return "Comando no encontrado! - Escribe \"help\" para la lista de comandos disponibles!";
		}
		#endregion

		#region Metodos Publicos
		/// <summary>
		/// <para>Prepara la ejecucion del comando.</para>
		/// </summary>
		public void PreEjecutar()// Prepara la ejecucion del comando
		{
			string resultado = EjecutarComando(InputText);
			Historial += MDevLinea + InputText + "\n" + (!string.IsNullOrEmpty(resultado) ? (resultado + "\n") : "");
			InputText = "";
		}

		/// <summary>
		/// <para>Muestra el teclado del mobile.</para>
		/// </summary>
		public void MostrarTouchScreenKeyboard()// Muestra el teclado del mobile
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
		/// <para>Actualiza el input text (Para teclados de PC).</para>
		/// </summary>
		/// <param name="input"></param>
		public void UpdateInputText(string input)// Actualiza el input text (Para teclados de PC)
		{
			InputText += input;
			InputText = InputText.Replace("\b", "");
		}
		#endregion

		#region Metodos Internos
		/// <summary>
		/// <para>Muestra/Oculta <see cref="MDev"/>.</para>
		/// </summary>
		internal void ToggleMDev()// Muestra/Oculta MDev
		{
			MostrarMDev = !MostrarMDev;
			MostrarTouchScreenKeyboard();
		}

		/// <summary>
		/// <para>Cambia el input actual.</para>
		/// </summary>
		/// <param name="input"></param>
		internal void CambiarInput(string input)// Cambia el input actual
		{
			InputText = input;
		}

		/// <summary>
		/// <para>Establece el input text (Para teclados de android).</para>
		/// </summary>
		/// <param name="inputString"></param>
		internal void SetInputText(string input)// Establece el input text (Para teclados de android)
		{
			InputText = input;
		}
		#endregion

		#region Eventos
		/// <summary>
		/// <para>Evento activo cuando se presiona la tecla return/Borrar/Delete.</para>
		/// </summary>
		internal void OnBackSpacePresionado()// Evento activo cuando se presiona la tecla return/Borrar/Delete
		{
			if (InputText.Length >= 1) InputText = InputText.Substring(0, InputText.Length - 1);
		}

		/// <summary>
		/// <para>Evento activo cuando se presiona la tecla Enter.</para>
		/// </summary>
		internal void OnEnterPresionado()// Evento activo cuando se presiona la tecla Enter
		{
			// Comprobar el autocompletado sino comprobar la ejecucion del comando
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

		/// <summary>
		/// <para>Evento activo cuando se presiona el tabulador.</para>
		/// </summary>
		internal void OnTabPresionado()// Evento activo cuando se presiona el tabulador
		{
			// Nodo autocompletado
			if (AutoCompletar.Count != 0) { OnEnterPresionado(); return; }
			AutoCompletarIndex = 0;
			AutoCompletar.Clear();
			AutoCompletar.AddRange(mdevMetodos.GetComandos(InputText));
		}

		/// <summary>
		/// <para>Evento activo cuando se presiona la tecla flecha arriba.</para>
		/// </summary>
		internal void OnUpArrowPresionado()// Evento activo cuando se presiona la tecla flecha arriba
		{
			if (AutoCompletar.Count > 0) AutoCompletarIndex = (int)Mathf.Repeat(AutoCompletarIndex - 1, AutoCompletar.Count);
		}

		/// <summary>
		/// <para>Evento activo cuando se presiona la tecla flecha abajo.</para>
		/// </summary>
		internal void OnDownArrowPresionado()// Evento activo cuando se presiona la tecla flecha abajo
		{
			if (AutoCompletar.Count > 0) AutoCompletarIndex = (int)Mathf.Repeat(AutoCompletarIndex + 1, AutoCompletar.Count);
		}
		#endregion

		#region Comandos Basicos
		[MDev("help", "Muestra todos los comandos disponibles.")]
		public string Help()
		{
			string help_string = "Lista de comandos disponibles:";
			foreach (var met in MDev.instance.mdevMetodos.Metodos)
			{
				foreach (var atri in met.GetCustomAttributes(true))
				{
					if (atri is MDevAttribute)
					{
						MDevAttribute com = (MDevAttribute)atri;
						help_string += "\n      " + com.nombreComando + " --> " + com.descripcionComando;
					}
				}
			}
			return help_string;
		}

		[MDev("hide", "Oculta MDev.")]
		public void Hide()
		{
			MostrarMDev = false;
		}

		[MDev("clear", "Limpia el historial.")]
		public void Clear()
		{
			StartCoroutine(ClearMDevCoroutine());
		}
		#endregion
	}
}
