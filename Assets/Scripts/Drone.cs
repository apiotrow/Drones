using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drone : MovingObject {
	protected Propulsion[] propulsion;
	protected Weapon[] weapon;

	protected override void Start () {
		base.Start();
		setUpPropulsion();
	}

	protected override void Update(){
		base.Update();
		checkForInputs();
	}

	private void checkForInputs(){
		checkPropulsionInput();
	}

	private void checkPropulsionInput(){
		if(Input.GetKey(KeyCode.Space)){
			initiatePropulsion();
		}else if(Input.GetKeyUp(KeyCode.Space)){
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

	protected new void setUpPropulsion(){
		propulsion = GetComponentsInChildren<Propulsion>();
	}

}
