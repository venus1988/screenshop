using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSimple : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	
	#region IBeginDragHandler implementation
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		//Vector3 pos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);
		//itemBeingDragged = Instantiate(Resources.Load("Belt"),pos ,Quaternion.identity) as GameObject;
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		//belt = (GameObject) GameObject.Instantiate(Resources.Load("Belt"));
		//GameObject belt = Instantiate (Shirt) as GameObject;
		//belt.transform.SetParent (startParent.transform);
		//belt.transform.position = startPosition;
		//itemBeingDragged = belt;
		
		
	}
	
	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag(PointerEventData eventData)
	{
		
		transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y,-10.0f);
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag(PointerEventData eventData)
	{
		
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent != startParent) {
			Debug.Log ("Inside Drop");
			Instantiate(gameObject,startPosition,Quaternion.identity);
			transform.position = startPosition;
		} else {
			transform.position = startPosition;
		}
	}
	
	#endregion
	
}