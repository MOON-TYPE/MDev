//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevConfig.cs (19/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Data del tipo de configuracion de MDev.						\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Data del tipo de configuracion de MDev.</para>
	/// </summary>
	[CreateAssetMenu(fileName ="MDevConfig",menuName ="MDev/Data")]
	public class MDevConfig : ScriptableObject
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Marcador inicial de MDev.</para>
		/// <example>
		/// '>' o '~'
		/// </example>
		/// </summary>
		public string marcador;															// Marcador inicial de MDev.
		/// <summary>
		/// <para>Direccion actual de MDev.</para>
		/// <example>
		/// /assets/objets
		/// </example>
		/// </summary>
		public string direccion;														// Direccion actual de MDev
		/// <summary>
		/// <para>Nombre de MDev.</para>
		/// <example>
		/// 'Terminal' o ' →'
		/// </example>
		/// </summary>
		public string nombreMDev;														// Nombre de MDev
		/// <summary>
		/// <para>Font de MDev.</para>
		/// </summary>
		public Font font;																// Font de MDev
		/// <summary>
		/// <para>Color del texto de MDev.</para>
		/// </summary>
		public Color colorTexto;														// Color del texto de MDev
		/// <summary>
		/// <para>Color de fondo de MDev.</para>
		/// </summary>
		public Color colorBG;															// Color de fondo de MDev
		/// <summary>
		/// <para>Color del texto autocompletado de MDev.</para>
		/// </summary>
		public Color colorAutoCompletar;												// Color del texto autocompletado de MDev
		#endregion
	}
}
