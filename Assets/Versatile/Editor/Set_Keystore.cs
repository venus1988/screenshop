using UnityEngine;
using System.Collections;
using UnityEditor;
[InitializeOnLoad]
public class Set_Keystore 
{
	static Set_Keystore()
	{
		if (EditorUserBuildSettings.selectedBuildTargetGroup == BuildTargetGroup.Android) 
		{
						PlayerSettings.companyName = "Versatile";
						PlayerSettings.Android.keystoreName = "D:/userversatile.keystore";
						PlayerSettings.Android.keyaliasName = "vtunity";
						PlayerSettings.Android.keyaliasPass = "VT_unity";
						PlayerSettings.Android.keystorePass = "VT_unity";
						//Debug.Log ("initialized..");
		}
	}
	void Update () {
	
	}
}
