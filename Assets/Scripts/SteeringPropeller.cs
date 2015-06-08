using UnityEngine;
using System.Collections;

public class SteeringPropeller : Steering {
	protected GameObject[] propellerBlade;
	private float hoverSpeed;
	private float propelSpeed;
	
	protected override void Start(){
		base.Start();
		Tag_PropellerBlade[] propBlades = transform.GetComponentsInChildren<Tag_PropellerBlade>();
		propellerBlade = new GameObject[propBlades.Length];
		int i = 0;
		foreach(Tag_PropellerBlade t in propBlades){
			propellerBlade[i] = t.gameObject;
			i++;
		}

		steerForce = 200f;
		hoverSpeed = 200f;
		propelSpeed = 800f;
	}
	
	protected override void Update () {
		base.Update();
		foreach(GameObject g in propellerBlade){
			if(steering)
				g.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * propelSpeed);
			else
				g.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * hoverSpeed);
		}
	}
}
