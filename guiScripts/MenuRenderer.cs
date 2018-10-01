using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRenderer : MonoBehaviour {

    public bool enabled = false;
    CanvasGroup alpha;

	// Use this for initialization
	void Start () {
        alpha = GetComponent<CanvasGroup>();
        alpha.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        enabled = BuildController.displayingStockMenu();

        if (enabled)
        {
            alpha.alpha = 1;
        }
        else
        {
            alpha.alpha = 0;
        }
		
	}

}
