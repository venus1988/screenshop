using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlerBelt : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	public  GameObject belt;

	bool ImageNotLoaded = true;
	public GameObject uiControllerObject;
	 void Update(){
		if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count > 0 && ImageNotLoaded) {
			ImageNotLoaded = false;
			Sprite S = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[0];
			this.GetComponent<Image> ().sprite = S;
			
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
		if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count > 1) {
			belt = (GameObject) GameObject.Instantiate(Resources.Load("Belt"));

			print ("=:::::::::::::::::::::::::: " + GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded [0]);
			Sprite S = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[0];
			print("===== >"+this.gameObject.GetComponent<Image> ().sprite);
			belt.GetComponent<SpriteRenderer> ().sprite = S;

			Vector3 temp = S.bounds.size;
			belt.GetComponent<BoxCollider>().size = temp;
			belt.GetComponent<SpriteRenderer> ().sprite = S;

		}

		//GameObject belt = Instantiate (Shirt) as GameObject;
		//belt.transform.SetParent (startParent.transform);
		belt.transform.position = startPosition;
		itemBeingDragged = belt;


	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		belt.transform.position = new Vector3(temp.x,temp.y,-3.0f);
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag(PointerEventData eventData)
	{

		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		//if (belt.transform.parent != startParent) {
			//Debug.Log ("Inside Drop");
			//Instantiate(belt,startPosition,Quaternion.identity);
			//belt.transform.position = startPosition;
		//} else {
			//belt.transform.position = startPosition;
		//}
	}

	#endregion

}
