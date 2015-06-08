using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drone : MovingObject {
	protected Propulsion[] propulsion;
	protected Steering[] steering;
	protected Weapon[] weapon;
	private GameObject chassis;
	private bool userInputting;

	protected override void Start () {
		base.Start();
		setUpPropulsion();
		setUpSteering();
		chassis = transform.Find("Chassis").gameObject;
	}

	protected override void Update(){
		base.Update();
		checkForInputs();
		if(!userInputting)
			stabilizeDrone();
	}

	private void stabilizeDrone(){
//		if(chassis.transform.eulerAngles != Vector3.zero){
//
//		}
	}

	private void checkForInputs(){
		checkPropulsionInput();
		checkSteeringInput();
	}

	private void checkSteeringInput(){
		if(Input.GetKey(KeyCode.W)){
			initiateForwardSteering();
			userInputting = true;
		}else if(Input.GetKeyUp(KeyCode.W)){
			endForwardSteering();
			userInputting = false;
		}

		if(Input.GetKey(KeyCode.S)){
			initiateBackwardSteering();
			userInputting = true;
		}else if(Input.GetKeyUp(KeyCode.S)){
			endBackwardSteering();
			userInputting = false;
		}
	}

	private void initiateBackwardSteering(){
		foreach(Steering s in steering){
			if(s.direction == Steering.Direction.backward){
				s.steering = true;
				s.steer();
			}
		}
	}
	
	private void endBackwardSteering(){
		foreach(Steering s in steering){
			if(s.direction == Steering.Direction.backward){
				s.steering = false;
			}
		}
	}

	private void initiateForwardSteering(){
		foreach(Steering s in steering){
			if(s.direction == Steering.Direction.forward){
				s.steering = true;
				s.steer();
			}
		}
	}

	private void endForwardSteering(){
		foreach(Steering s in steering){
			if(s.direction == Steering.Direction.forward){
				s.steering = false;
			}
		}
	}

	private void checkPropulsionInput(){
		if(Input.GetKey(KeyCode.Space)){
			userInputting = true;
			initiatePropulsion();
		}else if(Input.GetKeyUp(KeyCode.Space)){
			userInputting = false;
			endPropulsion();
		}
	}

	private void initiatePropulsion(){
		foreach(Propulsion p in propulsion){
			p.propelling = true;
			p.propel();
		}
	}

	private void endPropulsion(){
		foreach(Propulsion p in propulsion){
			p.propelling = false;
		}
	}

	protected void setUpPropulsion(){
		propulsion = GetComponentsInChildren<Propulsion>();
	}

	protected void setUpSteering(){
		steering = GetComponentsInChildren<Steering>();
	}

}
