using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SidePropeller : Propeller {
	protected override void Start () {
		base.Start();
		Tag_PropellerBlade[] propBlades = transform.GetComponentsInChildren<Tag_PropellerBlade>();
		propellerBlade = new GameObject[propBlades.Length];
		int i = 0;
		foreach(Tag_PropellerBlade t in propBlades){
			propellerBlade[i] = t.gameObject;
			i++;
		}
	}
}
