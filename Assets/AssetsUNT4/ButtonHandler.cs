using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	
	public void ActivateImage()
	{
		if (gameObject.activeSelf == false)
		{
			gameObject.SetActive (true);
		} else if (gameObject.activeSelf == true) 
		{
			gameObject.SetActive(false);
		}
		
	}
}
