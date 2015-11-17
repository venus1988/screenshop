#pragma strict

public var cursorIndicator:Transform;
public var swipeIndicator:Transform;
public var projectileObject:Transform;

private var body:Rigidbody;

public var label:GUIText;
private var labelTimer:float=-1;

function Start(){
	body=projectileObject.gameObject.GetComponent(Rigidbody);
}

function OnEnable(){
	IT_Gesture.onTouchPosE += OnOn;
	IT_Gesture.onMouse1E += OnOn;
	IT_Gesture.onSwipeE += OnSwipe;
}

function OnDisable(){
	IT_Gesture.onTouchPosE -= OnOn;
	IT_Gesture.onMouse1E -= OnOn;
	IT_Gesture.onSwipeE -= OnSwipe;
}

/*
//to differentiate swipe direction
function OnSwipe(sw:SwipeInfo){
	if(sw.angle>45 && sw.angle<=135){
		Debug.Log("Swipe Up");
		//insert your own code here
	}
	else if(sw.angle>135 && sw.angle<=225){
		Debug.Log("Swipe left");
		//insert your own code here
	}
	else if(sw.angle>225 && sw.angle<=315){
		Debug.Log("Swipe down");
		//insert your own code here
	}
	else{
		Debug.Log("Swipe right");
		//insert your own code here
	}
}
*/

function OnSwipe(sw:SwipeInfo){
	//position the projectile object at the start point of the swipe
	var p:Vector3=Camera.main.ScreenToWorldPoint(Vector3(sw.startPoint.x, sw.startPoint.y, 35));
	projectileObject.position=p;
	
	//clear the projectile current velocity before apply a new force in the swipe direction, take account of the swipe speed
	body.velocity=Vector3(0, 0, 0);
	var speed:float=Mathf.Min(sw.speed*0.0035/IT_Gesture.GetDPIFactor(), 15);
	//apply the DPI scaling
	sw.direction/=IT_Gesture.GetDPIFactor();
	body.AddForce(Vector3(sw.direction.x, 0, sw.direction.y) * speed);
	
	//show the swipe info 
	var labelText:String="Swipe Detected, ";
	if(sw.isMouse) labelText+="mouse "+sw.index.ToString()+"\n\n";
	else labelText+="finger "+sw.index.ToString()+"\n\n";
	
	//labelText+="\n\n";
	labelText+="direction: "+sw.direction+"\n";
	labelText+="angle: "+sw.angle.ToString("f1")+"\n";
	labelText+="speed: "+sw.speed.ToString("f1")+"\n";
	label.text=labelText;
	
	//if the label is previous cleared, re-initiate the coroutine to clear it
	if(labelTimer<0){
		StartCoroutine(ClearLabel());
	}
	//else just extend the timer
	else labelTimer=5;
	
	StartCoroutine(ShowSwipeIndicator(sw));
}

function ShowSwipeIndicator(sw:SwipeInfo){
	//position the projectile object at the start point of the swipe
	var p:Vector3=Camera.main.ScreenToWorldPoint(Vector3(sw.startPoint.x, sw.startPoint.y, 5));
	swipeIndicator.position=p;
	
	var t:float=0;
	while(t<=1){
		p=Vector2.Lerp(sw.startPoint, sw.endPoint, t);
		p=Camera.main.ScreenToWorldPoint(Vector3(p.x, p.y, 5));
		//Debug.Log(sw.startPoint+"  "+sw.endPoint+"   "+p);
		swipeIndicator.position=p;
		t+=0.2;
		yield;
	}
}

//clear the lable, if no new input has been assigned within 5 sec, the label will be cleared
function ClearLabel(){
	labelTimer=5;
	while(labelTimer>0){
		labelTimer-=Time.deltaTime;
		yield;
	}
	label.text="";
}


//called when the touch or a click is detected
function OnOn(pos:Vector2){
	//place the curose at the position where the tap/click take place
	var p:Vector3=Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 5));
	cursorIndicator.position=p;
}


private var instruction:boolean=false;
function OnGUI(){
	var title:String="swipe demo, use swipe to throw the ball object in the scene";
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
		
		GUI.Box(Rect(10, 100, 250, 50), "");
		
		GUI.Label(Rect(15, 105, 260, 40), "swipe on screen with 1 finger.\ncan use mouse to simulate that ");
	}
}