using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class playerMover : MonoBehaviour
{
		
	public Rigidbody avatar;

	public bool leftButtonHeld,rightButtonHeld;

	private float startingRotattionPoint;

	void Start ()
	{

		leftButtonHeld = false;

		rightButtonHeld = false;

		avatar = GetComponent<Rigidbody>();
	
		startingRotattionPoint = 180.0f;

	}

	public void LeftRelease()
	{

		leftButtonHeld = false;

	}

	public void LeftPressed(){

		leftButtonHeld = true;

	}

	public void RightRelease(){

		rightButtonHeld = false;
	}

	public void RightPressed(){

		rightButtonHeld = true;

	}
	
	void Update () {

		if(leftButtonHeld){

			rotateAntiClockWise();
		
		}else
		if(rightButtonHeld){

			rotateClockWise();

		}
	}
	

	public void rotateAntiClockWise()
	{

		float tempRotation = avatar.rotation.y + startingRotattionPoint;

		Quaternion rotation = Quaternion.Euler(0,tempRotation,0);

		if(tempRotation>360 ||tempRotation<0){

			tempRotation = 0.0f;

		}

		startingRotattionPoint = startingRotattionPoint + 1.0f ;

		avatar.rotation = rotation;

	}

	public void rotateClockWise(){
			
		float tempRotation = avatar.rotation.y + startingRotattionPoint;

		Quaternion rotation = Quaternion.Euler(0,tempRotation,0);

		if(tempRotation>360 || tempRotation<0){
			
			tempRotation = 0.0f;
			
		}
		
		startingRotattionPoint = startingRotattionPoint - 1.0f ;

		avatar.rotation = rotation;

	}

	IEnumerator Wait(){

		for(int i=0;i<=3;i++){
	
			rotateClockWise();

			yield return new WaitForSeconds(0.5f);

		}

	}

	IEnumerator Wait1(){
		
		for(int i=0;i<=3;i++){
			
			rotateAntiClockWise();

			yield return new WaitForSeconds(0.5f);
			
		}
		
	}
	/*
	public void rotateLeftContinuous(){
	
		if(!leftButtonHeld){

			leftButtonHeld = true;

			StartCoroutine(Wait());

			leftButtonHeld = false;

		}

	}

	public void rotateRightContinuous(){

		if(!rightButtonHeld){
			
			rightButtonHeld = true;
			
			StartCoroutine(Wait1());
			
			rightButtonHeld = false;
			
		}

	}
*/
	public void backButton(){

		//Application.LoadLevel ("startScreen");

	}
}
