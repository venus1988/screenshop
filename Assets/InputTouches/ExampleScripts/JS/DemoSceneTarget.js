#pragma strict

public var minX:float=-5;
public var maxX:float=8;
public var minY:float=-4;
public var maxY:float=4;

public var hitEffect:ParticleSystem;

//called when hit
function OnTriggerEnter(){
	//place the hitEffect at the object position and assign a random color to it
	hitEffect.transform.position=transform.position;
	hitEffect.startColor=Color(Random.Range(0.0, 1.0), Random.Range(0.0, 1.0), Random.Range(0.0f, 1.0));
	//emit a set number of particle
	hitEffect.Emit(30);
	
	yield;
	
	//place the target at a new position
	var pos:Vector3=Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
	transform.position=pos;
}