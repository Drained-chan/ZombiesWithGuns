using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform seeker, target;
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromPosition(startPos);
        Node targetNode = grid.NodeFromPosition(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    // Set new lowest G cost and calculate new H cost for the node to target node so we get accurate f-cost
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.previous = currentNode; //So we can get a path later on, sort of like linked lists?

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }

            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.previous;
        }
        path.Reverse();

        grid.path = path;
    }
    int GetDistance(Node A, Node B)
    {
        // Each diagonal costs 14, while each straight line costs 10.
        // We can know the number of diagonals in the distance by checking distance until it's inline, the smaller number is the amount of diagonals

        int distanceX = Mathf.Abs(A.gridX - B.gridX);
        int distanceY = Mathf.Abs(A.gridY - B.gridY);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + (distanceY - distanceX) * 10;
    }
}
