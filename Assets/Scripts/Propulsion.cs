using UnityEngine;
using System.Collections;

public class Propulsion : MonoBehaviour {
	protected Propeller propeller;
	protected float propelForce;
	private Rigidbody body;
	public bool propelling;

	protected virtual void Start () {
		body = gameObject.GetComponent<Rigidbody>();
	}

	protected virtual void Update () {
	}

	public void propel(){
		body.AddForce(transform.up * Time.deltaTime * 2000f);
	}
}
