using UnityEngine;
using System.Collections;

public class _InitializeScrollView : MonoBehaviour {
	bool ImageNotLoaded = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	
		/*if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count > 1 && ImageNotLoaded) {
			ImageNotLoaded = false;

			print ( "  ================" + GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count);
			GameObject.FindWithTag ("panel").GetComponent<DynamicScrollView> ().noOfItems = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count ;
			GameObject.FindWithTag ("panel").GetComponent<DynamicScrollView> ().InitializeList();
			if (GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded.Count > 1 ) 
			{
				for(int i=0;i<4;i++)
					GameObject.Find(i.ToString()).GetComponent<Image>().sprite = GameObject.FindWithTag ("uicontroller").GetComponent<UIController> ().tex_downloaded[i];
			}
		}*/
	}
}
