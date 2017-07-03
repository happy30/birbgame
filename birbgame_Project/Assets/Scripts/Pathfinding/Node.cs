using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;

    public Vector3 wordPosition;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parent;


    public Node(bool walk, Vector3 worldPos, int gX, int gY)
    {
        walkable = walk;
        wordPosition = worldPos;
        gridX = gX;
        gridY = gY;
    }

	public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
    