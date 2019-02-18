using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    protected Vector3 mousePosition;
    protected float moveSpeed = 100f;
    protected bool mouseHover;
    protected bool objectGrabbed;
    protected bool initial;

    public bool mouseDown;
    public Vector2 originalLoc;
    public int bleftx, blefty;
    public int myID;
    public GameObject stockButton;
    public SpriteRenderer spriteRenderer;
    public float myXsize;
    public float myYsize;
    public float myXfloorSize;
    public float myYfloorSize;
    public bool forward;
    public UnitAnimScript unitAnimScript;
    public UnitInfo.unitType myType;

    public string orientation = "forward";

    // Use this for initialization
    protected void Start()
    { 
        mouseHover = false;
        initial = true;
        originalLoc = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        unitAnimScript = GetComponent<UnitAnimScript>();
    }

    void Update()
    {
        
    }


    public void setTile(int bleftx, int blefty)
    {
        this.bleftx = bleftx;
        this.blefty = blefty;
    }

    public bool inGridBounds()
    {
        return TileGrid.inGridBounds(mousePosition, myXsize, myYsize);
    }

    public bool isGrabbed()
    {
        return objectGrabbed;
    }
    
    public void grab()
    {
        objectGrabbed = true;
    }

    public bool isHovered()
    {
        return mouseHover;
    }

    public bool showStockButton()
    {
        return (mouseHover);
    }

    public void setFloorLength(int xlength, int ylength)
    {
        myXfloorSize = xlength;
        myYfloorSize = ylength;
    }

    public void setSize(int xsize, int ysize)
    {
        myXsize = xsize;
        myYsize = ysize;
    }



    public void setId(int id)
    {
        myID = id;
    }

    public void giveStockButton(GameObject button)
    {
        stockButton = button;

        stockButton.transform.position = new Vector3(-100, -100, -100);
    }



    /// <summary>
    /// Checks for key presses to rotate a shape and rotates it if necessary
    /// </summary>
    protected void rotate(bool doAnyway)
    {

        if ((Input.GetKeyDown(KeyCode.Space) && objectGrabbed) || doAnyway)
        {
            //this if is temporary until i add an animator for the counter
            if (unitAnimScript != null)
            {
                unitAnimScript.rotate();
                Quaternion rot = transform.rotation;
                float myZ = rot.eulerAngles.z;
                myZ += 180;
                transform.rotation = Quaternion.Euler(0, 0, myZ);
                forward = !forward;
            }
        }

    }

    public void addToGrid()
    {
        UnitPlacement.addUnit(bleftx, blefty, myID, myXfloorSize, myYfloorSize, forward, myType);
    }

    public void destroyMe()
    {
        Destroy(gameObject);
    }

    public void removeFromGrid()
    {
        UnitPlacement.removeUnit(bleftx, blefty, myXfloorSize, myYfloorSize);
    }

    protected void OnMouseOver()
    {
        mouseHover = true;
        if (!objectGrabbed)
        {
            stockButton.transform.position = new Vector3(bleftx + (3 * myXsize) / 4, blefty + (3 * myYsize) / 4, 20);
        }
    }

    protected void OnMouseExit()
    {
        mouseHover = false;
        stockButton.transform.position = new Vector3(-100, -100, -100);
    }


}
