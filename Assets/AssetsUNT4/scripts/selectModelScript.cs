using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class selectModelScript : MonoBehaviour {

	public Toggle toggleSelect;


	void Start () {

		toggleSelect = GetComponent<Toggle>();



	}

	void Update () {

		if(toggleSelect.isOn){

			if(toggleSelect.tag.Equals("model1")){



			}else

			if(toggleSelect.tag.Equals("model2")){



			}

		}
		toggleSelect.isOn = false;
	}
}
