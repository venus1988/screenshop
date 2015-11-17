using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Transform),true)]
public class Override_Transfrom : Editor
{


	bool ShowGlobal;

	public override void OnInspectorGUI ()
	{
		//base.OnInspectorGUI ();
		EditorGUIUtility.LookLikeControls ();
		Transform transform=(Transform)base.target;
		
		Vector3 localposition=EditorGUILayout.Vector3Field("Local Position",transform.localPosition,new GUILayoutOption[0]);	
		Vector3 localeular=EditorGUILayout.Vector3Field("Local Eular",transform.localEulerAngles,new GUILayoutOption[0]);
		Vector3 localscale=EditorGUILayout.Vector3Field("Local Scale",transform.localScale,new GUILayoutOption[0]);
		
	  

		//ShowGlobal=EditorGUILayout.Toggle("show",ShowGlobal,new GUILayoutOption[0]);
		EditorGUILayout.Separator ();	EditorGUILayout.Separator ();

		//if(ShowGlobal)
		{
			Vector3 position=EditorGUILayout.Vector3Field("Position",transform.position,new GUILayoutOption[0]);	
			Vector3 eular=EditorGUILayout.Vector3Field("Eular",transform.eulerAngles,new GUILayoutOption[0]);
			Vector3 scale=EditorGUILayout.Vector3Field("Scale",transform.lossyScale,new GUILayoutOption[0]);
		}
		
		if(GUI.changed)
		{
			Undo.RecordObject(transform,"Transfrom to undo");
			transform.localPosition=localposition;
			transform.localEulerAngles=localeular;
			transform.localScale=localscale;

		}
		
	}
	

}
