using UnityEngine;
using System.Collections;

public class ZoomHandler : MonoBehaviour 
{
	public float zoomSpeed = 0.02f;
	public static GameObject itemBeingZoomed;
	
	void Update()
	{
		if (Input.touchCount == 2) 
		{
				//hit.transform.GetComponent<GameObject>();
				//currentPos=hit.point;
				//hit.collider.transform.position =currentPos;
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

				if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
				{
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit))
					{
						itemBeingZoomed=hit.collider.gameObject;
					Debug.Log("dsf");
					}
				}
			if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
			{
			
			Vector2 touchZeroPreviousPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePreviousPos = touchOne.position - touchOne.deltaPosition;
			
			float prevTouchDeltaMag = (touchZeroPreviousPos - touchOnePreviousPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;
			if(itemBeingZoomed!=null)
			{
				itemBeingZoomed.transform.localScale = new Vector2(itemBeingZoomed.transform.localScale.x+deltaMagnitudediff * -zoomSpeed,itemBeingZoomed.transform.localScale.y+deltaMagnitudediff * -zoomSpeed);
			
				//obj.transform.localScale.x += deltaMagnitudediff * zoomSpeed;
			itemBeingZoomed.transform.localScale = new Vector2(Mathf.Max (itemBeingZoomed.transform.localScale.x, 0.25f),Mathf.Max (itemBeingZoomed.transform.localScale.y, 0.25f));
			itemBeingZoomed.transform.localScale = new Vector2(Mathf.Min (itemBeingZoomed.transform.localScale.x, 1.5f),Mathf.Min (itemBeingZoomed.transform.localScale.y, 1.5f));
				}
			}

			if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended)
			{
				itemBeingZoomed=null;
			}
		}
	}
}