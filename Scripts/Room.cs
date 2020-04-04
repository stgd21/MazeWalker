//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Room
//{
//    public int width = 5;
//    public int height = 5;
//}

//public class GridLevelWithRooms : GridLevel
//{
//    public Stack<Room> unplacedRooms = new Stack<Room>();
//    const float CHANCE_OF_ROOM = 0.3f;

//    public bool canPlaceRoom(Room room, int x, int y)
//    {
//        bool inBounds = (0 <= x && x < (width - room.width) && 0 <= y && y < (height - room.height));
//        if (!inBounds)
//        {
//            return false;
//        }
//        int rx = x;
//        int ry = y;
//        while (rx < x + room.width)
//        {
//            while (ry < y + room.height)
//            {
//                if (cells[rx, ry].inMaze)
//                    return false;
//                ry++;
//            }
//            rx++;
//        }

//        return true;
//    }

//    void addRoom(Room room, Location location)
//    {
//        int x = location.x;
//        int y = location.y;
//        while (x < location.x + room.width)
//        {
//            while (y < location.y + room.height)
//            {
//                cells[x, y].inMaze = true;

//                y++;
//            }
//            x++;
//        }
//        //Go through and set connections in the room
//        x = location.x;
//        y = location.y;
//        while (x < location.x + room.width)
//        {
//            while (y < location.y + room.height)
//            {
//                //If using connections, set all connections in the room
//                //right
//                if (cells[x + 1, y] != null && cells[x + 1, y].inMaze == true)
//                    cells[x, y].directions[0] = true;
//                //up
//                if (cells[x, y + 1] != null && cells[x, y + 1].inMaze == true)
//                    cells[x, y].directions[1] = true;
//                //down
//                if (y != 0)
//                    if (cells[x, y - 1] != null && cells[x, y - 1].inMaze == true)
//                        cells[x, y].directions[2] = true;
//                //left
//                if (x != 0)
//                    if (cells[x - 1, y] != null && cells[x - 1, y].inMaze == true)
//                        cells[x, y].directions[3] = true;
//                y++;
//            }
//            x++;
//        }
//    }

//    public override Location MakeConnection(Location location)
//    {
//        //try to fit a room
//        if (unplacedRooms.Count != 0 && Random.Range(0f, 1f) < CHANCE_OF_ROOM)
//        {
//            Debug.Log("making a room");
//            int x = location.x;
//            int y = location.y;

//            //Choose a room and work out its origin
//            Room room = unplacedRooms.Pop();
//            Vector3 info = NEIGHBORS[(int)Random.Range(0, NEIGHBORS.Length)];
//            int nx = x + (int)info.x;
//            int ny = y + (int)info.y;
//            if (info.x < 0)
//                nx -= room.width;
//            if (info.y < 0)
//                ny -= room.height;

//            if (canPlaceRoom(room, nx, ny))
//            {
//                //Fill the room
//                addRoom(room, new Location(nx, ny)); //is this right?

//                //perform connection
//                cells[x, y].directions[(int)info.z] = true;
//                cells[x + (int)info.x, y + (int)info.y].directions[3 - (int)info.z] = true;

//                //return nothing if rooms aren't part of main maze, otherwise might return room exit
//                return null;

//            }
//        }
//        Debug.Log("not making a room");
//        return base.MakeConnection(location);
//    }
//}
