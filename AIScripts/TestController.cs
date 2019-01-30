using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : CustomerController
{

    bool moving = false;
    private float time = 0.0f;
    public float period = 0.02f;
    int x;
    int y;
    bool done;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(TileGrid.GRIDRANGEX[0], TileGrid.GRIDRANGEX[1] + 1);
        y = Random.Range(TileGrid.GRIDRANGEY[0], TileGrid.GRIDRANGEY[0] + 1);

        spriteRenderer = GetComponent<SpriteRenderer>();

        myBehaviour = new Idle(y);
        myBehaviour.Init(spriteRenderer, transform, DoneIdle);
    }

    
    protected override void DoneRouteFollower()
    {
        //int myXTile = Tile.getTile(x);
        int[] goal = ChooseRandomGoal();
        bool okRoute = ((RouteFollower)myBehaviour).findRoute(goal[0], goal[1]);

        if (!okRoute)
        {
            myBehaviour = new Idle(y);
            myBehaviour.Init(spriteRenderer, transform, DoneIdle);
        }
    }


    protected override void  DoneIdle()
    {
        //x = Tile.getTile(transform.position[0]);
        //y = Tile.getTile(transform.position[1]);

        //Debug.Log("Hello!!!");
        myBehaviour = new RouteFollower();
        myBehaviour.Init(spriteRenderer, transform, DoneRouteFollower);
        DoneRouteFollower();
    }


    
}
