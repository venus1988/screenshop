using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pointerHandler : MonoBehaviour {

	public Button leftRotateBtn;

	public Button rightRotateBtn;

	public bool _pressedLeftRotate;

	public bool _pressedRightRotate;

	void Start () {

		_pressedLeftRotate = false;

		_pressedRightRotate = false;

	}

	IEnumerator Wait(){

		yield return new WaitForSeconds(2.0f);

	}

	void Update () {

		if(_pressedLeftRotate){
			
			print ("rotate Left is pressed");

		}else
		if(_pressedRightRotate){
			
			print ("roate right is pressed");

		}

	}
	
	public void RotateAntiClock(){

		if(_pressedLeftRotate){

			_pressedLeftRotate  = false;

		}else

		if(!_pressedLeftRotate){

			_pressedLeftRotate = true;

		}

		if(_pressedLeftRotate){

			print ("left rotate is pressed");
		
		}else
		if(!_pressedLeftRotate){

			print("left rotate is not pressed");

		}

//		StartCoroutine (Wait());

	}

	public void RotateClock(){

		if(_pressedRightRotate){

			_pressedRightRotate = false;
		
		}else
		if(!_pressedRightRotate){

			_pressedRightRotate = true;

		}

		if(_pressedRightRotate){

			print ("right rotate is pressed");
		}else
		if(!_pressedRightRotate){

			print("right roate is not pressed");

		}
//		StartCoroutine (Wait());

	}
}
