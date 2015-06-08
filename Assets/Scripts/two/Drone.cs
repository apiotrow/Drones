using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace two
{
	public class Drone : MovingObject
	{
		protected Propeller[] propellers;
		private GameObject chassis;
		public bool userInputting;
		RotationDirection rotationDirection;
		private enum RotationDirection{
			left,
			right,
			forward,
			back,
			stable
		};
		float vel = 1f;

		protected override void Start ()
		{
			base.Start ();
			setUpPropellers ();
			chassis = transform.Find ("Chassis").gameObject;
		}

		protected override void Update ()
		{
			base.Update ();
			checkForInputs ();
			if (!userInputting){
				stabilizeDrone ();
				keepDroneAboveGround();
			}
		}

		private void keepDroneAboveGround(){
//			print (vel);
			if(distToGround() < 3f){
				initiatePropulsion(4000f / distToGround());
//				vel += 20f;
			}
//			initiatePropulsion(vel);
		}

		private void stabilizeDrone ()
		{
			Vector3 rot = chassis.transform.eulerAngles;
			if(rotatedLeft()){
				foreach (Propeller p in propellers) {
					if(p.type == Propeller.Type.right){
						p.propel ();
					}
				}
			}else if(rotatedRight()){
				foreach (Propeller p in propellers) {
					if(p.type == Propeller.Type.left){
						p.propel ();
					}
				}
			}

			if(rotatedForward()){
				foreach (Propeller p in propellers) {
					if(p.type == Propeller.Type.front){
						p.propel ();
					}
				}
			}else if(rotatedBack()){
				foreach (Propeller p in propellers) {
					if(p.type == Propeller.Type.back){
						p.propel ();
					}
				}
			}
		}

		private float distToGround(){
			RaycastHit hit;
			if (Physics.Raycast(chassis.transform.position, Vector3.down, out hit))
				return hit.distance;
			return -1f;
		}
		
		private bool rotatedRight(){
			Vector3 rot = chassis.transform.eulerAngles;
			if(rot.z > 1f && rot.z < 179f){
				return true;
			}
			return false;
		}

		private bool rotatedLeft(){
			Vector3 rot = chassis.transform.eulerAngles;
			if(rot.z > 181f && rot.z < 356f){
				return true;
			}
			return false;
		}

		private bool rotatedForward(){
			Vector3 rot = chassis.transform.eulerAngles;
			if(rot.x > 1f && rot.x < 179f){
				return true;
			}
			return false;
		}

		private bool rotatedBack(){
			Vector3 rot = chassis.transform.eulerAngles;
			if(rot.x > 181f && rot.x < 356f){
				return true;
			}
			return false;
		}

		private void checkForInputs ()
		{
			checkPropulsionInput ();
			checkSteeringInput ();
		}

		private void checkSteeringInput ()
		{
			HashSet<Propeller.Type> pt = new HashSet<Propeller.Type>();
			if (Input.GetKey (KeyCode.W)) {
				pt.Add(Propeller.Type.back);
				userInputting = true;
			} else if (Input.GetKeyUp (KeyCode.W)) {
				userInputting = false;
			}

			if (Input.GetKey (KeyCode.S)) {
				pt.Add(Propeller.Type.front);
				userInputting = true;
			} else if (Input.GetKeyUp (KeyCode.S)) {
				userInputting = false;
			}

			if (Input.GetKey (KeyCode.A)) {
				pt.Add(Propeller.Type.right);
				userInputting = true;
			} else if (Input.GetKeyUp (KeyCode.A)) {
				userInputting = false;
			}

			if (Input.GetKey (KeyCode.D)) {
				pt.Add(Propeller.Type.left);
				userInputting = true;
			} else if (Input.GetKeyUp (KeyCode.D)) {
				userInputting = false;
			}

			initiateSteering(pt);
		}

		private void checkPropulsionInput ()
		{
			if (Input.GetKey (KeyCode.Space)) {
				initiatePropulsion ();
				userInputting = true;
			} else if (Input.GetKeyUp (KeyCode.Space)) {
				userInputting = false;
			}
		}

		private void initiateSteering(HashSet<Propeller.Type> pt){
			foreach (Propeller p in propellers) {
				if(pt.Contains(p.type)){
					p.steer ();
				}
			}
		}

		private void initiatePropulsion (float force)
		{
			foreach (Propeller p in propellers) {
				p.propel (force);
			}
		}

		private void initiatePropulsion ()
		{
			foreach (Propeller p in propellers) {
				p.propel ();
			}
		}

		private void setUpPropellers ()
		{
			propellers = GetComponentsInChildren<Propeller> ();
		}

	}
}
