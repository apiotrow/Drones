using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace two
{
	public class Propeller : MonoBehaviour
	{
		protected float propelForce = 2000f;
		private List<Rigidbody> propellerBlades = new List<Rigidbody> ();
		protected float hoverSpeed = 200f;
		protected float propelSpeed = 80f;
		public Rigidbody propellingBody;
		public bool propelling;
		public Type type;
		public enum Type
		{

		}

		protected virtual void Start ()
		{
			initPropellingBody();
			initPropellerBlades();
		}

		protected virtual void Update ()
		{
		}

		public void propel ()
		{
			propellingBody.AddForce (transform.up * Time.deltaTime * propelForce);

			foreach (Rigidbody g in propellerBlades) {
				g.AddTorque (Vector3.up * propelSpeed, ForceMode.Acceleration);
			}

		}

		private void initPropellingBody(){
			foreach (Transform child in transform)
				if (child.CompareTag ("Propeller"))
					propellingBody = child.GetComponent<Rigidbody> ();
		}

		private void initPropellerBlades(){
			foreach (Transform child in transform){
				if (child.CompareTag ("PropellerBlade")) {
					child.gameObject.GetComponent<Rigidbody> ().maxAngularVelocity = 1000f;
					propellerBlades.Add (child.gameObject.GetComponent<Rigidbody> ());		
				}
			}
		}
	}
}
