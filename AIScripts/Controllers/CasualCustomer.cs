using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualCustomer : CustomerController
{

    int x;
    int y;
    int shelvesVisited = 0;
    public CustomerSpawner spawner;
    bool exit;
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

    private void setPos()
    {
        x = Tile.getTile(transform.position.x);
        y = Tile.getTile(transform.position.y);
    }


    private void makeNewRouteFollower()
    {
        myBehaviour = new RouteFollower();
        myBehaviour.Init(spriteRenderer, transform, DoneRouteFollower);


    }


    private void goToRandomLoc()
    {
        //int myXTile = Tile.getTile(x);
        goal = ChooseRandomGoal();
        setPos();
        myRoute = pathFinder.findRoute(new int[] { x, y }, goal);

        myBehaviour.done = DoneRouteFollower;

        checkRouteValid();
    }


    private void goToNearestShelf()
    {
        setPos();

        myRoute = pathFinder.FindShelf(x, y);
        myBehaviour.done = DoneFindShelf;

        checkRouteValid();
    }

    

    private void GoToCounter()
    {
        makeNewRouteFollower();
        setPos();
        myRoute = pathFinder.findRoute(new int[] { x, y }, UnitPlacement.counterLoc);
        
        myBehaviour.done = DoneFindShelf;
        exit = true;

        checkRouteValid();
    }

    private void GoToExit()
    {
        makeNewRouteFollower();
        setPos();
        myRoute = pathFinder.findRoute(new int[] { x, y }, TileGrid.getDoorLoc());

        myBehaviour.done = LeaveSupermarket;

        checkRouteValid();

    }

    private void LeaveSupermarket()
    {
        if(spawner != null)
        {
            spawner.removeCustomer();
        }
        Destroy(gameObject);
    }


    private void checkRouteValid()
    {
        if (myRoute == null || myRoute.Count < 1)
        {
            myBehaviour = new Idle(y);
            myBehaviour.Init(spriteRenderer, transform, DoneIdle);
            return;
        }

        bool okRoute = ((RouteFollower)myBehaviour).giveRoute(myRoute);
    }

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

        makeNewRouteFollower();

        if (shelvesVisited >= 3)
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

        makeNewRouteFollower();

        if(shelvesVisited >= 3)
        {
            GoToExit();
        }
        else
        {
            goToRandomLoc();
        }

        
    }


  

}
