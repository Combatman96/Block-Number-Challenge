using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    private Dictionary<Vector2Int, Node> m_map = new Dictionary<Vector2Int, Node>();

    public void DoStart()
    {
        //Init grid map 
        var nodes = GetComponentsInChildren<Node>();
        foreach (var node in nodes)
        {
            node.DoStart();
            m_map.Add(node.coordinate, node);
        }
    }

    public void Spawn(Node root, Node end)
    {
        if (root.coordinate.x != end.coordinate.x && root.coordinate.y != end.coordinate.y)
            return;
        if (root.rootNumber == 0)
            return;
        Vector2 dir = end.coordinate - root.coordinate;
        if (dir.normalized == Vector2.up)
        {
            SpawnUp(root, end);
        }
        if (dir.normalized == Vector2.down)
        {
            SpawnDown(root, end);
        }
        if (dir.normalized == Vector2.left)
        {
            SpawnLeft(root, end);
        }
        if (dir.normalized == Vector2.right)
        {
            SpawnRight(root, end);
        }
    }

    private async void SpawnUp(Node root, Node end)
    {
        int i = 0;
        for (i = root.coordinate.y + 1; i <= end.coordinate.y; ++i)
        {
            if (root.rootNumber == 0) break;
            var node = m_map[new Vector2Int(root.coordinate.x, i)];
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                    break;
            }
            node.SetIsEnd(false);
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.up);
            root.rootNumber--;
            root.UpdateRootNumber();
        }
        m_map[new Vector2Int(root.coordinate.x, i - 1)].SetIsEnd(true);
    }

    private async void SpawnDown(Node root, Node end)
    {
        int i = 0;
        for (i = root.coordinate.y - 1; i >= end.coordinate.y; --i)
        {
            if (root.rootNumber == 0) break;
            var node = m_map[new Vector2Int(root.coordinate.x, i)];
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                    break;
            }
            node.SetIsEnd(false);
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.down);
            root.rootNumber--;
            root.UpdateRootNumber();
        }
        m_map[new Vector2Int(root.coordinate.x, i + 1)].SetIsEnd(true);
    }

    private async void SpawnLeft(Node root, Node end)
    {
        int i = 0;
        for (i = root.coordinate.x - 1; i >= end.coordinate.x; --i)
        {
            if (root.rootNumber == 0) break;
            var node = m_map[new Vector2Int(i, root.coordinate.y)];
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                    break;
            }
            node.SetIsEnd(false);
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.left);
            root.rootNumber--;
            root.UpdateRootNumber();
        }
        m_map[new Vector2Int(i + 1, root.coordinate.y)].SetIsEnd(true);
    }

    private async void SpawnRight(Node root, Node end)
    {
        int i = 0;
        for (i = root.coordinate.x + 1; i <= end.coordinate.x; ++i)
        {
            if (root.rootNumber == 0) break;
            var node = m_map[new Vector2Int(i, root.coordinate.y)];
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                    break;
            }
            node.SetIsEnd(false);
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.right);
            root.rootNumber--;
            root.UpdateRootNumber();
        }
        m_map[new Vector2Int(i - 1, root.coordinate.y)].SetIsEnd(true);
    }

    public void Despawn(Node node)
    {
        var linkNode = m_map[node.GetLinkNodeCoord()];
        var root = m_map[node.GetRootCoordinate()];
        linkNode.SetIsEnd(!linkNode.IsRoot());
        node.Despawn();
        root.rootNumber++;
        root.UpdateRootNumber();
    }
}
