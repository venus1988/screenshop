#pragma strict

public var center:Vector3=Vector3.zero;
public var dist:float;

private var orbitSpeed:Vector2;
private var rotateSpeed:float;
private var zoomSpeed:float;

public var orbitSpeedModifier:float=1;
public var zoomSpeedModifier:float=5;
public var rotateSpeedModifier:float=1;


// Use this for initialization
function Start () {
	dist=transform.position.z;
}

function OnEnable(){
	IT_Gesture.onDraggingE += OnDragging;
	IT_Gesture.onRotateE += OnRotate;
	IT_Gesture.onPinchE += OnPinch;
}

function OnDisable(){
	IT_Gesture.onDraggingE -= OnDragging;
	IT_Gesture.onRotateE += OnRotate;
	IT_Gesture.onPinchE += OnPinch;
}

// Update is called once per frame
function Update () {
	//calculate the distance from center based on zoomSpeed
	dist+=Time.deltaTime*zoomSpeed*0.01;
	//clamp the distance so it's within permmited range
	dist=Mathf.Clamp(dist, -15, -3);
	
	//set the camera rotation back to center, so we can calculate things from this point
	transform.position=center;
	//apply the rotation based on the current orbit and rotate speed
	transform.rotation*=Quaternion.Euler(-orbitSpeed.y, orbitSpeed.x, rotateSpeed);
	//apply the distance
	transform.position=transform.TransformPoint(Vector3(0, 0, dist));
	
	//slow down all the speed
	orbitSpeed*=(1-Time.deltaTime*3);
	rotateSpeed*=(1-Time.deltaTime*4f);
	zoomSpeed*=(1-Time.deltaTime*4);
	
	//use mouse scroll wheel to simulate pinch, sorry I sort of cheated here
	zoomSpeed+=Input.GetAxis("Mouse ScrollWheel")*500*zoomSpeedModifier;
}

function OnDragging(dragInfo:DragInfo){
	orbitSpeed=dragInfo.delta*orbitSpeedModifier/IT_Gesture.GetDPIFactor();
}

function OnRotate(rinfo:RotateInfo){
	rotateSpeed=Mathf.Lerp(rotateSpeed, rinfo.magnitude*rotateSpeedModifier/IT_Gesture.GetDPIFactor(), 0.75);
}

function OnPinch(pinfo:PinchInfo){
	zoomSpeed-=pinfo.magnitude*zoomSpeedModifier/IT_Gesture.GetDPIFactor();
}


private var instruction:boolean=false;
function OnGUI(){
	var title:String="free orbit camera, the camera will orbit around the object";
	GUI.Label(Rect(150, 15, 500, 40), title);
	
	if(!instruction){
		if(GUI.Button(Rect(10, 55, 130, 35), "Instruction On")){
			instruction=true;
		}
	}
	else{
		if(GUI.Button(Rect(10, 55, 130, 35), "Instruction Off")){
			instruction=false;
		}
		
		GUI.Box(Rect(10, 100, 400, 80), "");
		
		var instInfo:String="";
		instInfo+="- swipe or drag on screen to rotate the camera in x and y-axis\n";
		instInfo+="- pinch or using mouse wheel to zoom in/out\n";
		instInfo+="- rotate two fingers on screen to rotate the camera in z-axis\n";
		instInfo+="- single finger interaction can be simulate using left mosue button\n";
		
		GUI.Label(Rect(15, 105, 390, 70), instInfo);
	}
}