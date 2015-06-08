using UnityEngine;
using System.Collections;

namespace one
{
	public class Steering : MonoBehaviour
	{
		protected float steerForce;
		private Rigidbody thisRigidBody;
		public bool steering;
		public Direction direction;
		public enum Direction
		{
			forward,
			backward,
			left,
			right
	}
		;

		protected virtual void Start ()
		{
			thisRigidBody = gameObject.GetComponent<Rigidbody> ();
		}

		protected virtual void Update ()
		{
	
		}

		public void steer ()
		{
			thisRigidBody.AddForce (transform.up * Time.deltaTime * steerForce);
		}
	}
}
