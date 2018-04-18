//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevAttribute.cs (19/04/2018)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Comandos de MDev.											\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using System;
using UnityEngine;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Comandos de MDev</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class MDevAttribute : Attribute 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Nombre del comando.</para>
		/// </summary>
		public string nombreComando;							// Nombre del comando
		/// <summary>
		/// <para>Descripcion del comando.</para>
		/// </summary>
		public string descripcionComando;						// Descripcion del comando
		#endregion

		#region Constructores
		/// <summary>
		/// <para>Crea un comando.</para>
		/// </summary>
		/// <param name="nom">Nombre del comando.</param>
		public MDevAttribute(string nom)// Crea un comando
		{
			nombreComando = nom;
		}

		/// <summary>
		/// <para>Crea un comando.</para>
		/// </summary>
		/// <param name="nom">Nombre del comando.</param>
		/// <param name="desc">Descripcion del comando.</param>
		public MDevAttribute(string nom, string desc)// Crea un comando
		{
			nombreComando = nom;
			descripcionComando = desc;
		}
		#endregion
	}
}
