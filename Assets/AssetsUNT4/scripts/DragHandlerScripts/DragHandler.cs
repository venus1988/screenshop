using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	public static GameObject belt;

	#region IBeginDragHandler implementation

	public void OnBeginDrag(PointerEventData eventData)
	{
		//Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);
		//itemBeingDragged = Instantiate(Resources.Load("Belt"),pos ,Quaternion.identity) as GameObject;
		//itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		belt = (GameObject) GameObject.Instantiate(Resources.Load("Neck1"));
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
