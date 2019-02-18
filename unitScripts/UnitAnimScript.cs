using
    System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitAnimScript : MonoBehaviour {

    public bool mouseHover;
    public bool pending;

    private UnitBehaviour unitBehav;
    private UnitMovement unitMovement;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite inactive;
    [SerializeField]
    private Sprite active;
    [SerializeField]
    private Sprite invalid;
    

	// Use this for initialization
	public void Start () {
        unitBehav = GetComponent<UnitBehaviour>();
        unitMovement = GetComponent<UnitMovement>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mouseHover = false;
    }

    public void hover(bool hover)
    {
        return;
    }

    public void rotate()
    {
        transform.rotation *= Quaternion.Euler(0, 180, 0);
        //.Rotate(Vector3.forward * -90);
        //anim.SetTrigger("rotate");
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

        //Debug.Log(mouseHover);

        if (!unitBehav.isGrabbed() && !mouseHover)
        {
            //anim.SetBool("red", false);
            spriteRenderer.sprite = inactive;
            pending = false;
            //print("notgrab");
        }
        else if(unitMovement == null)
        {
            spriteRenderer.sprite = active;
            //print("move");
            //anim.SetBool("red", false);
        }
        else if (unitBehav.inGridBounds() && !unitMovement.thereIsClash() )
        {
            spriteRenderer.sprite = active;
            //anim.SetBool("red", false);
            //print("bounds");
        }
        else
        {
            //print("invalid");
            //anim.SetBool("red", true);
            spriteRenderer.sprite = invalid;
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
