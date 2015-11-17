// CopyTransform Helper Versatile
//% Ctrl   # (shift), & (alt), 
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class TransformCopier : ScriptableObject 
{
	
	private static Vector3 position;
	private static Quaternion rotation;
	private static Vector3 scale;
	private static string myName;

	static bool state=true;
	[MenuItem ("Versatile/Copier/Toogle_on_off &d",false,400)]
	static void ToogleOnoff()
	{	

		GameObject[] g = Selection.gameObjects;
		if (state) 
		{	state = false;
			for (int i=0; i<g.Length; i++)
			{
				if (g [i].activeSelf)
					g [i].SetActive (false);
			}
		} 
		else
		{	state = true;
			for (int i=0; i<g.Length; i++)
			{
				if (!g [i].activeSelf)
					g [i].SetActive (true);
			}
		}
	}


	[MenuItem ("Versatile/Copier/Copy &c",false,0)]
	static void DoRecord ()
	{
		position = Selection.activeTransform.localPosition;
		rotation = Selection.activeTransform.localRotation;
		scale = Selection.activeTransform.localScale;
		myName = Selection.activeTransform.name;
		EditorUtility.DisplayDialog("Transform Copy", "Local position, rotation, & scale of "+myName +" copied relative to parent.", "OK", "");
		Debug.Log(System.Environment.GetEnvironmentVariable("HOMEDRIVE") + System.Environment.GetEnvironmentVariable("HOMEPATH") + @"\.android\debug.keystore");
		Debug.Log(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + @"/.android/debug.keystore");
	}
	
	// PASTE POSITION:
	[MenuItem ("Versatile/Copier/PastePosition &p",false,50)]
	static void DoApplyPositionXYZ () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = position;
	}
	
	[MenuItem ("Versatile/Copier/Paste Position X",false,51)]
	static void DoApplyPositionX () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = new Vector3(position.x, selection.localPosition.y, selection.localPosition.z);
	}
	
	[MenuItem ("Versatile/Copier/Paste Position Y",false,52)]
	static void DoApplyPositionY () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = new Vector3(selection.localPosition.x, position.y, selection.localPosition.z);
	}
	
	[MenuItem ("Versatile/Copier/Paste Position Z",false,53)]
	static void DoApplyPositionZ () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = new Vector3(selection.localPosition.x, selection.localPosition.y, position.z);
	}
	
	// PASTE ROTATION:

	[MenuItem ("Versatile/Copier/Paste Rotation &r",false,100)]
	static void DoApplyRotationXYZ () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = rotation;
	}
	
	[MenuItem ("Versatile/Copier/Paste Rotation X",false,101)]
	static void DoApplyRotationX () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = Quaternion.Euler(rotation.eulerAngles.x, selection.localRotation.eulerAngles.y, selection.localRotation.eulerAngles.z);
	}
	
	[MenuItem ("Versatile/Copier/Paste Rotation Y",false,102)]
	static void DoApplyRotationY () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = Quaternion.Euler(selection.localRotation.eulerAngles.x, rotation.eulerAngles.y, selection.localRotation.eulerAngles.z);
	}
	
	[MenuItem ("Versatile/Copier/Paste Rotation Z",false,103)]
	static void DoApplyRotationZ () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = Quaternion.Euler(selection.localRotation.eulerAngles.x, selection.localRotation.eulerAngles.y, rotation.eulerAngles.z);
	}
	
	// PASTE SCALE:


	[MenuItem ("Versatile/Copier/Paste Scale &s",false,150)]
	static void DoApplyScaleXYZ () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = scale;
	}
	
	[MenuItem ("Versatile/Copier/Paste Scale X",false,151)]
	static void DoApplyScaleX ()
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3(scale.x, selection.localScale.y, selection.localScale.z);
	}
	
	[MenuItem ("Versatile/Copier/Paste Scale Y",false,152)]
	static void DoApplyScaleY () {
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3(selection.localScale.x, scale.y, selection.localScale.z);
	}
	
	[MenuItem ("Versatile/Copier/Paste Scale Z",false,153)]
	static void DoApplyScaleZ () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3(selection.localScale.x, selection.localScale.y, scale.z);
	}
	
	// CHANGE LOCAL ROTATION :


	[MenuItem ("Versatile/Copier/localRotation.x + 90",false,200)]
	static void localRotateX90 () {
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = selection.localRotation*Quaternion.Euler(90f,0f,0f);
	}
	
	[MenuItem ("Versatile/Copier/localRotation.y + 90",false,201)]
	static void localRotateY90 () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = selection.localRotation*Quaternion.Euler(0f,90f,0f);
	}
	
	[MenuItem ("Versatile/Copier/localRotation.z + 90",false,202)]
	static void localRotateZ90 ()
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = selection.localRotation*Quaternion.Euler(0f,0f,90f);
	}
	
	// SWAP:


	[MenuItem ("Versatile/Copier/Swap Y and Z Scale", false, 251)]
	static void SwapYZScale () 
	{
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3 (selection.localScale.x,selection.localScale.z,selection.localScale.y);
	}


	[MenuItem("Versatile/Helper/Clear PlayerPrefs",false,254)]
	static void ClearPlayerprefs()
	{
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
		Debug.Log ("Cleared All PlayerPrefs.....");
	}

	[MenuItem("Versatile/Helper/SimulateParticle &x",false,253)]
	static void SimulateParticlesystem()
	{
		GameObject obj=Selection.activeGameObject;
		if (obj.particleSystem) 
		{
			obj.particleSystem.Play();
		}
	}
	[MenuItem("Versatile/Helper/AutoScale #[",false,254)]
	static void AutoScaleTexture()
	{
		GameObject g = Selection.activeGameObject;
		if (g.renderer!=null) 
		{
			float tex_width=g.gameObject.renderer.sharedMaterial.mainTexture.width/200f;
			float tex_height=g.gameObject.renderer.sharedMaterial.mainTexture.height/200f;
			g.transform.localScale=new Vector3(tex_width,tex_height,0f);
		}
	}
	        



}