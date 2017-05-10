//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Comando.cs (10/05/2017)														\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Clase de los comandos de MDev								\\
// Fecha Mod:		10/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Clase de los comandos de MDev</para>
	/// </summary>
	[AddComponentMenu("Moon Antonio/MDev/Comando")]
	[System.Serializable]
	public class Comando 
	{
		#region Propiedades
		private string nombreComando;
		/// <summary>
		/// <para>Nombre del comando</para>
		/// </summary>
		public string NombreComando
		{
			set { nombreComando = value; }
			get {  return nombreComando; }
		}

		private bool activo;
		/// <summary>
		/// <para>Define si esta activo el comando o no</para>
		/// </summary>
		public bool Activo
		{
			set { activo = value; }
			get { return activo; }
		}
		#endregion
	}
}