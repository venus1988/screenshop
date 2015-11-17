using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Cropper : MonoBehaviour
{


	public LineRenderer LineRenderer;
	public float TouchThreshold = 0.1f;
	public Transform Image;

	Plane 			imagePlane;
	List<Vector3> 	nodes = new List<Vector3>();
	bool 			selectionMarqueeEnabled;
	public Sprite originalSprite;
	public SpriteRenderer originalSpriteR;

	GameObject 		croppedObject;
	public Sprite 	croppedSprite;
	public Texture2D 		croppedTex;

	Vector2 pivotofSprite;
	public static Cropper Instance;
	public delegate void OncropImageDone();
	public static event OncropImageDone OnCropDone;
	public static bool StopCrop=true;

	void Start ()
	{
		if(Image != null)
		{
			imagePlane = new Plane(-Image.forward, Image.position);
			//	originalSprite = Image.GetComponent<SpriteRenderer>().sprite;
		}
	}
	void Awake()
	{
		Instance=this;
	}

	//float 			minimum  
	
	// Use this for initialization


	public Camera cam;
    
	// Update is called once per frame
	void Update ()
	{
		if(StopCrop)
			return;

		if(Image == null)
			return;

		if(Input.GetMouseButtonDown(0))
		{
			nodes.Clear();
			LineRenderer.SetVertexCount(0);
			pivotofSprite=new Vector2(originalSpriteR.sprite.bounds.extents.x * 100f,originalSpriteR.sprite.bounds.extents.y * 100f);

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			float d;
			if(imagePlane.Raycast(ray, out d))
			{
                Vector3 p = ray.GetPoint(d);
				Vector2 localSpacePoint = Image.InverseTransformPoint(p);
                
				Vector2 pixelPoint = localSpacePoint * originalSpriteR.sprite.pixelsPerUnit + pivotofSprite;


				if(pixelPoint.x > 0 && pixelPoint.x < originalSpriteR.sprite.texture.width &&
				   pixelPoint.y > 0 && pixelPoint.y < originalSpriteR.sprite.texture.height)
				{
					selectionMarqueeEnabled = true;
				}
			}
            
        }
		if(Input.GetMouseButton(0) && selectionMarqueeEnabled)
		{
			Ray ray =cam.ScreenPointToRay(Input.mousePosition);
			float d;
			if(imagePlane.Raycast(ray, out d))
			{
				Vector3 p = ray.GetPoint(d);

                // Add this point to selection list only if we have crossed a minimum threshold
				if(!(nodes.Count > 0 && (p - nodes[nodes.Count - 1]).magnitude < TouchThreshold))
                {

					AddToSelection (p);
                }

			}
		}
		if(Input.GetMouseButtonUp(0) && selectionMarqueeEnabled)
		{
			selectionMarqueeEnabled = false;
		
			if(nodes.Count > 2)
			{
				Crop();               
			}
		}

		// Marching Ants animation
		LineRenderer.sharedMaterial.mainTextureOffset = new Vector2(2 * Time.time, 0.0f);

	}

	void AddToSelection (Vector3 p)
	{
		nodes.Add (p);
		LineRenderer.SetVertexCount (nodes.Count);

		float lineLength = 0;

		for (int i = 0; i < nodes.Count; i++)
		{
			if(i > 0)
				lineLength += (nodes [i] - nodes[i - 1]).magnitude;
			LineRenderer.SetPosition (i, nodes [i]);
		}

		LineRenderer.sharedMaterial.mainTextureScale = new Vector2(lineLength * 10, 1);
	}
	
	public void Crop()
	{
		Vector2[] pixelNodes = new Vector2[nodes.Count]; 
		for (int i = 0; i < nodes.Count; i++)
		{
			Vector2 localSpaceNode = Image.InverseTransformPoint(nodes[i]);
			pixelNodes[i] = localSpaceNode * originalSpriteR.sprite.pixelsPerUnit + pivotofSprite;
		}

		Rect cropRect = GetSelectionBounds(pixelNodes);

		croppedTex = new Texture2D((int)cropRect.width, (int)cropRect.height, TextureFormat.ARGB32, false);


		for(int x = 0; x < cropRect.width; x++)
		{
			for(int y = 0; y < cropRect.height; y++)
			{
				Vector2 sourcePos = new Vector2(x + cropRect.min.x, y + cropRect.min.y);
				if(!IsInPolygon(pixelNodes, sourcePos))
					croppedTex.SetPixel(x, y, new Color(0, 0, 0, 0));
				else
					croppedTex.SetPixel(x, y, originalSpriteR.sprite.texture.GetPixel((int)sourcePos.x, (int)sourcePos.y));

			}
		}

		croppedTex.Apply();
		//Image.GetComponent<SpriteRenderer>().color=Color.gray;
		// Save to Assets folder in Editor
		string dataPath;

#if UNITY_EDITOR
		dataPath = Application.dataPath;
#else
		dataPath = Application.persistentDataPath;
#endif

		byte[] bytes = croppedTex.EncodeToPNG();
		File.WriteAllBytes(dataPath + "/CroppedPhoto.png", bytes);
        

		croppedSprite = Sprite.Create(croppedTex, new Rect(0, 0, cropRect.width, cropRect.height), new Vector2(0.5f, 0.5f), originalSpriteR.sprite.pixelsPerUnit);
		Vector3 positionOffset = (cropRect.center - pivotofSprite) / originalSpriteR.sprite.pixelsPerUnit;

		croppedObject = new GameObject("Cropped_Sprite");

		Debug.Log ("Posoffset==>"+positionOffset);
		croppedObject.transform.position = LineRenderer.transform.localPosition; //Image.position; //+ positionOffset;
		croppedObject.transform.rotation = Image.rotation;
		croppedObject.transform.parent=Image.parent;
		croppedObject.transform.localScale = Image.localScale;

		croppedObject.AddComponent<SpriteRenderer>().sprite = croppedSprite;


		Image.gameObject.SetActive(false);
		this.LineRenderer.gameObject.SetActive (false);
		OnCropDone();
		StopCrop=true;

	}

	public void ReCrop()
	{
		Image.gameObject.SetActive(true);
		this.LineRenderer.gameObject.SetActive (true);
		if(croppedObject != null)
			Destroy(croppedObject);

		if(croppedSprite != null)
			Destroy(croppedSprite);

		if(croppedTex != null)
			Destroy(croppedTex); 

		nodes.Clear();
		LineRenderer.SetVertexCount(0);
	}

	bool IsInPolygon(Vector2[] poly, Vector2 p)
	{
		Vector2 p1, p2;
		bool inside = false;

		Vector2 oldPoint = new Vector2(poly[poly.Length - 1].x, poly[poly.Length - 1].y);

		for (int i = 0; i < poly.Length; i++)
		{
			Vector2 newPoint = new Vector2(poly[i].x, poly[i].y);

			if (newPoint.x > oldPoint.x)
			{
				p1 = oldPoint;				
				p2 = newPoint;
			}
			else
			{
				p1 = newPoint;
				p2 = oldPoint;
			}

			if ((newPoint.x < p.x) == (p.x <= oldPoint.x)
			    && (p.y - (long) p1.y)*(p2.x - p1.x)
			    < (p2.y - (long) p1.y)*(p.x - p1.x))
			{
				inside = !inside;
			}		
			
			oldPoint = newPoint;
		}
		
		return inside;
	}


	Rect GetSelectionBounds(Vector2[] poly)
	{
		Vector2 Min, Max;

		Min = Max = new Vector2(poly[poly.Length - 1].x, poly[poly.Length - 1].y);
		
		for (int i = 0; i < poly.Length; i++)
		{
			Vector2 p = new Vector2(poly[i].x, poly[i].y);
			
			if (p.x > Max.x) Max.x = p.x;
			if (p.x < Min.x) Min.x = p.x;
			if (p.y > Max.y) Max.y = p.y;
			if (p.y < Min.y) Min.y = p.y;
        }

		Min.x = Mathf.FloorToInt(Min.x);
		Min.y = Mathf.FloorToInt(Min.y);
		Max.x = Mathf.CeilToInt(Max.x);
		Max.y = Mathf.CeilToInt(Max.y);
        
        return new Rect(Min.x, Min.y, Max.x - Min.x, Max.y - Min.y);
	}

}
