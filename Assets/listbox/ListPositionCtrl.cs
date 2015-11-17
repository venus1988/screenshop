﻿/* Calculate the final position of ListBoxes.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListPositionCtrl : MonoBehaviour
{
	public static ListPositionCtrl Instance;
	public bool controlByButton = false;
	public bool alignToCenter = false;

	public ListBox[] listBoxes;
	public float centerPosY;

	public Button[] buttons;

	public int slidingFrames = 35;
	[Range( 0.0f, 1.0f )]
	public float slidingFactor = 0.2f;

	private bool isTouchingDevice;

	private Vector3 lastInputWorldPos;
	private Vector3 currentInputWorldPos;
	private Vector3 deltaInputWorldPos;

	void Awake()
	{
		Instance = this;

		switch( Application.platform )
		{
		case RuntimePlatform.WindowsEditor:
			isTouchingDevice = false;
			break;
		case RuntimePlatform.Android:
			isTouchingDevice = true;
			break;
		}
	}

	void Start()
	{
		if ( !controlByButton )
			foreach ( Button button in buttons )
				button.gameObject.SetActive( false );
	}

	void Update()
	{
		if (UIController._Instance.LoadingScreen.activeSelf)
			return;
		if ( !controlByButton )
		{
			if ( !isTouchingDevice )
				storeMousePosition();
			else
				storeFingerPosition();
		}
	}

	/* Store the position of mouse when the player clicks the left mouse button.
	 */
	void storeMousePosition()
	{
		if ( Input.GetMouseButtonDown(0) )
		{
			lastInputWorldPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		}
		else if ( Input.GetMouseButton(0) )
		{
			currentInputWorldPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			deltaInputWorldPos = new Vector3( 0.0f, currentInputWorldPos.y - lastInputWorldPos.y, 0.0f );
			foreach ( ListBox listbox in listBoxes )
			{
				if(listbox.gameObject.activeSelf)
				listbox.updatePosition( deltaInputWorldPos );
			}

			lastInputWorldPos = currentInputWorldPos;
		}
		else if ( Input.GetMouseButtonUp(0) )
			setSlidingEffect();
	}

	/* Store the position of touching on the mobile.
	 */
	void storeFingerPosition()
	{
		if ( Input.GetTouch(0).phase == TouchPhase.Began )
		{
			lastInputWorldPos = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).position );
		}
		else if ( Input.GetTouch(0).phase == TouchPhase.Moved )
		{
			currentInputWorldPos = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).position );
			deltaInputWorldPos = new Vector3( 0.0f, currentInputWorldPos.y - lastInputWorldPos.y, 0.0f );
			foreach ( ListBox listbox in listBoxes )
			{
				if(listbox.gameObject.activeSelf)
				listbox.updatePosition( deltaInputWorldPos );
			}

			lastInputWorldPos = currentInputWorldPos;
		}
		else if ( Input.GetTouch(0).phase == TouchPhase.Ended )
			setSlidingEffect();
	}

	/* If the touch is ended, calculate the distance to slide and
	 * assign to the listBoxes.
	 */
	void setSlidingEffect()
	{
		float deltaPos = deltaInputWorldPos.y;

		if ( alignToCenter )
			deltaPos = findDeltaPositionY();

		foreach( ListBox listbox in listBoxes )
		{
			if(listbox.gameObject.activeSelf)
			listbox.setSlidingDistance( deltaPos );
		}
	}

	/* Find the listBox which is the closest to the center y position,
	 * And calculate the delta y position between them.
	 */
	float findDeltaPositionY()
	{
		float minDeltaPosY = 99999.9f;
		float deltaPosY;

		foreach ( ListBox listBox in listBoxes )
		{
			if(!listBox.gameObject.activeSelf)
				return 0f;
			deltaPosY = centerPosY - listBox.transform.position.y;

			if ( Mathf.Abs( deltaPosY ) < Mathf.Abs( minDeltaPosY ) )
				minDeltaPosY = deltaPosY;
		}

		return minDeltaPosY;
	}

	/* controlByButton is enabled!
	 * When the next content button is pressed,
	 * move all listBoxes 1 unit up.
	 */
	public void nextContent()
	{
		foreach( ListBox listbox in listBoxes )
			if(listbox.gameObject.activeSelf)
			listbox.unitMove( 1, true );
	}

	/* controlByButton is enabled!
	 * When the last content button is pressed,
	 * move all listBoxes 1 unit down.
	 */
	public void lastContent()
	{
		foreach( ListBox listbox in listBoxes )
			if(listbox.gameObject.activeSelf)
			listbox.unitMove( 1, false );
	}
}
