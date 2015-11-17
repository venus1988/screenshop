#pragma strict

public var Indicator:ParticleSystem;
	
public var shortTapObj:Transform;
public var longTapObj:Transform;
public var doubleTapObj:Transform;
public var chargeObj:Transform;

public var chargeTextMesh:TextMesh;

public var dragObj1:Transform;
public var dragTextMesh1:TextMesh;

public var dragObj2:Transform;
public var dragTextMesh2:TextMesh;

function Start () {

}

function OnEnable(){
	IT_Gesture.onMultiTapE += OnMultiTap;
	IT_Gesture.onLongTapE += OnLongTap;
	
	IT_Gesture.onChargingE += OnCharging;
	IT_Gesture.onChargeEndE += OnChargeEnd;
	
	IT_Gesture.onDraggingStartE += OnDraggingStart;
	IT_Gesture.onDraggingE += OnDragging;
	IT_Gesture.onDraggingEndE += OnDraggingEnd;
}

function OnDisable(){
	IT_Gesture.onMultiTapE -= OnMultiTap;
	IT_Gesture.onLongTapE -= OnLongTap;
	
	IT_Gesture.onChargingE -= OnCharging;
	IT_Gesture.onChargeEndE -= OnChargeEnd;
	
	IT_Gesture.onDraggingStartE -= OnDraggingStart;
	IT_Gesture.onDraggingE -= OnDragging;
	IT_Gesture.onDraggingEndE -= OnDraggingEnd;
}

/*
public Transform obj;
function OnEnable(){
	IT_Gesture.onTouchDownE += OnTouchDown;
	IT_Gesture.onTouchUpE += OnTouchUp;
	IT_Gesture.onDraggingE += OnDragging;
}
function OnEnable(){
	IT_Gesture.onTouchDownE -= OnTouchDown;
	IT_Gesture.onTouchUpE -= OnTouchUp;
	IT_Gesture.onDraggingE -= OnDragging;
}

function OnTouchDown(Vector2 pos){
	var ray:Ray = Camera.main.ScreenPointToRay(pos);
	var hit:RaycastHit;
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		obj=hit.transform;
		obj.GetComponent(drag-rigidbody).OnMouseDown();
	}
}

function OnTouchUp(Vector2 pos){
	var ray:Ray = Camera.main.ScreenPointToRay(pos);
	var hit:RaycastHit;
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		obj=null;
		obj.GetComponent(drag-rigidbody).OnMouseUp();
	}
}

function OnDragging(dragInfo:DragInfo){
	if(obj!=null) obj.GetComponent(drag-rigidbody).FixedUpdate(dragInfo.pos);
}
*/

//called when a multi-Tap event is detected
function OnMultiTap(tap:Tap){
	//do a raycast base on the position of the tap
	var ray:Ray = Camera.main.ScreenPointToRay(tap.pos);
	var hit:RaycastHit;
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		//if the tap lands on the shortTapObj, then shows the effect.
		if(hit.collider.transform==shortTapObj){
			//place the indicator at the object position and assign a random color to it
			Indicator.transform.position=shortTapObj.position;
			Indicator.startColor=GetRandomColor();
			//emit a set number of particle
			Indicator.Emit(30);
		}
		//if the tap lands on the doubleTapObj
		else if(hit.collider.transform==doubleTapObj){
			//check to make sure if the tap count matches
			if(tap.count==2){
				//place the indicator at the object position and assign a random color to it
				Indicator.transform.position=doubleTapObj.position;
				Indicator.startColor=GetRandomColor();
				//emit a set number of particle
				Indicator.Emit(30);
			}
		}
	}
}



//called when a long tap event is ended
function OnLongTap(tap:Tap){
	//do a raycast base on the position of the tap
	var ray:Ray = Camera.main.ScreenPointToRay(tap.pos);
	var hit:RaycastHit;
	//if the tap lands on the longTapObj
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		if(hit.collider.transform==longTapObj){
			//place the indicator at the object position and assign a random color to it
			Indicator.transform.position=longTapObj.position;
			Indicator.startColor=GetRandomColor();
			//emit a set number of particle
			Indicator.Emit(30);
		}
	}
}

//called when a double tap event is ended
//this event is used for onDoubleTapE in v1.0, it's still now applicabl
function OnDoubleTap(pos:Vector2){
	var ray:Ray = Camera.main.ScreenPointToRay(pos);
	var hit:RaycastHit;
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		if(hit.collider.transform==doubleTapObj){
			//place the indicator at the object position and assign a random color to it
			Indicator.transform.position=doubleTapObj.position;
			Indicator.startColor=GetRandomColor();
			//emit a set number of particle
			Indicator.Emit(30);
		}
	}
}


//called when a charging event is detected
function OnCharging(cInfo:ChargedInfo){
	var ray:Ray = Camera.main.ScreenPointToRay(cInfo.pos);
	var hit:RaycastHit;
	//use raycast at the cursor position to detect the object
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		if(hit.collider.transform==chargeObj){
			//display the charged percentage on screen
			chargeTextMesh.text="Charging "+(cInfo.percent*100).ToString("f1")+"%";
		}
	}
}

