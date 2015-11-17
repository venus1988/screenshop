using UnityEngine;
using System.Collections;

public class Texcompress : MonoBehaviour {

	// Use this for initialization
	public Texture2D tex_source;

	void Start () 
	{
	
		float width = tex_source.width;
		float height = tex_source.height;
		//tex_source.Resize ((int)width / 2, (int)height / 2, TextureFormat.ARGB32, false);



		TextureScale.Bilinear (tex_source, (int)width / 2, (int)height / 2);

		  //2448 * 3264

		//1224  * 1632
	}

}
