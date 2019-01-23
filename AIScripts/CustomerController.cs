using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour {

    CustomerBehaviour myBehaviour;
    SpriteRenderer renderer;
    bool moving = false;
    private float time = 0.0f;
    public float period = 0.02f;
    int x;
    int y;
    bool done;

	// Use this for initialization
	void Start () {
		x = Random.Range(TileGrid.GRIDRANGEX[0], TileGrid.GRIDRANGEX[1] + 1);
        y = Random.Range(TileGrid.GRIDRANGEY[0], TileGrid.GRIDRANGEY[0] + 1);

        renderer = GetComponent<SpriteRenderer>();

        myBehaviour = new Idle(y);
        myBehaviour.Init(renderer, transform, Done);

    }


    int[] ChooseRandomGoal()
    {
        int[] goal = new int[2];
        goal[0] = Random.Range(TileGrid.GRIDRANGEX[0], TileGrid.GRIDRANGEX[1] + 1);
        goal[1] = Random.Range(TileGrid.GRIDRANGEY[0], TileGrid.GRIDRANGEY[1] + 1);
        return goal;
    }

    void Done()
    {
        x = Tile.getTile(transform.position[0]);
        y = Tile.getTile(transform.position[1]);

        
        if(!myBehaviour.myType().Equals("routeFollower"))
        {
            myBehaviour = new RouteFollower();
            myBehaviour.Init(renderer, transform, Done);
        }

        int myXTile = Tile.getTile(x);
        int[] goal = ChooseRandomGoal();
        ((RouteFollower)myBehaviour).givePos(x, y);
        bool okRoute = ((RouteFollower) myBehaviour).findRoute(goal[0], goal[1]);

        if (!okRoute)
        {
            myBehaviour = new Idle(y);
            myBehaviour.Init(renderer, transform, Done);
        }
    }


	
	// Update is called once per frame
	void Update () {


        myBehaviour.Update();
        

	}

    /*
    private void Update()
    {
       
        
        time += Time.deltaTime;

        if (true)
        {
            time = time - period;
            Control();
        }
        
    }*/
}
