using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;


    void Start()
    {
        //
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        //Get the world position borders
        Vector3 worldBottonLeft = transform.position = -Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottonLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                bool walkable;
                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask))
                {
                    walkable = false;
                }
                else
                {
                    walkable = true;
                }

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        
        }
     }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Skip if it's the center node
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //Check if we're actually still in the grid
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    
    //To get the position of the player to the griz, we must convert worldPos to gridPos
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        //First get percentage of the worldpos
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        //Should always be between 0 - 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? new Color(1,1,1,0.3f) : new Color(1,0,0,0.3f);
                if(path != null)
                {
                    if(path.Contains(n))
                    {
                        Debug.Log("Got Path");
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.wordPosition, Vector3.one * (nodeDiameter -0.1f));
            }
        }
    }
    
	
}
