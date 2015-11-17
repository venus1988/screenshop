#pragma strict

public var targetT:Transform;

private var dist:float;

private var orbitSpeedX:float;
private var orbitSpeedY:float;
private var zoomSpeed:float;

public var rotXSpeedModifier:float=0.25f;
public var rotYSpeedModifier:float=0.25f;
public var zoomSpeedModifier:float=5;

//public var minRotX:float=-20;
//public var maxRotX:float=70;


// Use this for initialization
function Start () {
	dist=transform.localPosition.z;
}

function OnEnable(){
	IT_Gesture.onDraggingE += OnDragging;
	//IT_Gesture.onMFDraggingE += OnMFDragging;
	
	IT_Gesture.onPinchE += OnPinch;
}

function OnDisable(){
	IT_Gesture.onDraggingE -= OnDragging;
	//IT_Gesture.onMFDraggingE -= OnMFDragging;
	
	IT_Gesture.onPinchE -= OnPinch;
}


// Update is called once per frame
function Update () {
	
	targetT.Rotate(new Vector3(-orbitSpeedX, -orbitSpeedY, 0), Space.World);
	
	transform.parent.position=targetT.position;
	
	//calculate the zoom and apply it
	dist+=Time.deltaTime*zoomSpeed*0.01;
	dist=Mathf.Clamp(dist, -15, -3);
	transform.localPosition=new Vector3(0, 0, dist);
	
	//reduce all the speed
	orbitSpeedX*=(1-Time.deltaTime*12);
	orbitSpeedY*=(1-Time.deltaTime*3);
	zoomSpeed*=(1-Time.deltaTime*4);
	
	//use mouse scroll wheel to simulate pinch, sorry I sort of cheated here
	zoomSpeed+=Input.GetAxis("Mouse ScrollWheel")*500*zoomSpeedModifier;
}

//called when one finger drag are detected
function OnDragging(dragInfo:DragInfo){
	//apply the DPI scaling
	dragInfo.delta/=IT_Gesture.GetDPIFactor();
	//vertical movement is corresponded to rotation in x-axis
	orbitSpeedX=-dragInfo.delta.y*rotXSpeedModifier;
	//horizontal movement is corresponded to rotation in y-axis
	orbitSpeedY=dragInfo.delta.x*rotYSpeedModifier;
}

//called when pinch is detected
function OnPinch(pinfo:PinchInfo){
	zoomSpeed-=pinfo.magnitude*zoomSpeedModifier/IT_Gesture.GetDPIFactor();
}



private var instruction:boolean=false;
function OnGUI(){
	var title:String="RTS camera, the camera will orbit around a pivot point but the rotation in z-axis is locked.";
	GUI.Label(Rect(150, 10, 400, 60), title);
	
	if(!instruction){
		if(GUI.Button(Rect(10, 55, 130, 35), "Instruction On")){
			instruction=true;
		}
	}
	else{
		if(GUI.Button(Rect(10, 55, 130, 35), "Instruction Off")){
			instruction=false;
		}
		
		GUI.Box(Rect(10, 100, 400, 100), "");
		
		var instInfo:String="";
		instInfo+="- swipe or drag on screen to rotate the camera\n";
		instInfo+="- pinch or using mouse wheel to zoom in/out\n";
		instInfo+="- swipe or drag on screen with 2 fingers to move around\n";
		instInfo+="- single finger interaction can be simulate using left mosue button\n";
		instInfo+="- two fingers interacion can be simulate using right mouse button";
		
		GUI.Label(Rect(15, 105, 390, 90), instInfo);
	}
}