//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevBehaviour.cs (10/05/2017)												\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Core del sistema de MDev									\\
// Fecha Mod:		10/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Core del sistema de MDev.</para>
	/// </summary>
	[AddComponentMenu("Moon Antonio/MDev/MDevBehaviour")]
	public class MDevBehaviour : MonoBehaviour 
	{
		#region Variables Publicas
		public UITextList textList;
		public UIInput mInput;
		public GameObject mando;
		public UILabel textAutocompletado;
		#endregion

		#region Variables Estaticas
		public static bool devDebug = true;
		public static bool devMando = true;
		#endregion

		// test
		public List<string> comandos = new List<string>();
		string texto = "";

		#region Inicializadores
		private void Start()
		{
			mInput.label.maxLineCount = 1;
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			Intellisent();

			if (devDebug == true)
			{
				// TODO Callback del debug de unity
			}
			else
			{

			}

			if (devMando == true)
			{
				mando.SetActive(true);
			}
			else
			{
				mando.SetActive(false);
			}
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Envia la notificacion cuando se presiona enter.</para>
		/// </summary>
		public void OnSubmit()
		{
			if (textList != null)
			{
				// Es una buena idea eliminar todos los símbolos, ya que no queremos que la entrada del usuario altere los colores, agregue nuevas lineas, etc.
				string text = NGUIText.StripSymbols(mInput.value);

				if (!string.IsNullOrEmpty(text))
				{
					ComprobarComando(text);
					textAutocompletado.text = "";
				}
			}
		}

		public void ComprobarComando(string value)
		{
			switch (value)
			{
				#region Mist
				case ".help":
					textList.Add("> .help -> Muestra la lista de comandos.");
					textList.Add("> .devDebug.enable/disable -> Activa/Desactiva el debug.");
					textList.Add("> .devMando.enable/disable -> Activa/Desactiva el mando de referenciacion.");
					textList.Add("> .devNotifi.enable/disable -> Activa/Desactiva las notificaciones visuales.");
					mInput.value = "";
					mInput.isSelected = false;
					break;

				case "exit()":
					Application.Quit();
					mInput.value = "";
					mInput.isSelected = false;
					break;
				#endregion

				#region devDebug

				case ".devDebug.enable":
					devDebug = true;
					textList.Add("> El debug de la consola a sido activado.");
					mInput.value = "";
					mInput.isSelected = false;
					break;

				case ".devDebug.disable":
					devDebug = false;
					textList.Add("> El debug de la consola a sido desactivado.");
					mInput.value = "";
					mInput.isSelected = false;
					break;
				#endregion

				#region devMando
				case ".devMando.enable":
					devMando = true;
					textList.Add("> El visualizador del input esta activado.");
					mInput.value = "";
					mInput.isSelected = false;
					break;

				case ".devMando.disable":
					devMando = false;
					textList.Add("> El visualizador del input esta desactivado.");
					mInput.value = "";
					mInput.isSelected = false;
					break;
				#endregion
			}

		}

		public void Intellisent()
		{
			string oldString = texto;
			texto = mInput.value;

			if (!string.IsNullOrEmpty(texto) && texto.Length > oldString.Length)
			{
				List<string> adivinando = comandos.FindAll(w => w.StartsWith(texto));
				if (adivinando.Count > 0)
				{
					textAutocompletado.text = adivinando[0];
				}
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				mInput.value = textAutocompletado.text;
			}
		}
		#endregion
	}
}