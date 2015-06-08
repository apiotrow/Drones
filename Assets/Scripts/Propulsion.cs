using UnityEngine;
using System.Collections;

public class Propulsion : MonoBehaviour {
	protected float propelForce;
	private Rigidbody thisRigidBody;
	public bool propelling;

	protected virtual void Start () {
		thisRigidBody = gameObject.GetComponent<Rigidbody>();
	}

	protected virtual void Update () {
	}

	public void propel(){
		thisRigidBody.AddForce(transform.up * Time.deltaTime * propelForce);
	}
}
