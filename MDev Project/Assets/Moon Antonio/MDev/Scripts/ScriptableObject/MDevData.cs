//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevData.cs (10/05/2017)														\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		ScriptableObject de la data de MDev							\\
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
	/// <para>ScriptableObject de la data de MDev</para>
	/// </summary>
	[AddComponentMenu("Moon Antonio/MDev/MDevData")]
	[System.Serializable]
	public class MDevData : ScriptableObject 
	{
		#region Lista
		/// <summary>
		/// <para>Lista de comandos de MDev</para>
		/// </summary>
		public List<Comando> Comandos = new List<Comando>();						// Lista de comandos de MDev
		#endregion
	}
}