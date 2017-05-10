//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDevDataInspector.cs (10/05/2017)											\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Inspector de MDevData										\\
// Fecha Mod:		10/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
#endregion

namespace MoonAntonio.MDev
{
	/// <summary>
	/// <para>Inspector de MDevData</para>
	/// </summary>
	[AddComponentMenu("Moon Antonio/MDev/MDevDataInspector")]
	[CustomEditor(typeof(MDevData))]
	public class MDevDataInspector : Editor 
	{
		string txt = string.Empty;

		public override void OnInspectorGUI()
		{
			MDevData myTarget = (MDevData)target;
			

			EditorGUILayout.BeginVertical("box");

			EditorGUILayout.LabelField("Hay " + myTarget.Comandos.Count + " comandos activos.");

			EditorGUILayout.EndVertical();


			EditorGUILayout.BeginVertical("box");

			for (int n = 0; n < myTarget.Comandos.Count; n++)
			{
				EditorGUILayout.BeginHorizontal("box");

				myTarget.Comandos[n].Activo = EditorGUILayout.Toggle(myTarget.Comandos[n].Activo, GUILayout.Width(20));
				EditorGUILayout.LabelField("ON/OFF", GUILayout.Width(60));
				
				myTarget.Comandos[n].NombreComando = EditorGUILayout.TextField(myTarget.Comandos[n].NombreComando);
				if (GUILayout.Button("X", GUILayout.Width(20))) RemoveComando(n);

				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();

			EditorGUILayout.Space();

			EditorGUILayout.BeginVertical("box");

			txt = EditorGUILayout.TextField(txt);
			if (GUILayout.Button("+")) AddComando(txt);

			EditorGUILayout.EndVertical();
		}

		public void AddComando(string value)
		{
			MDevData myTarget = (MDevData)target;

			myTarget.Comandos.Add(new Comando());
			myTarget.Comandos[myTarget.Comandos.Count - 1].NombreComando = value;
			myTarget.Comandos[myTarget.Comandos.Count - 1].Activo = true;
		}

		public void RemoveComando(int id)
		{
			MDevData myTarget = (MDevData)target;

			myTarget.Comandos.RemoveAt(id);
		}
	}
}