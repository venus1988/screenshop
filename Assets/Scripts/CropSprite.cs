using UnityEngine;
using System.Collections;


public class CropSprite : MonoBehaviour
{
	// Reference for sprite which will be cropped and it has BoxCollider or BoxCollider2D
	public GameObject spriteToCrop;
	public Material LineMaterial;
	public static CropSprite Instance;
	public static bool StopCrop=true;
	private Vector3 startPoint, endPoint;
	private bool isMousePressed;
	// For sides of rectangle. Rectangle that will display cropping area
	private LineRenderer leftLine, rightLine, topLine, bottomLine;
	SpriteRenderer spriteRenderer;


	//public delegate void OncropImageDone();
	//public static event OncropImageDone OnCropDone;


	void Awake()
	{
		Instance=this;
	    spriteRenderer = spriteToCrop.GetComponent<SpriteRenderer>();
	}
	
	void Start ()
	{
		isMousePressed = false;
		// Instantiate rectangle sides
		leftLine = createAndGetLine("LeftLine");
		rightLine = createAndGetLine("RightLine");
		topLine = createAndGetLine("TopLine");
		bottomLine = createAndGetLine("BottomLine");
	}
	// Creates line through LineRenderer component
	private LineRenderer createAndGetLine (string lineName)
	{
		GameObject lineObject = new GameObject(lineName);
		LineRenderer line = lineObject.AddComponent<LineRenderer>();
		line.material=LineMaterial;
		line.SetWidth(0.04f,0.04f);
		line.SetVertexCount(2);
		return line;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(startPoint, 0.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(endPoint, 0.5f);
	}


	void Update ()
	{
		//if(StopCrop)
		//	return;


		if(Input.GetMouseButtonDown(0) && isSpriteTouched(spriteToCrop))
		{
			isMousePressed = true;
			startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(isMousePressed)
				cropSprite();	
			isMousePressed = false;
		}
		if(isMousePressed && isSpriteTouched(spriteToCrop))
		{
			endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			drawRectangle();
		}
	}
	// Following method draws rectangle that displays cropping area
	private void drawRectangle()
	{

		leftLine.SetPosition(0, new Vector3(startPoint.x, endPoint.y, 0));
		leftLine.SetPosition(1, new Vector3(startPoint.x, startPoint.y, 0));
		
		rightLine.SetPosition(0, new Vector3(endPoint.x, endPoint.y, 0));
		rightLine.SetPosition(1, new Vector3(endPoint.x, startPoint.y, 0));
		
		topLine.SetPosition(0, new Vector3(startPoint.x, startPoint.y, 0));
		topLine.SetPosition(1, new Vector3(endPoint.x, startPoint.y, 0));
		
		bottomLine.SetPosition(0, new Vector3(startPoint.x, endPoint.y, 0));
		bottomLine.SetPosition(1, new Vector3(endPoint.x, endPoint.y, 0));
	}

	GameObject croppedSpriteObj;//sprite which is cropped
	public Sprite croppedSprite;
	// Following method crops as per displayed cropping area

	public Texture2D testtex;
	private void cropSprite()
	{
		Resources.UnloadUnusedAssets();
		// Calculate topLeftPoint and bottomRightPoint of drawn rectangle
		Vector3 topLeftPoint = startPoint, bottomRightPoint=endPoint;
	
		if((startPoint.x > endPoint.x))
		{
			topLeftPoint = endPoint;
			bottomRightPoint = startPoint;
		}
		if(bottomRightPoint.y > topLeftPoint.y)
		{
			float y = topLeftPoint.y;
			topLeftPoint.y = bottomRightPoint.y;
			bottomRightPoint.y = y;
		}

		if(Vector3.Distance(topLeftPoint,bottomRightPoint)<0.5f)
			return;

		//
	//	OnCropDone();

		
		SpriteRenderer spriteRenderer = spriteToCrop.GetComponent<SpriteRenderer>();

		Texture2D spriteTexture = spriteRenderer.sprite.texture;

		Rect spriteRect = spriteToCrop.GetComponent<SpriteRenderer>().sprite.textureRect;



		int pixelsToUnits = 100; // It's PixelsToUnits of sprite which would be cropped
		
		// Crop sprite
		
		croppedSpriteObj = new GameObject("CroppedSprite");
		Rect croppedSpriteRect = spriteRect;
		Debug.Log ("width-" + croppedSpriteRect.width + "height-" + croppedSpriteRect.height + "x-" + croppedSpriteRect.x + "y-" + croppedSpriteRect.y);


		croppedSpriteRect.width = (Mathf.Abs(bottomRightPoint.x - topLeftPoint.x)*pixelsToUnits)* (1/spriteToCrop.transform.localScale.x);
		croppedSpriteRect.x = (Mathf.Abs(topLeftPoint.x - (spriteRenderer.bounds.center.x-spriteRenderer.bounds.size.x/2)) *pixelsToUnits)* (1/spriteToCrop.transform.localScale.x);
		croppedSpriteRect.height = (Mathf.Abs(bottomRightPoint.y - topLeftPoint.y)*pixelsToUnits)* (1/spriteToCrop.transform.localScale.y);
		croppedSpriteRect.y = ((topLeftPoint.y - (spriteRenderer.bounds.center.y - spriteRenderer.bounds.size.y/2))*(1/spriteToCrop.transform.localScale.y))* pixelsToUnits - croppedSpriteRect.height;//*(spriteToCrop.transform.localScale.y);

		Debug.Log ("**width-" + croppedSpriteRect.width + "height-" + croppedSpriteRect.height + "x-" + croppedSpriteRect.x + "y-" + croppedSpriteRect.y);

		testtex=new Texture2D((int)croppedSpriteRect.width,(int)croppedSpriteRect.height);
		Color[] c=	spriteTexture.GetPixels((int)croppedSpriteRect.x,(int)croppedSpriteRect.y,(int)croppedSpriteRect.width,(int)croppedSpriteRect.height);
		testtex.SetPixels(c);
		testtex.Apply();

		croppedSprite = Sprite.Create(spriteTexture, croppedSpriteRect, new Vector2(0,1), pixelsToUnits);
		SpriteRenderer cropSpriteRenderer = croppedSpriteObj.AddComponent<SpriteRenderer>();	
		cropSpriteRenderer.sprite = croppedSprite;



		topLeftPoint.z = 0.5f;
		croppedSpriteObj.transform.position = topLeftPoint;
		croppedSpriteObj.transform.parent = spriteToCrop.transform.parent;
		croppedSpriteObj.transform.localScale = spriteToCrop.transform.localScale;

		spriteRenderer.color=Color.gray;
		//Destroy(spriteToCrop);
		StopCrop=true;
	}
	/// <summary>
	/// Destroy previous sprite
	/// </summary>
	public void ReCrop()
	{
		Destroy(croppedSpriteObj);
		startPoint=endPoint=Vector3.zero;
		if(leftLine!=null)
		drawRectangle();
		spriteRenderer.color=Color.white;
	}

	public void ResetifthereAre()
	{

	}

	public Texture2D CroppedImage
	{
		get
		{
//			return croppedSprite.texture;
			return testtex;
		}
	}



	
	// Following method checks whether sprite is touched or not. There are two methods for simple collider and 2DColliders. you can use as per requirement and comment another one.
	
	// For simple 3DCollider
	// private bool isSpriteTouched(GameObject sprite)
	// {
	// RaycastHit hit;
	// Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	// if (Physics.Raycast (ray, out hit))
	// {
	// if (hit.collider != null && hit.collider.name.Equals (sprite.name))
	// return true;
	// }
	// return false;
	// }
	
	// For 2DCollider
	private bool isSpriteTouched(GameObject sprite)
	{
		Vector3 posFor2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit2D = Physics2D.Raycast(posFor2D, Vector2.zero);
		if (hit2D != null && hit2D.collider != null)
		{
			if(hit2D.collider.name.Equals(sprite.name))
				return true;
		}
		return false;
	}

	//Retruen texture in Circular Formate crop with specified Radious
	Texture2D CalculateTexture(
		int h, int w,float r,float cx,float cy,Texture2D sourceTex
		){
		Color [] c= sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
		Texture2D b=new Texture2D(h,w);
		for (int i = 0 ; i<(h*w);i++){
			int y=Mathf.FloorToInt(((float)i)/((float)w));
			int x=Mathf.FloorToInt(((float)i-((float)(y*w))));
			if (r*r>=(x-cx)*(x-cx)+(y-cy)*(y-cy)){
				b.SetPixel(x, y, c[i]);
			}else{
				b.SetPixel(x, y, Color.clear);
			}
		}
		b.Apply ();
		return b;
	}



	public static Texture2D textureFromSprite(Sprite sprite)
	{
		if(sprite.rect.width != sprite.texture.width)
		{
			Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
			Color[] newColors = sprite.texture.GetPixels(Mathf.RoundToInt(sprite.textureRect.x), 
			                                             Mathf.RoundToInt(sprite.textureRect.y), 
			                                             Mathf.RoundToInt(sprite.textureRect.width), 
			                                             Mathf.RoundToInt(sprite.textureRect.height));




			newText.SetPixels(newColors);
			newText.Apply();
			return newText;
		} 
		else
			return sprite.texture;
	}
}
