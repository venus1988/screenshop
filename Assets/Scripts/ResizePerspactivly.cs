using UnityEngine;
using System.Collections;

public class ResizePerspactivly : MonoBehaviour
{

	public static ResizePerspactivly _Instance;
	void Awake()
	{
		_Instance = this;
	}

	public Texture2D Img;
public	void OnEnable()
	{



		//transform.GetComponent<SpriteRenderer>().sprite=Sprite.Create(Img,new Rect(0,0,Img.width,Img.height),new Vector2(0.5f,0.5f),100f);

		SpriteRenderer sr = (SpriteRenderer)GetComponent ("Renderer");
		if (sr == null)
			return;
		
		// Set filterMode
		sr.sprite.texture.filterMode = FilterMode.Point;
		
		// Get stuff
		double width = sr.sprite.bounds.size.x;
		Debug.Log ("width: " + width);

		double worldScreenHeight = Camera.main.orthographicSize * 2.0;
		double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		// Resize
		transform.localScale = new Vector2 (1, 1) * (float)(worldScreenWidth / width);

		Debug.Log ("Scale Set===========>"+transform.localScale);
	}
}