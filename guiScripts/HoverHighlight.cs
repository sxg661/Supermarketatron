using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight : MonoBehaviour {

    public bool hover;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //anim.SetBool("hover", hover);
	}

    private void OnMouseOver()
    {
        hover = true;
    }

    private void OnMouseExit()
    {
        hover = false;
    }
    
}
