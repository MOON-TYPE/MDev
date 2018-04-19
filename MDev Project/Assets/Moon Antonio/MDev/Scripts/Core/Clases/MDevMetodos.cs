//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevMetodos.cs (19/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador de la obtencion de los comandos.				\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#endregion

namespace MoonAntonio.MDev
{
	public class MDevMetodos 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Nombres de metodos con comandos.</para>
		/// </summary>
		private List<string> nombresMetodos = new List<string>();						// Nombres de metodos con comandos
		#endregion

		#region Propiedades
		/// <summary>
		/// <para>Metodos con comandos.</para>
		/// </summary>
		public List<MethodInfo> Metodos { get; private set; }
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Constructor de <see cref="MDevMetodos"/>.</para>
		/// </summary>
		public MDevMetodos()// Constructor de MDevMetodos
		{
			// Inicializar los metodos y obtener todos los elementos monobehaviour de la escena
			Metodos = new List<MethodInfo>();
			MonoBehaviour[] escenaActiva = GameObject.FindObjectsOfType<MonoBehaviour>();

			// Recorrer cada elemento monobehaviour de la escena
			foreach (MonoBehaviour mono in escenaActiva)
			{
				Type monoType = mono.GetType();

				// Recuperar los campos de la instancia mono
				MethodInfo[] metodosObtenidos = monoType.GetMethods(BindingFlags.Instance | BindingFlags.Public);

				// Busca todos los campos y encontrar los atributos
				for (int i = 0; i < metodosObtenidos.Length; i++)
				{
					MDevAttribute comandos = Attribute.GetCustomAttribute(metodosObtenidos[i], typeof(MDevAttribute)) as MDevAttribute;

					// Si detectamos algun atributo, agregar datos.
					if (comandos != null)
					{
						nombresMetodos.Add(comandos.nombreComando);
						Metodos.Add(metodosObtenidos[i]);
					}
				}
			}
		}
		#endregion

		#region Funcionalidad
		/// <summary>
		/// <para>Obtiene todos los comandos.</para>
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public string[] GetComandos(string input)// Obtiene todos los comandos
		{
			return nombresMetodos.Where(k => k.Contains(input)).ToArray();
		}
		#endregion
	}
}
