using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : CustomerController
{

    int x;
    int y;
    int[] goal;
    Optional<List<int[]>> currentPath = Optional<List<int[]>>.empty();
    public SupermarketPathFinder pathFinder = new SupermarketPathFinder();
    List<int[]> myRoute;


    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(TileGrid.GRIDRANGEX[0], TileGrid.GRIDRANGEX[1] + 1);
        y = Random.Range(TileGrid.GRIDRANGEY[0], TileGrid.GRIDRANGEY[1] + 1);

        spriteRenderer = GetComponent<SpriteRenderer>();

        myBehaviour = new Idle(y);
        myBehaviour.Init(spriteRenderer, transform, DoneIdle);
    }

    

    private void makeNewRouteFollower()
    {
        myBehaviour = new RouteFollower();
        myBehaviour.Init(spriteRenderer, transform, DoneRouteFollower);


    }

    protected override void DoneRouteFollower()
    {
        //int myXTile = Tile.getTile(x);
        goal = ChooseRandomGoal();
        x = Tile.getTile(transform.position.x);
        y = Tile.getTile(transform.position.y);
        myRoute = pathFinder.findRoute(new int[] { x, y }, goal);
        //myRoute = pathFinder.findNearestShelf(x, y);
        

        if (myRoute == null || myRoute.Count < 1)
        {
            myBehaviour = new Idle(y);
            myBehaviour.Init(spriteRenderer, transform, DoneIdle);
            return;
        }
        

        bool okRoute = ((RouteFollower)myBehaviour).giveRoute(myRoute);
    }



    protected override void  DoneIdle()
    {
        //x = Tile.getTile(transform.position[0]);
        //y = Tile.getTile(transform.position[1]);

        //Debug.Log("Hello!!!");

        makeNewRouteFollower();

        DoneRouteFollower();
    }


  

}
