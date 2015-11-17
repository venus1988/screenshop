using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float yMin,yMax;
}

public class HandleScroll : MonoBehaviour {

	public float speed = 0.1F;
	public GameObject rb;
	public Boundary bound;


	void Update() {
		if (Input.touchCount ==1  && Input.GetTouch(0).phase == TouchPhase.Moved ) {
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			Vector2 touchPosition= Input.GetTouch(0).position;

			
			// Move object across XY plane
			if(touchPosition.x>800)
			{
			rb.transform.Translate(0, touchDeltaPosition.y * speed, 0);
			}
			rb.transform.position=new Vector2
				(
					2.1f,
					Mathf.Clamp(rb.transform.position.y,bound.yMin,bound.yMax)
				);
		}
	}
}
