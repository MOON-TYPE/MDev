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

		#region Metodos Publicos
		public void PreEjecutar()
		{
			/*string result = ExecuteCommand(InputText);
			Historial += MDevLinea + InputText + "\n" + (!string.IsNullOrEmpty(result) ? (result + "\n") : "");
			InputText = "";*/
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
	}
}
