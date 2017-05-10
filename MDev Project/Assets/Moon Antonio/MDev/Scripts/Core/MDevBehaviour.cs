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
		/// <summary>
		/// <para>Area de la consola visual</para>
		/// </summary>
		public UITextList consolaReport;							// Area de la consola visual
		/// <summary>
		/// <para>Area de la consola donde escribir</para>
		/// </summary>
		public UIInput mInput;										// Area de la consola donde escribir
		/// <summary>
		/// <para>Root de mando</para>
		/// </summary>
		public GameObject mando;									// Root de mando
		/// <summary>
		/// <para>Texto autocompletado</para>
		/// </summary>
		public UILabel textAutocompletado;							// Texto autocompletado
		#endregion

		#region Variables Estaticas
		/// <summary>
		/// <para>Activar/Desactivar debug</para>
		/// </summary>
		public static bool devDebug = true;							// Activar/Desactivar debug
		/// <summary>
		/// <para>Activar/Desactivar mando</para>
		/// </summary>
		public static bool devMando = true;							// Activar/Desactivar mando
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Texto predictivo</para>
		/// </summary>
		private string texto = "";									// Texto predictivo
		#endregion

		// test
		public List<string> comandos = new List<string>();

		#region Inicializadores
		/// <summary>
		/// <para>Inicializa MDevBehaviour</para>
		/// </summary>
		private void Start()// Inicializa MDevBehaviour
		{
			mInput.label.maxLineCount = 1;
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de MDevBehaviour</para>
		/// </summary>
		private void Update()// Actualizador de MDevBehaviour
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
			if (consolaReport != null)
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
			string temp = value;
			string temp2 = string.Empty;

			temp2 = temp.Replace(".", "");
			Debug.Log(temp2);

			for (int n = 0; n < comandos.Count; n++)
			{
				if (comandos[n] == value)
				{
					if (!IsInvoking(temp2)) Invoke(temp2, 0f);
				}
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

		#region Comandos
		public void help()
		{
			consolaReport.Add("> .help -> Muestra la lista de comandos.");
			consolaReport.Add("> .devDebug.enable/disable -> Activa/Desactiva el debug.");
			consolaReport.Add("> .devMando.enable/disable -> Activa/Desactiva el mando de referenciacion.");
			consolaReport.Add("> .devNotifi.enable/disable -> Activa/Desactiva las notificaciones visuales.");
			mInput.value = "";
			mInput.isSelected = false;
		}

		public void exit()
		{
			Application.Quit();
			mInput.value = "";
			mInput.isSelected = false;
		}

		public void devDebugenable()
		{
			devDebug = true;
			consolaReport.Add("> El debug de la consola a sido activado.");
			mInput.value = "";
			mInput.isSelected = false;
		}

		public void devDebugdisable()
		{
			devDebug = false;
			consolaReport.Add("> El debug de la consola a sido desactivado.");
			mInput.value = "";
			mInput.isSelected = false;
		}

		public void devMandoenable()
		{
			devMando = true;
			consolaReport.Add("> El visualizador del input esta activado.");
			mInput.value = "";
			mInput.isSelected = false;
		}

		public void devMandodisable()
		{
			devMando = false;
			consolaReport.Add("> El visualizador del input esta desactivado.");
			mInput.value = "";
			mInput.isSelected = false;
		}
		#endregion
	}
}