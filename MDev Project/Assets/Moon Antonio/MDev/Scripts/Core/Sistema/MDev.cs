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
	}
}
