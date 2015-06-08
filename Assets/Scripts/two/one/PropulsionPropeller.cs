using UnityEngine;
using System.Collections;

namespace two
{
	public class PropulsionPropeller : Propulsion
	{
		protected GameObject[] propellerBlade;
		private float hoverSpeed;
		private float propelSpeed;
	
		protected override void Start ()
		{
			base.Start ();

			Tag_PropellerBlade[] propBlades = transform.GetComponentsInChildren<Tag_PropellerBlade> ();
			propellerBlade = new GameObject[propBlades.Length];
			int i = 0;
			foreach (Tag_PropellerBlade t in propBlades) {
				propellerBlade [i] = t.gameObject;
				i++;
			}

			propelForce = 2000f;
			hoverSpeed = 200f;
			propelSpeed = 800f;
		}

		protected override void Update ()
		{
			base.Update ();
			foreach (GameObject g in propellerBlade) {
				if (propelling)
					g.gameObject.transform.Rotate (Vector3.up * Time.deltaTime * propelSpeed);
				else
					g.gameObject.transform.Rotate (Vector3.up * Time.deltaTime * hoverSpeed);
			}
		}
	}
}
