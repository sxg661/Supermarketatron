using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicker : MonoBehaviour {

    public Animator anim;


    private bool mouseHover;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        mouseHover = false;

    }
	
	// Update is called once per frame
	void Update () {

        //once dragging I don't want hover to come off even if you go temporarily
        //outside the box

        anim.SetBool("hover", mouseHover);
     

        if (mouseHover && Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("mouseDown");
        }

    }

 



    public void OnMouseOver()
    {
        mouseHover = true;
    }

    public void OnMouseExit()
    {
        mouseHover = false;
    }
}
