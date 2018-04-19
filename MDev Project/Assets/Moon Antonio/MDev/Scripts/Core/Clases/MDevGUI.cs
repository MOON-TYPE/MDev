//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevGUI.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDev
{
	public class MDevGUI 
	{
		#region Variables Privadas
		private MDev mdev;
		private MDevConfig config;
		private Texture2D texture2D;
		#endregion

		#region Propiedades
		public GUIStyle EstiloMDev { get; private set; }
		#endregion

		#region Constructor
		public MDevGUI(MDev terminal)
		{
			this.mdev = terminal;
			this.config = terminal.data;
			EstiloMDev = new GUIStyle();
			EstiloMDev.font = config.font;
			EstiloMDev.fontSize = 16;
			EstiloMDev.richText = true;
			EstiloMDev.normal.textColor = config.colorTexto;
			EstiloMDev.hover.textColor = config.colorAutoCompletar;
			EstiloMDev.active.textColor = config.colorAutoCompletar;
			EstiloMDev.onHover.textColor = config.colorAutoCompletar;
			EstiloMDev.onActive.textColor = config.colorAutoCompletar;

			texture2D = new Texture2D(2, 2);
			texture2D.SetPixels(0, 0, texture2D.width, texture2D.height, new Color[] { config.colorBG, config.colorBG, config.colorBG, config.colorBG });
			texture2D.wrapMode = TextureWrapMode.Repeat;
			texture2D.Apply();

			EstiloMDev.normal.background = texture2D;
		}
		#endregion

		#region GUI
		internal void OnGUI()
		{
			GUILayout.Label(mdev.Historial + mdev.MDevLinea + mdev.InputText, EstiloMDev);
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
