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
			backward,
			stable
		};

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
			if (!userInputting)
				stabilizeDrone ();
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

		private RotationDirection getRotationDirection(){
			Vector3 rot = chassis.transform.eulerAngles;
			if(rot.z > 181f && rot.z < 356f){
				return RotationDirection.left;
			}else if(rot.z > 1f && rot.z < 179f){
				return RotationDirection.right;
			}
			return RotationDirection.stable;
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
				pt.Add(Propeller.Type.left);
				userInputting = true;
			} else if (Input.GetKeyUp (KeyCode.A)) {
				userInputting = false;
			}

			if (Input.GetKey (KeyCode.D)) {
				pt.Add(Propeller.Type.right);
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
			} else if (Input.GetKeyUp (KeyCode.Space)) {

			}
		}

		private void initiateSteering(HashSet<Propeller.Type> pt){
			foreach (Propeller p in propellers) {
				if(pt.Contains(p.type)){
					p.steer ();
				}
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
