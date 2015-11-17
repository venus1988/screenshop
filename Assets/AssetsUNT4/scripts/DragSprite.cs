using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using Prime31;
using Newtonsoft.Json;
using System.Linq;

using UnionAssets.FLE;
[System.Serializable]
public class BoundaryObject
{
	public float xMin,xMax,yMin,yMax;
}

public class DragSprite : MonoBehaviour 
{
	public BoundaryObject boundObject;
	Vector2 currentPos;
	public static GameObject itemBeingDragged = null;


	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			  if (Physics.Raycast (ray, out hit))
				{
					//hit.transform.GetComponent<GameObject>();
					
					itemBeingDragged=hit.collider.gameObject;
					
					//currentPos=hit.point;
					//hit.collider.transform.position =currentPos;
				}
			}
		if (Input.GetMouseButton(0))
			    {
					Vector3 temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					if(itemBeingDragged != null)
					{

					itemBeingDragged.transform.position = new Vector3(temp.x,temp.y,-3.0f);
					itemBeingDragged.transform.position=new Vector3
						( Mathf.Clamp(itemBeingDragged.transform.position.x,boundObject.xMin,boundObject.xMax),
						  Mathf.Clamp(itemBeingDragged.transform.position.y,boundObject.yMin,boundObject.yMax), -3.0f
						);
						
					}
				}
		if (Input.GetMouseButtonUp (0)) 
			{
				//print (Input.mousePosition);
				if(GameObject.Find ("Scrollbar"))
				if(Input.mousePosition.x > GameObject.Find ("Scrollbar").transform.position.x){
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				print(ray);
					if(Physics.Raycast (ray, out hit)) 
					{
						int temp = int.Parse(hit.collider.gameObject.name)/10;
						print ("---------------------print"+ temp);
						PlayerPrefs.SetString("sep_enable"+ temp,"true");
						Sprite sp = hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite;
				
							DestroyObject( hit.collider.gameObject);
							GameObject.FindWithTag ("panel").GetComponent<DynamicScrollView> ().AddElement(""+temp);
							GameObject.Find(""+temp).GetComponent<Image>().sprite = sp;
							//GameObject.Find(""+temp).GetComponent<Image>().sprite = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[temp];
						//GameObject.Find("" + temp).SetActive(true);
						//GameObject.Find(temp + ".5").SetActive(true);
					}
				}
				itemBeingDragged = null;
			}
	}	
}
