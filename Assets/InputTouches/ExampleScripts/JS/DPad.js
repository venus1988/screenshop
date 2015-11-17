#pragma strict

	public var up:GUITexture;
	public var down:GUITexture;
	public var left:GUITexture;
	public var right:GUITexture;
	
	public var controlObject:Transform;
	
	// Use this for initialization
	function Start () {
	
	}
	
	function OnEnable(){
		IT_Gesture.onMouse1E += Pressed;
		IT_Gesture.onTouchPosE += Pressed;
	}
	
	function OnDisable(){
		IT_Gesture.onMouse1E -= Pressed;
		IT_Gesture.onTouchPosE -= Pressed;
	}
	
	// Update is called once per frame
	function Update () {
		//limit the position of the control object
		var x:float=controlObject.position.x;
		var z:float=controlObject.position.z;
		
		x=Mathf.Clamp(x, -5, 5);
		z=Mathf.Clamp(z, -5, 5);
		
		controlObject.position=Vector3(x, controlObject.position.y, z);
	}
	
	//call when the screen is touched/clicked
	function Pressed(pos:Vector2){
		
		//set a zero vector
		var moveDir:Vector3=Vector3.zero;
		
		//if any of the button is pressed, set the corresponding  move direction
		if(up.HitTest(pos)){
			moveDir+=Vector3(0, 0, 1);
		}
		if(down.HitTest(pos)){
			moveDir+=Vector3(0, 0, -1);
		}
		if(left.HitTest(pos)){
			moveDir+=Vector3(-1, 0, 0);
		}
		if(right.HitTest(pos)){
			moveDir+=Vector3(1, 0, 0);
		}
		
		//normalized the total moveDir
		moveDir=moveDir.normalized;
		
		//move the controlObject according to the input move direction
		controlObject.Translate(moveDir*Time.deltaTime*3);
	}
	
	
	function OnGUI(){
		var title:String="DPad demo, click/touch the arrows on the bottom left corner to move the sphere.";
		GUI.Label(Rect(150, 15, 500, 40), title);
	}