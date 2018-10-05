using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatic : UnitBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (!forward && initial)
        {
            initial = false;
            rotate(true);
        }
	}
}
