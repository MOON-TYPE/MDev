//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevGUI.cs (19/04/2018)														\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Interfaz de MDev.											\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Interfaz de MDev.</para>
	/// </summary>
	public class MDevGUI 
	{
		#region Variables Privadas
		/// <summary>
		/// <para>Instancia de <see cref="MDev"/>.</para>
		/// </summary>
		private MDev mdev;											// Instancia de MDev
		/// <summary>
		/// <para>Configuracion de <see cref="MDev"/>.</para>
		/// </summary>
		private MDevConfig config;									// Configuracion de MDev
		/// <summary>
		/// <para>Textura.</para>
		/// </summary>
		private Texture2D texture2D;								// Textura
		#endregion

		#region Propiedades
		/// <summary>
		/// <para>Estilo visual de MDev.</para>
		/// </summary>
		public GUIStyle EstiloMDev { get; private set; }
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Constructor de <see cref="MDevGUI"/>.</para>
		/// </summary>
		/// <param name="instance"></param>
		public MDevGUI(MDev instance)// Constructor de MDevGUI
		{
			// Asignar la instancia de MDev y la configuracion visual
			this.mdev = instance;
			this.config = instance.data;

			// Crear el estilo visual
			EstiloMDev = new GUIStyle();
			EstiloMDev.font = config.font;
			EstiloMDev.fontSize = 16;
			EstiloMDev.richText = true;
			EstiloMDev.normal.textColor = config.colorTexto;
			EstiloMDev.hover.textColor = config.colorAutoCompletar;
			EstiloMDev.active.textColor = config.colorAutoCompletar;
			EstiloMDev.onHover.textColor = config.colorAutoCompletar;
			EstiloMDev.onActive.textColor = config.colorAutoCompletar;

			// Generar la textura de contencion
			texture2D = new Texture2D(2, 2);
			texture2D.SetPixels(0, 0, texture2D.width, texture2D.height, new Color[] { config.colorBG, config.colorBG, config.colorBG, config.colorBG });
			texture2D.wrapMode = TextureWrapMode.Repeat;
			texture2D.Apply();

			// Aplicar el fondo.
			EstiloMDev.normal.background = texture2D;
		}
		#endregion

		#region GUI
		/// <summary>
		/// <para>Interfaz de <see cref="MDev"/>.</para>
		/// </summary>
		internal void OnGUI()// Interfaz de MDev
		{
			// Mostrar el marcador inicial
			GUILayout.Label(mdev.Historial + mdev.MDevLinea + mdev.InputText, EstiloMDev);

			// Nodo Autocompletar y ejecutar
			if (mdev.AutoCompletar.Count > 0)
			{
				for (int n = 0; n < mdev.AutoCompletar.Count; n++)
				{
					GUI.SetNextControlName(mdev.AutoCompletar[n]);
					GUIStyle t = EstiloMDev;
					if (n == mdev.AutoCompletarIndex) GUI.color = config.colorAutoCompletar;
					else GUI.color = config.colorTexto;
					if (GUILayout.Button(mdev.AutoCompletar[n], t))
					{
						mdev.CambiarInput(mdev.AutoCompletar[n]);
						mdev.PreEjecutar();
					}
				}
			}
			else
			{
				GUI.color = config.colorTexto;
			}	
		}
		#endregion
	}
}
