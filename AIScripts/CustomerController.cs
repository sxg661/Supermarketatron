using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerController : MonoBehaviour {




    protected CustomerBehaviour myBehaviour;
    protected SpriteRenderer spriteRenderer;
   



    protected static int[] ChooseRandomGoal()
    {
        int[] goal = new int[2];
        goal[0] = Random.Range(TileGrid.GRIDRANGEX[0], TileGrid.GRIDRANGEX[1] + 1);
        goal[1] = Random.Range(TileGrid.GRIDRANGEY[0], TileGrid.GRIDRANGEY[1] + 1);
        return goal;
    }

    protected abstract void DoneRouteFollower();

    protected abstract void DoneIdle();

  

    void FixedUpdate()
    {
        if (myBehaviour != null)
        {
            myBehaviour.Update();
        }


    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (myBehaviour != null && trigger.gameObject.tag == "AI")
        {
            myBehaviour.OnCollision();
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (myBehaviour != null && trigger.gameObject.tag == "AI")
        {
            myBehaviour.onCollsionEnd();
        }
    }



}
