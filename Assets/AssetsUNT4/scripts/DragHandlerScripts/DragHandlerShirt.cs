using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlerShirt : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	public static GameObject belt;

	bool ImageNotLoaded = true;
	public GameObject uiControllerObject;

	//AddNewElement


	void Start()
	{

		//PlayerPrefs.DeleteAll ();

		if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count >= 1)
		{
			if (PlayerPrefs.GetInt ("ArrayCountSize_1") >= (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count - 1))
			{
				print ("pref Array sizen is: " + PlayerPrefs.GetInt ("ArrayCountSize_1"));
			}

		}
		//print("start{)"+GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count);
		//print ("=> " + PlayerPrefs.GetString ("ImagesLoaded"));
	}

	void Update()
	{
		if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count >= 1 && PlayerPrefs.GetString ("ImagesLoaded") == "yes")
		{
			//Sprite S = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[1];
			//this.GetComponent<Image> ().sprite = S;
			print ("our first update");
			if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count >= 1 ) 
			{	
				if (PlayerPrefs.GetString ("forFirstTime") == "no")	
				{
					print ("UPDATE============================== 1");
				}else
					{
						GameObject.FindWithTag ("panel").GetComponent<DynamicScrollView> ().noOfItems = (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count) ;
						PlayerPrefs.SetString ("forFirstTime","no");
						GameObject.FindWithTag ("panel").GetComponent<DynamicScrollView> ().InitializeList();
						PlayerPrefs.SetInt ("ArrayCountSize_1", GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count - 1);
					print ("UPDATE============================== 2"+ GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count);

						for(int i=0;i<GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count;i++)
						{
							
							print ("UPDATE============================== 3"+GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[i]);
							GameObject.Find(i.ToString()).GetComponent<Image>().sprite = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[i];
						}
					}
			
				PlayerPrefs.SetString ("ImagesLoaded","no");
				//for(int i=0;i<GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count;i++){
					
			}



			//AddNewElement
		}
	}


	#region IBeginDragHandler implementation

	public void OnBeginDrag(PointerEventData eventData)
	{
		//Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);
		//itemBeingDragged = Instantiate(Resources.Load("Belt"),pos ,Quaternion.identity) as GameObject;
		//itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		if (PlayerPrefs.GetString ("sep_enable" + name)=="true")
		if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count >= 1) 
		{
			belt = (GameObject) GameObject.Instantiate(Resources.Load("Shirt"));
			belt.name = name + "0";

			print ("========= " + this.gameObject.name);

			Sprite S = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[int.Parse(name)];
			GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().Anim_ScrollPanel.Play("AnimScrollOFF");

			//print("===== >"+this.gameObject.GetComponent<Image> ().sprite);

			Vector3 temp = S.bounds.size;
			belt.GetComponent<BoxCollider>().size = temp;
			belt.GetComponent<SpriteRenderer> ().sprite = S;

			//gameObject.SetActive(true);
			//GameObject.Find(name).SetActive(true);
			print(gameObject.name);


		}
		//GameObject belt = Instantiate (Shirt) as GameObject;
		//belt.transform.SetParent (startParent.transform);
		if(belt) belt.transform.position = startPosition;
		itemBeingDragged = belt;

	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if( belt )belt.transform.position = new Vector3(temp.x,temp.y,-3.0f);
		print (temp);
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag(PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Destroy (GameObject.Find(name));
		Destroy (GameObject.Find (name + ".5"));
		PlayerPrefs.SetString("sep_enable"+name,"false");
		GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().Anim_ScrollPanel.Play("AnimScrollON");
		GameObject.FindWithTag ("panel").GetComponent<DynamicScrollView> ().SetContentHeight ();

		//if (belt.transform.parent != startParent)
		//{
			//Debug.Log ("Inside Drop");
			//Instantiate(belt,startPosition,Quaternion.identity);
			//belt.transform.position = startPosition;
		//} else 
			//{
				//belt.transform.position = startPosition;
			//}
	}

	#endregion

}
