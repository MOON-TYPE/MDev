//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevInput.cs (19/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Manager de las entradas del usuario.						\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Manager de las entradas del usuario.</para>
	/// </summary>
	public class MDevInput
	{
		#region Variables Privadas
		/// <summary>
		/// <para>Instancia de MDev.</para>
		/// </summary>
		private MDev mdev;												// Instancia de MDev
		/// <summary>
		/// <para>Tiempo de CD.</para>
		/// </summary>
		private float touchDelay = 1.5f;								// Tiempo de CD
		/// <summary>
		/// <para>Tiempo transcurrido.</para>
		/// </summary>
		private float tiempo = 0f;										// Tiempo transcurrido
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Constructor de <see cref="MDevInput"/>.</para>
		/// </summary>
		/// <param name="instance"></param>
		public MDevInput(MDev instance)// Constructor de MDevInput
		{
			mdev = instance;
		}
		#endregion

		#region Actualizador
		/// <summary>
		/// <para>Actualizador de <see cref="MDevInput"/>.</para>
		/// </summary>
		public void Update()// Actualizador de MDevInput
		{
			#region Plataforma
			// Comprobacion de plataformas
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if (MobileInput())
				{
					mdev.ToggleMDev();
					return;
				}
			}
			#endregion

			#region Input Init
			// Key para mostrar/ocultar MDev
			if (Input.GetKeyDown(mdev.data.keyToggle))
			{
				mdev.ToggleMDev();
				return;
			}
			#endregion

			#region Eventos
			// Si no se esta mostrando MDev, volver
			if (!mdev.MostrarMDev) return;
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				mdev.OnBackSpacePresionado();
				return;
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				mdev.OnTabPresionado();
				return;
			}

			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				mdev.OnEnterPresionado();
				return;
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				mdev.OnDownArrowPresionado();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				mdev.OnUpArrowPresionado();
			}
			else
			{
				if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) mdev.UpdateInputText(Input.inputString);
			}
			#endregion
		}
		#endregion

		#region Funcionalidad
		/// <summary>
		/// <para>Inputs de mobile.</para>
		/// </summary>
		/// <returns></returns>
		private bool MobileInput()// Inputs de mobile
		{
			// Si hay teclado
			if (mdev.touchScreenKeyboard != null)
			{
				if (mdev.touchScreenKeyboard.done)
				{
					// Establecer el texto
					mdev.SetInputText(mdev.touchScreenKeyboard.text);
					mdev.OnEnterPresionado();
					mdev.touchScreenKeyboard = null;
				}
			}

			// Comprobar si se ha tocado la pantalla lo suficiente
			if (Input.touchCount == mdev.data.mobileTouchCount)
			{
				tiempo += Time.deltaTime;
			}

			// Mostrar MDev y resetear tiempo
			if (tiempo > touchDelay)
			{
				tiempo = 0;
				if (mdev.MostrarMDev)
				{
					mdev.MostrarTouchScreenKeyboard();
					return false;
				}
				return true;
			}
			else return false;
		}
		#endregion
	}
}
