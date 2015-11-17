using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	private ButtonHandler btn;

	// Use this for initialization
	void Start () {
	
	}

	void OnButton()
	{
		btn.ActivateImage();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
