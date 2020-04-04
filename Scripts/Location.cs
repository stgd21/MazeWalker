using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level
{
    abstract public void StartAt(Location location);
    abstract public Location MakeConnection(Location location);
}


public class Location
{
    public int x;
    public int y;

    public Location(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
}

public class Connections
{
    public bool inMaze = false;
    public bool[] directions = { false, false, false, false };
}

public class GridLevel : Level
{
    public Vector3[] NEIGHBORS = { new Vector3(1, 0, 0), new Vector3(0, 1, 1), new Vector3(0, -1, 2), new Vector3(-1, 0, 3) };
    public static int width = 10;
    public static int height = 10;
    public Connections[,] cells = new Connections[width, height];

    public GridLevel()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cells[i, j] = new Connections();
            }
        }
    }


    public override void StartAt(Location location)
    {
        cells[location.x, location.y].inMaze = true;
    }

    bool CanPlaceCorridor(int x, int y, int dirn)
    {
        //In bounds and not part of maze
        return (0 <= x && x < width && 0 <= y && y < height && !cells[x, y].inMaze);
    }

    public override Location MakeConnection(Location location)
    {
        //Debug.Log("neighbors before: ");
        //DebugArrayValues(NEIGHBORS);
        Vector3[] neighbors = shuffle(NEIGHBORS);
        //Debug.Log("new shuffled array: ");
        //DebugArrayValues(neighbors);

        int x = location.x;
        int y = location.y;
        for (int i = 0; i < neighbors.Length; i++)
        {
            int nx = x + (int)neighbors[i].x;
            int ny = y + (int)neighbors[i].y;
            int fromDirn = 3 - (int)neighbors[i].z;
            if (CanPlaceCorridor(nx, ny, fromDirn))
            {
                //Perform connection
                cells[x, y].directions[(int)neighbors[i].z] = true;
                cells[nx, ny].inMaze = true;
                cells[nx, ny].directions[fromDirn] = true;
                return new Location(nx, ny);
            }
        }
        return null;
    }

    Vector3[] shuffle(Vector3[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Vector3 temp = array[i];
            int r = Random.Range(i, array.Length);
            array[i] = array[r];
            array[r] = temp;
        }
        return array;
    }

    void DebugArrayValues(Vector3[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(i + " (" + array[i].x + ", " + array[i].y + ", " + array[i].z + ")");
        }
    }
}
