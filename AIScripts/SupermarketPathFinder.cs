using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermarketPathFinder {

    private int[] GridBoundsX;

    private int[] GridBoundsY;

    private HashSet<String> visitedTiles = new HashSet<string>();



    private class Tile
    {
        public Optional<Tile> previous;
        public int x;
        public int y;

        public Tile(Optional<Tile> prev, int x, int y)
        {
            this.x = x;
            this.y = y;
            previous = prev;
        }
    }


    private int heuristic(int[] start, int[] end)
    {
        return Mathf.Abs(start[0] - end[0]) + Mathf.Abs(start[1] - end[1]);
    }

    private int[][] getAdjIndexes(int x, int y)
    {
        return new int[][]{
            new int[] {x-1, y},
            new int[] {x+1, y},
            new int[] {x, y-1},
            new int[] {x, y+1} };
    }


    private bool checkFree(int x, int y)
    {
    
        return TileGrid.indexInGrid(x, y) &&
           !UnitPlacement.occupied(x, y, -1);
    }

    /// <summary>
    /// This gives us the route without the start tile
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    private List<int[]> traceBackRoute(Tile tile)
    {
        List<int[]> route = new List<int[]>();
        
        while (tile.previous.isPresent())
        {
            route.Insert(0, new int[] { tile.x, tile.y });
            tile = tile.previous.get();
        }
        return route;
    }
    
    /// <summary>
    /// returns null if no route is found
    /// this is some kind of incomplete A* algorithm (don't really want to find the
    /// abosolute quickest route)
    /// It's a greedy search
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<int[]> findRoute(int[] start, int[] end)
    {

        if (!TileGrid.indexInGrid(start[0], start[1]) || !TileGrid.indexInGrid(end[0], end[1]))
        {
            return null;
        }
        
        PriorityQueue<Tile> tilesToSearch = new PriorityQueue<Tile>(TileGrid.getGridSize() + 5);
        visitedTiles = new HashSet<string>();

        tilesToSearch.add(new Tile(Optional<Tile>.empty(), start[0], start[1])
            , heuristic(start, end));


        while (tilesToSearch.hasElements())
        {
            Tile currentTile = tilesToSearch.take();

            if (currentTile.x == end[0] && currentTile.y == end[1])
            {
                return traceBackRoute(currentTile);
            }

            foreach(int[] tile in getAdjIndexes(currentTile.x, currentTile.y))
            {
                if(checkFree(tile[0], tile[1]) && !visitedTiles.Contains(TileGrid.getKey(tile[0], tile[1])) )
                {
                    int aproxDist = heuristic(tile, end);
                    Tile newTile = new Tile(Optional<Tile>.of(currentTile), tile[0], tile[1]);
                    tilesToSearch.add(newTile, aproxDist);
                    visitedTiles.Add(TileGrid.getKey(tile[0], tile[1]));

                }
            }
        }

        return null;
    }



    /// <summary>
    /// Finds a shelf near to the location passed in.
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="startY"></param>
    /// <returns></returns>
    public List<int[]> FindShelf(int startX, int startY)
    {
        PriorityQueue<Tile> tilesToSearch = new PriorityQueue<Tile>(TileGrid.getGridSize() * 4);
        visitedTiles = new HashSet<string>();
        tilesToSearch.add(new Tile(Optional<Tile>.empty(), startX, startY), 0);
        

        while (tilesToSearch.hasElements())
        {
            int currentDist = tilesToSearch.TopPriority();
            Tile currentTile = tilesToSearch.take();

            visitedTiles.Add(TileGrid.getKey(currentTile.x, currentTile.y));

            if (UnitPlacement.canAccessShelf(currentTile.x, currentTile.y) && UnityEngine.Random.Range(0, 5) == 0)
            {
                return traceBackRoute(currentTile);
            }

            foreach(int[] tile in getAdjIndexes(currentTile.x, currentTile.y))
            {
                if(checkFree(tile[0], tile[1]) && !visitedTiles.Contains(TileGrid.getKey(tile[0], tile[1])))
                {
                    tilesToSearch.add(new Tile(Optional<Tile>.of(currentTile), tile[0], tile[1]), currentDist - 1);
                }
            }
        }

        return null;
    }
}
