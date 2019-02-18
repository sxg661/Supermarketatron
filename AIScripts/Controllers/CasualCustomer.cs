using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualCustomer : CustomerController
{

    int x;
    int y;
    int shelvesVisited = 0;
    public CustomerSpawner spawner;
    RouteFollower myRouteFollower;
    bool exit;
    int[] goal;
    Optional<List<int[]>> currentPath = Optional<List<int[]>>.empty();
    public SupermarketPathFinder pathFinder = new SupermarketPathFinder();
    List<int[]> myRoute;
    int maxShelfVisits = 4;



    // Start is called before the first frame update
    void Start()
    {
        
        //x = Random.Range(TileGrid.GRIDRANGEX[0], TileGrid.GRIDRANGEX[1] + 1);
        //y = Random.Range(TileGrid.GRIDRANGEY[0], TileGrid.GRIDRANGEY[1] + 1);

        spriteRenderer = GetComponent<SpriteRenderer>();

        myRouteFollower = new RouteFollower();
        myRouteFollower.Init(spriteRenderer, transform, DoneRouteFollower);

        goToNearestShelf();
    }

    private void setPos()
    {
        x = Tile.getTile(transform.position.x);
        y = Tile.getTile(transform.position.y);
    }



    private void goToRandomLoc()
    {
        //int myXTile = Tile.getTile(x);
        goal = ChooseRandomGoal();
        setPos();
        myRoute = pathFinder.findRoute(new int[] { x, y }, goal);

        myBehaviour.done = DoneRouteFollower;

        giveRoute();
    }


    private void goToNearestShelf()
    {
        myBehaviour = myRouteFollower;
        setPos();

        myRoute = pathFinder.FindShelf(x, y);
        myBehaviour.done = DoneFindShelf;

        giveRoute();
    }

    

    private void GoToCounter()
    {
        myBehaviour = myRouteFollower;
        setPos();
        myRoute = pathFinder.findRoute(new int[] { x, y }, UnitPlacement.counterLoc);
        
        myBehaviour.done = DoneFindShelf;
        exit = true;

        giveRoute();
    }

    private void GoToExit()
    {
        myBehaviour = myRouteFollower;
        setPos();
        myRoute = pathFinder.findRoute(new int[] { x, y }, TileGrid.getDoorLoc());

        myBehaviour.done = LeaveSupermarket;

        giveRoute();

    }

    private void LeaveSupermarket()
    {
        if(spawner != null)
        {
            spawner.removeCustomer();
        }
        Destroy(gameObject);
    }


    private void giveRoute()
    {
        if (myRoute == null || myRoute.Count < 1)
        {
            myBehaviour = new Idle(y);
            myBehaviour.Init(spriteRenderer, transform, DoneIdle);
            return;
        }

        ((RouteFollower)myBehaviour).giveRoute(myRoute);
    }





    //VVV ----------------- HERE ARE THE STATE TRANSITIONS -------------------------------------------------------------------------- VVV


    protected void DoneFindShelf()
    {
        shelvesVisited++;
        myBehaviour = new Wait();
        
        myBehaviour.Init(spriteRenderer, transform, DoneWait);

        if (exit)
        {
            myBehaviour.done = GoToExit;
        }
    }



    protected void DoneRouteFollower()
    {
        goToNearestShelf();

    }

    protected void DoneWait()
    {

        myBehaviour = myRouteFollower;

        if (shelvesVisited >= maxShelfVisits)
        {
            GoToCounter();
        }
        else{
            goToRandomLoc();
        }

        
        
    }


    protected void  DoneIdle()
    {
        //x = Tile.getTile(transform.position[0]);
        //y = Tile.getTile(transform.position[1]);

        //Debug.Log("Hello!!!");

        myBehaviour = myRouteFollower;

        if (shelvesVisited >= maxShelfVisits)
        {
            GoToExit();
        }
        else
        {
            goToRandomLoc();
        }

        
    }


  

}
