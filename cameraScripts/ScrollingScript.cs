using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

    private float[] offset = new float[] { 0, 0 };


    private static float SPEED = 0.4f;

    //arrays tells you wherther you have recently collided with the edge of the screen
    //left right down up
    private bool[] collisions = new bool[]{false, false, false, false};



    public float[] getOffset()
	{
	    return offset;
	}
        
   

    // Update is called once per frame

    /// <summary>
    /// Deals with the scrolling of the screen when arrow keys are pressed.
    /// </summary>
    void Update()
    {

        float xmove = Input.GetAxis("Horizontal");
        float ymove = Input.GetAxis("Vertical");
        Vector2 cameraPosition = transform.position;



        //the ammount of movent depends on how much you press the arrow key and
        //as you take your finger off the screen may do a little jumpy thing due to the lower multiplier
        //being allowed whereas the higher one wasn't, so it stops, then starts again.
        //If we disable movement in this direction at this point until the camera moves 
        //in the opposite direction then we won't get a jump and the game looks nicer :)

		
        //Left collision 
        xmove = handleCollisions(xmove, x => x < 0, 0);

        //Right collion
        xmove = handleCollisions(xmove, x => x > 0, 1);

        //Down collision
        ymove = handleCollisions(ymove, y => y < 0, 2);

        //Up collisions
        ymove = handleCollisions(ymove, y => y > 0, 3);

        //this updates the offset for my static components
        offset[0] += SPEED * xmove;
        offset[1] += SPEED * ymove;


        Vector3 newPosition = new Vector3(cameraPosition[0] + SPEED * xmove, cameraPosition[1] + SPEED * ymove, -10f);


        //checks if the outside of the screen has been bumped
        int checkCollsion = TileGrid.inScreenBounds(newPosition);

        if (checkCollsion != -1)
        {
            collisions[checkCollsion] = true;
        }
        else
        {
            transform.position = newPosition;
        }


    }


    private float handleCollisions(float move, Predicate<float> check, int index)
    {
        if (collisions[index])
        {
            //if you're still colliding
            if (check(move))
            {
                return 0;
            }
            //else you have started going the other way now
            else
            {
                collisions[index] = false;
            }
        }
        return move;

    }
}
