using UnityEngine;
using System.Collections;

public class SwipeController : MonoBehaviour 
{

	private float fingerStartTime = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	private Vector2 fingerEndPos = Vector2.zero;
	private bool isSwipe = false;
	private float minSwipeDist = 50.0f;
	private float maxSwipeTime = 0.5f;

	public delegate void swipe(Gesture type);
	public static event SwipeController.swipe onSwipe;

	public  bool EnableSwipe=false;


	public enum Gesture
	{
		SWIPE_UP,
		SWIPE_DOWN,
		SWIPE_RIGHT,
		SWIPE_LEFT,
	}
	private Gesture Gesture_Type;


	void Update () 
	{
		if (!EnableSwipe)
			return;

		if (Input.GetMouseButtonDown (0)) 
		{
			isSwipe = true;
			fingerStartTime = Time.time;
			fingerStartPos =new Vector3(Input.mousePosition.x,Input.mousePosition.y);
		}
		if (Input.GetMouseButtonUp (0))
		{
			fingerEndPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y);

			float gestureTime = Time.time - fingerStartTime;
			float gestureDist = (fingerEndPos - fingerStartPos).magnitude;
		
			if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) 
			{
				Vector2 direction = fingerEndPos - fingerStartPos;
				Vector2 swipeType = Vector2.zero;
			//	Debug.Log(direction);

				if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) 
				{
					// the swipe is horizontal:

					swipeType = Vector2.right * Mathf.Sign (direction.x);
				}
				else 
				{
					// the swipe is vertical:
					swipeType = Vector2.up * Mathf.Sign (direction.y);
				}
			
				if (swipeType.x != 0.0f) 
				{
					if (swipeType.x > 0.0f) 
					{
						// MOVE RIGHT
						//Debug.Log ("Move Right");
						onSwipe(Gesture.SWIPE_RIGHT);
					} 
					else 
					{
						// MOVE LEFT
						//Debug.Log ("Move Left");
						onSwipe(Gesture.SWIPE_LEFT);
					}
				}

				if (swipeType.y != 0.0f) 
				{
					if (swipeType.y > 0.0f) 
					{
						// MOVE UP
						//Debug.Log ("Move UP");
						onSwipe(Gesture.SWIPE_UP);
					}
					else 
					{
						// MOVE DOWN
						//Debug.Log ("Move Down");
						onSwipe(Gesture.SWIPE_DOWN);
					}
				}
			}
			isSwipe = false;
		}  //End Mousebutton up
	}
}