//called when a charge event is ended
function OnChargeEnd(cInfo:ChargedInfo){
	var ray:Ray = Camera.main.ScreenPointToRay(cInfo.pos);
	var hit:RaycastHit;
	//use raycast at the cursor position to detect the object
	if(Physics.Raycast(ray, hit, Mathf.Infinity)){
		if(hit.collider.transform==chargeObj){
			//place the indicator at the object position and assign a random color to it
			Indicator.transform.position=chargeObj.position;
			Indicator.startColor=GetRandomColor();
			
			//adjust the indicator speed with respect to the charged percent
			Indicator.startSpeed=1+3*cInfo.percent;
			//emit a set number of particles with respect to the charged percent
			Indicator.Emit(10+cInfo.percent*75);
			
			//reset the particle speed, since it's shared by other event
			StartCoroutine(ResumeSpeed());
		}
	}
	chargeTextMesh.text="HoldToCharge";
}

//reset the particle emission speed of the indicator
function ResumeSpeed(){
	yield WaitForSeconds(Indicator.startLifetime);
	Indicator.startSpeed=2;
}

private var currentDragIndex1:int=-1;
private var currentDragIndex2:int=-1;
function OnDraggingStart(dragInfo:DragInfo){
	//currentDragIndex=dragInfo.index;
	
	//if(currentDragIndex==-1){
		var ray:Ray = Camera.main.ScreenPointToRay(dragInfo.pos);
		var hit:RaycastHit;
		//use raycast at the cursor position to detect the object
		if(Physics.Raycast(ray, hit, Mathf.Infinity)){
			//if the drag started on dragObj1
			if(hit.collider.transform==dragObj1){
				//change the scale of dragObj1, give the user some visual feedback
				dragObj1.localScale*=1.1;
				//latch dragObj1 to the cursor, based on the index
				Obj1ToCursor(dragInfo);
				currentDragIndex1=dragInfo.index;
			}
			//if the drag started on dragObj2
			else if(hit.collider.transform==dragObj2){
				//change the scale of dragObj2, give the user some visual feedback
				dragObj2.localScale*=1.1;
				//latch dragObj2 to the cursor, based on the index
				Obj2ToCursor(dragInfo);
				currentDragIndex2=dragInfo.index;
			}
		}
	//}
}

//triggered on a single-finger/mouse dragging event is on-going
function OnDragging(dragInfo:DragInfo){
	//if the dragInfo index matches dragIndex1, call function to position dragObj1 accordingly
	if(dragInfo.index==currentDragIndex1){
		Obj1ToCursor(dragInfo);
	}
	//if the dragInfo index matches dragIndex2, call function to position dragObj2 accordingly
	else if(dragInfo.index==currentDragIndex2){
		Obj2ToCursor(dragInfo);
	}
}

//assign dragObj1 to the dragInfo position, and display the appropriate tooltip
function Obj1ToCursor(dragInfo:DragInfo){
	var p:Vector3=Camera.main.ScreenToWorldPoint(Vector3(dragInfo.pos.x, dragInfo.pos.y, 30));
	dragObj1.position=p;
	
	if(dragInfo.isMouse){
		dragTextMesh1.text="Dragging with mouse"+(dragInfo.index+1);
	}
	else{
		dragTextMesh1.text="Dragging with finger"+(dragInfo.index+1);
	}
}

//assign dragObj2 to the dragInfo position, and display the appropriate tooltip
function Obj2ToCursor(dragInfo:DragInfo){
	var p:Vector3=Camera.main.ScreenToWorldPoint(Vector3(dragInfo.pos.x, dragInfo.pos.y, 30));
	dragObj2.position=p;
	
	if(dragInfo.isMouse){
		dragTextMesh2.text="Dragging with mouse"+(dragInfo.index+1);
	}
	else{
		dragTextMesh2.text="Dragging with finger"+(dragInfo.index+1);
	}
}

function OnDraggingEnd(dragInfo:DragInfo){
	
	var p:Vector3;	//position of the dragObj
	
	//drop the dragObj being drag by this particular cursor
	if(dragInfo.index==currentDragIndex1){
		currentDragIndex1=-1;
		dragObj1.localScale*=10.0/11.0;
		
		p=Camera.main.ScreenToWorldPoint(Vector3(dragInfo.pos.x, dragInfo.pos.y, 30));
		dragObj1.position=p;
		dragTextMesh1.text="DragMe";
	}
	else if(dragInfo.index==currentDragIndex2){
		currentDragIndex2=-1;
		dragObj2.localScale*=10.0/11.0;
		
		p=Camera.main.ScreenToWorldPoint(Vector3(dragInfo.pos.x, dragInfo.pos.y, 30));
		dragObj2.position=p;
		dragTextMesh2.text="DragMe";
	}
	
}


//return a random color when called
private function GetRandomColor():Color{
	return Color(Random.Range(0.0, 1.0), Random.Range(0.0, 1.0), Random.Range(0.0, 1.0));
}


private var instruction:boolean=false;
function OnGUI(){
	if(!instruction){
		if(GUI.Button(Rect(10, 55, 130, 35), "Instruction On")){
			instruction=true;
		}
	}
	else{
		if(GUI.Button(Rect(10, 55, 130, 35), "Instruction Off")){
			instruction=false;
		}
		
		GUI.Box(Rect(10, 100, 200, 65), "");
		
		GUI.Label(Rect(15, 105, 190, 65), "interact with each object using the interaction stated on top of each of them");
	}
}