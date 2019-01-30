using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollower : CustomerBehaviour {

    public enum direction
    {
        LEFT, RIGHT, UP, DOWN
    }

    public direction myDirection;
    public float x;
    public float y;

    public int goalX;
    public int goalY;
    public int startX;
    public int startY;

    public int routeIndex;
    public List<int[]> route;
    public float SPEED = 0.05f;
    SpriteRenderer spriteRenderer;
    Transform transform;
    bool first = true;
    public SupermarketPathFinder pathFinder = new SupermarketPathFinder();

    private float timeSinceLast;
    private float timePerMove = 0.5f;

    public override string myType()
    {
        return "routeFollower";
    }

    public void givePos(int x, int y)
    {
        this.x = Tile.getPos(x, 1);
        this.y = Tile.getPos(y, 1);
    }


    bool finished = false;

    

    // Use this for initialization
    public override void Init(SpriteRenderer renderer, Transform tran, DoneDel doneMethod) {
        spriteRenderer = renderer;
        transform = tran;
        x = transform.position[0];
        y = transform.position[1];
        transform.position = new Vector2(x, y);

        done = doneMethod;


    }


    public bool findRoute(int xg, int yg)
    {
        
        route = pathFinder.findRoute(new int[] { Tile.getTile(x), Tile.getTile(y) }, new int[] { xg, yg });

        if (route == null || route.Count < 1)
        {
            route = new List<int[]>();
            return false;
        }

        goalX = route[0][0];
        goalY = route[0][1];
        startX = Tile.getTile(x);
        startY = Tile.getTile(y);
        //Debug.Log("hey " + transform.position);
        transform.position = new Vector2(x, y);
        //Debug.Log("hey2 " + transform.position);
        routeIndex = 0;
        establishDirection();
        timeSinceLast = 0;

        return true;
    }



    private void setSortingLayer()
    {
        spriteRenderer.sortingOrder = TileGrid.getSortingNum(Tile.getTile(y));

    }


    //NOT CURRENTLY USED, BUT I MIGHT NEED THIS IN THE FUTURE
    private void establishDirection()
    {
        if (Tile.getPos(route[routeIndex][0],1) != x)
        {
            if (Tile.getPos(route[routeIndex][0], 1) > x)
            {
                myDirection = direction.RIGHT;

            }
            else
            {
                myDirection = direction.LEFT;
            }
        }
        else
        {
            if (Tile.getPos(route[routeIndex][1], 1) > y)
            {
                myDirection = direction.UP;
            }
            else
            {
                myDirection = direction.DOWN;
            }

        }
    }






     // Move the AI
    public override void Update()
    {
        timeSinceLast += Time.deltaTime;
        //Debug.Log(timeSinceLast);
;
        if (timeSinceLast >= timePerMove)
        {


            x = Tile.getPos(goalX, 1);
            y = Tile.getPos(goalY, 1);

            transform.position = new Vector2(x, y);


            if (routeIndex == route.Count - 1)
            {
                done();
                finished = true;
                return;
            }

            routeIndex += 1;
            goalX = route[routeIndex][0];
            goalY = route[routeIndex][1];
            startX = Tile.getTile(x);
            startY = Tile.getTile(y);

            timeSinceLast = 0;

            establishDirection();
            

        }
        else
        {
            //moveInDirection(0.1f);

            //Might change to using LERP but this is good :)
            x = (timeSinceLast/timePerMove) * (Tile.getPos(goalX, 1) - Tile.getPos(startX, 1)) + Tile.getPos(startX, 1);
            y = (timeSinceLast/timePerMove) * (Tile.getPos(goalY, 1) - Tile.getPos(startY, 1)) + Tile.getPos(startY, 1);

            setSortingLayer();
        }

        Vector2 newPos = transform.position;
        newPos.x = x;
        newPos.y = y;

        transform.position = newPos;
    }

    
}

