using
    System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitAnimScript : MonoBehaviour { 

    public bool mouseHover = false;
    public bool pending;

    private UnitBehaviour unitBehav;
    private UnitMovement unitMovement;
    private Animator anim;

	// Use this for initialization
	public void Start () {
        unitBehav = GetComponent<UnitBehaviour>();
        unitMovement = GetComponent<UnitMovement>();
        anim = GetComponent<Animator>();
        if (pending)
        {
            hover(true);
        }
        Debug.Log("script");
    }

    public void hover(bool hover)
    {
        mouseHover = hover;
        anim.SetBool("hover", hover);
    }

    public void rotate()
    {
        anim.SetTrigger("rotate");
    }

    public void Update()
    {
        if (!UnitPlacement.unitsAreFrozen())
        {
            highlightUnit();
        }
        

    }

    private void highlightUnit()
    {
        //once dragging I don't want hover to come off even if you go temporarily
        //outside the box

        if (!unitBehav.isGrabbed() || pending)
        {
            anim.SetBool("red", false);
            pending = false;
        }
        else if(unitMovement == null)
        {
            anim.SetBool("red", false);
        }
        else if (unitBehav.inGridBounds() && !unitMovement.thereIsClash() )
        {
            anim.SetBool("red", false);
        }
        else
        {
            anim.SetBool("red", true);
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
