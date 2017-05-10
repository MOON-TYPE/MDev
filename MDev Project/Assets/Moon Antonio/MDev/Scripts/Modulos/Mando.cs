//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Mando.cs (15/03/2017)														\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Control del visualizador de inputs del mando.				\\
// Fecha Mod:		15/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections.Generic;
using XboxCtrlrInput;
#endregion

namespace Pendulum.Dev
{
	/// <summary>
	/// <para>Control del visualizador de inputs del mando</para>
	/// </summary>
	[AddComponentMenu("Pendulum/Dev/Mando")]
	public class Mando : MonoBehaviour 
	{
		/// <summary>
		/// <para>Controlador de pad.</para>
		/// </summary>
		public XboxController controllerInput;                                          // Controlador de pad
		/// <summary>
        /// <para>Llamada al controlador.</para>
        /// </summary>
        public bool llamadaControles = false;                                           // Llamada al controlador

		public List<UISprite> botones = new List<UISprite>();

		/// <summary>
		/// <para>Inicializa los controles.</para>
		/// </summary>
		private void InicializarControles()// Inicializa los controles
		{
			if (llamadaControles == false)
			{
				llamadaControles = true;

				int llamadaNumContr = XCI.GetNumPluggedCtrlrs();
				if (llamadaNumContr == 1)
				{
					Debug.Log("Solo se encontro " + llamadaNumContr + " controlador de XBOX.");
				}
				else if (llamadaNumContr == 0)
				{
					Debug.Log("No se encontro ningun controlador de XBOX.");
				}
				else
				{
					Debug.Log(llamadaNumContr + " controladores encontrados.");
				}

				XCI.DEBUG_LogControllerNames();
			}
		}

		private void Update()
		{
			ActualizarControles();
		}

		/// <summary>
		/// <para>Actualiza el estado de los controles.</para>
		/// </summary>
		private void ActualizarControles()// Actualiza el estado de los controles
		{
			if (XCI.GetButton(XboxButton.A))
			{
				botones[0].color = Color.red;
			}
			else
			{
				botones[0].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.B))
			{
				botones[1].color = Color.red;
			}
			else
			{
				botones[1].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.X))
			{
				botones[2].color = Color.red;
			}
			else
			{
				botones[2].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.Y))
			{
				botones[3].color = Color.red;
			}
			else
			{
				botones[3].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.RightBumper))
			{
				botones[4].color = Color.red;
			}
			else
			{
				botones[4].color = Color.white;
			}

			if (XCI.GetAxis(XboxAxis.RightTrigger) > 0)
			{
				botones[5].color = Color.red;
			}
			else
			{
				botones[5].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.LeftBumper))
			{
				botones[6].color = Color.red;
			}
			else
			{
				botones[6].color = Color.white;
			}

			if (XCI.GetAxis(XboxAxis.LeftTrigger) >= 1.0f)
			{
				botones[7].color = Color.red;
			}
			else
			{
				botones[7].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.Start))
			{
				botones[8].color = Color.red;
			}
			else
			{
				botones[8].color = Color.white;
			}

			if (XCI.GetButton(XboxButton.Back))
			{
				botones[9].color = Color.red;
			}
			else
			{
				botones[9].color = Color.white;
			}

			if (XCI.GetAxis(XboxAxis.LeftStickX) != 0 || XCI.GetAxis(XboxAxis.LeftStickY) != 0)
			{
				botones[10].color = Color.red;
			}
			else
			{
				botones[10].color = Color.white;
			}

			if (XCI.GetAxis(XboxAxis.RightStickX) != 0 || XCI.GetAxis(XboxAxis.RightStickY) != 0)
			{
				botones[11].color = Color.red;
			}
			else
			{
				botones[11].color = Color.white;
			}
		}
	}
}
