using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour 
{

	private float lastScreenwidth;
	void OnEnable () 
	{
		ResizeSprite();
		lastScreenwidth=Screen.width;
		//Debug.Log("***`Resize Sprite ");
		//Invoke("Resizes",1f);
	}
	public void Resizes()
	{
		ResizeSprite();
	}


	#if UNITY_EDITOR
		void Update()
		{
			if(lastScreenwidth!=Screen.width)
			{
				//lastScreenwidth = Screen.width;
				OnEnable();
			}
		}
	#endif
	void ResizeTexture()  //For MeshRenderer
	{
		float h,w;
		h=Camera.main.orthographicSize*2.0f;
		w=h * Screen.width/Screen.height;
		transform.localScale=new Vector3(w,h,1f);
	}
	void ResizeSprite()  //For Sprite only
	{
		SpriteRenderer sr = (SpriteRenderer)GetComponent ("Renderer");
		if (sr == null ||sr.sprite==null)
			return;
		
		// Set filterMode
		//sr.sprite.texture.filterMode = FilterMode.Point;
		float width = sr.sprite.bounds.size.x;
		float height=sr.sprite.bounds.size.y;

	
	
		Debug.Log ("width: " + width +"Height : "+height) ;

		float worldScreenHeight = Camera.main.orthographicSize * 2f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;




		Vector3 imgScale = new Vector3(1f, 1f, 1f);
		imgScale.x = worldScreenWidth / width;
		imgScale.y = worldScreenHeight / height;



		//Keep Aspect
		/*
		Vector2 ratio = new Vector2(width / height, height / width);
		if ((worldScreenWidth / width) > (worldScreenHeight / height))
		{
			// wider than tall
			imgScale.x = worldScreenWidth / width;
			imgScale.y = imgScale.x * ratio.y; 
		}
		else
		{
			// taller than wide
			imgScale.y = worldScreenHeight / height;
			imgScale.x = imgScale.y * ratio.x;             
		}*/
		transform.localScale=imgScale;
	}



}
