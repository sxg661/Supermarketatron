using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : UnitBehaviour {

    // Update is called once per frame
    void Update()
    {
        //this is a bit hacky, fix maybe
        if (initial)
        {
            if (!forward)
                rotate(true);
            initial = false;
            spriteRenderer.sortingOrder = TileGrid.getSortingNum(blefty);
        }

        if (!UnitPlacement.unitsAreFrozen())
        {
            snapToMousePosition();
            grabOrDropObject();
            rotate(false);
        }

    }


    /// <summary>
    /// Checks for a mouse click to see whether or not it should drop/pickup the object
    /// </summary>
    private void grabOrDropObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objectGrabbed)
            {

                //the mouse is always in the same position as the bottom left tile
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (!inGridBounds() || thereIsClash())
                {
                    //transform.position = originalLoc;
                    Debug.Log("There is clash");

                }
                else
                {
                    BuildController.drop();

                    bleftx = Tile.getTile(mousePosition[0]);
                    blefty = Tile.getTile(mousePosition[1]);

                    //Debug.Log("x " + bleftx + " y " + blefty + " ID " + myID);

                    addToGrid();

                    originalLoc = transform.position;
                    pending = false;
                    objectGrabbed = false;
                }

            }
            else if (mouseHover)
            {
                BuildController.pickUp(myID);
                objectGrabbed = true;
                removeFromGrid();
                stockButton.transform.position = new Vector3(-100, -100, -100);
                originalLoc = transform.position;
            }

        }
    }




    /// <summary>
    /// Snaps the position of the init to the tile that the mouse is currently in.
    /// </summary>
    private void snapToMousePosition()
    {
        if (objectGrabbed || pending)
        {
            ///I want to snap to the tile that mouse in in
            ///rather than just follow the mouse

            //gets the mousep position
            mouseDown = true;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //find the tile that the mouse is in and the corropsonding position to snap to
            float mouseXTilePos = Tile.getPos(Tile.getTile(mousePosition[0]), myXsize);
            float mouseYTilePos = Tile.getPos(Tile.getTile(mousePosition[1]), myYsize);
            Vector2 snapPosition = new Vector3(mouseXTilePos, mouseYTilePos, -1);
            spriteRenderer.sortingOrder = TileGrid.getSortingNum(Tile.getTile(mousePosition[1]));


            //changes the position of the tile
            transform.position = snapPosition;
        }
    }

    /// <summary>
    /// True if the current position clashes with the differet unit
    /// </summary>
    /// <returns></returns>
    public bool thereIsClash()
    {
        //gets the tile coordinates of the bottom left of the unit
        int blX = Tile.getTile(mousePosition[0]);
        int blY = Tile.getTile(mousePosition[1]);
        //checks for clashes
        for (int x = 0; x < myXfloorSize; x++)
        {
            for (int y = 0; y < myYfloorSize; y++)
            {
                //Debug.Log("check");
                if (UnitPlacement.occupied(blX + x, blY + y, myID))
                {
                    //Debug.Log("Occupied");
                    return true;
                }
            }
        }
        return false;
    }

}
