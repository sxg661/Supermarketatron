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
    public float timeUntilGoal;
    public int routeIndex;
    public List<int[]> route;
    public float SPEED = 0.05f;
    SpriteRenderer spriteRenderer;
    Transform transform;
    bool first = true;
    public SupermarketPathFinder pathFinder = new SupermarketPathFinder();

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
        routeIndex = 0;
        establishDirection();

        return true;
    }

    private void setSortingLayer()
    {
        spriteRenderer.sortingOrder = TileGrid.getSortingNum(Tile.getTile(y));

    }

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


    public bool checkGoalReached()
    {
        switch (myDirection)
        {
            case direction.UP:
                return y >= Tile.getPos(goalY, 1);
            case direction.DOWN:
                return y <= Tile.getPos(goalY, 1);
            case direction.RIGHT:
                return x >= Tile.getPos(goalX, 1);
            case direction.LEFT:
                return x <= Tile.getPos(goalX, 1);
        }

        return false;
    }


    public void moveInDirection(float speed)
    {
        switch (myDirection)
        {
            case direction.UP:
                y = y + speed;
                break;
            case direction.DOWN:
                y = y - speed;
                break;
            case direction.LEFT:
                x = x - speed;
                break;
            case direction.RIGHT:
                x = x + speed;
                break;

        }
    }




     // Move the AI
    public override void Update()
    {
        if (checkGoalReached())
        {


            x = Tile.getPos(goalX, 1);
            y = Tile.getPos(goalY, 1);
            

            if (finished || routeIndex == route.Count - 1)
            {
                done();
                finished = true;
                return;
            }

            routeIndex += 1;
            goalX = route[routeIndex][0];
            goalY = route[routeIndex][1];

            establishDirection();
            

        }
        else
        {
            timeUntilGoal -= Time.deltaTime;
            setSortingLayer();
        }


        transform.position = new Vector2(x, y);
    }

    
}

