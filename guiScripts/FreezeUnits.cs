using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeUnits : MonoBehaviour {

	

    public void OnMouseOver()
    {
        UnitPlacement.freezeUnits(true);
    }

    public void OnMouseExit()
    {
        UnitPlacement.freezeUnits(false);
    }
}
