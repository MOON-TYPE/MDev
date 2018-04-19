//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// AlexandriaTest.cs (19/04/2018)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Ejemplo para usar MDev										\\
// Fecha Mod:		19/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using MoonAntonio.MDev;
#endregion

namespace MoonAntonio
{
	/// <summary>
	/// <para>Ejemplo para usar MDev.</para>
	/// </summary>
	public class AlexandriaTest : MonoBehaviour 
	{
		#region Variables Publicas
		public Transform unidad;
		public bool stop;
		#endregion

		#region Actualizadores
		private void Update()
		{
			if (!stop) unidad.transform.Rotate(new Vector3(0, 1, 0));
		}
		#endregion

		#region Comandos
		[MDev("stop-unit", "Detiene la rotacion de la unidad.")]
		public void StopUnit()
		{
			stop = true;
		}


		[MDev("rotate-unit", "Rota la unidad")]
		public void RotateUnit()
		{
			stop = false;
		}

		[MDev("move-unit", "move-unit(x,y,z) Mueve la unidad")]
		public void Move(float x, float y, float z)
		{
			transform.position = new Vector3(x, y, z);
		}
		#endregion
	}
}
