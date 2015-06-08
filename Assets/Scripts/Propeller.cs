using UnityEngine;
using System.Collections;

public class Propeller : Propulsion {
	protected GameObject[] propellerBlade;
	private float hoverSpeed;
	private float propelSpeed;
	
	protected override void Start(){
		base.Start();
		propelForce = 2000f;
		hoverSpeed = 200f;
		propelSpeed = 800f;
	}

	protected override void Update () {
		base.Update();
		foreach(GameObject g in propellerBlade){
			if(propelling)
				g.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * propelSpeed);
			else
				g.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * hoverSpeed);
		}
	}

}
