using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector2 worldPosition;

    public int gCost;
    public int hCost;

    public Node(bool _walkable, Vector2 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
