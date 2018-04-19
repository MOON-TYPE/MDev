//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevInput.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para></para>
	/// </summary>
	public class MDevInput
	{
		#region Variables Privadas
		private MDev mdev;
		private float touchDelay = 1.5f;
		private float tiempo = 0f;
		#endregion

		#region Constructor
		public MDevInput(MDev instance)
		{
			mdev = instance;
		}
		#endregion

		#region Actualizador
		public void Update()
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if (MobileInput())
				{
					mdev.ToggleMDev();
					return;
				}
			}

			if (Input.GetKeyDown(mdev.data.keyToggle))
			{
				mdev.ToggleMDev();
				return;
			}

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
		}
		#endregion

		#region Funcionalidad
		private bool MobileInput()
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
