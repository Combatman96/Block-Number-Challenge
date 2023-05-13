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
        root.SetCenterMarking(new Vector2Int((int)dir.normalized.x, (int)dir.normalized.y));
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
        Node previous = null;
        for (i = root.coordinate.y + 1; i <= end.coordinate.y; ++i)
        {
            var node = m_map[new Vector2Int(root.coordinate.x, i)];
            if (root.rootNumber == 0)
            {
                if (previous) previous.SetIsEnd(true);
                UpdateRootLineMarking(root);
                break;
            }
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                {
                    if (previous) previous.SetIsEnd(true);
                    UpdateRootLineMarking(root);
                    break;
                }
            }
            root.rootNumber--;
            root.UpdateRootNumber();
            if (i == end.coordinate.y)
                node.SetIsEnd(true);
            else
                node.SetIsEnd(false);
            previous = node;
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.up);
        }
    }

    private async void SpawnDown(Node root, Node end)
    {
        int i = 0;
        Node previous = null;
        for (i = root.coordinate.y - 1; i >= end.coordinate.y; --i)
        {
            var node = m_map[new Vector2Int(root.coordinate.x, i)];
            if (root.rootNumber == 0)
            {
                if (previous) previous.SetIsEnd(true);
                UpdateRootLineMarking(root);
                break;
            }
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                {
                    if (previous) previous.SetIsEnd(true);
                    UpdateRootLineMarking(root);
                    break;
                }
            }
            root.rootNumber--;
            root.UpdateRootNumber();
            if (i == end.coordinate.y)
                node.SetIsEnd(true);
            else
                node.SetIsEnd(false);
            previous = node;
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.down);
        }
    }

    private async void SpawnLeft(Node root, Node end)
    {
        int i = 0;
        Node previous = null;
        for (i = root.coordinate.x - 1; i >= end.coordinate.x; --i)
        {
            var node = m_map[new Vector2Int(i, root.coordinate.y)];
            if (root.rootNumber == 0)
            {
                if (previous) previous.SetIsEnd(true);
                UpdateRootLineMarking(root);
                break;
            }
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                {
                    if (previous) previous.SetIsEnd(true);
                    UpdateRootLineMarking(root);
                    break;
                }
            }
            root.rootNumber--;
            root.UpdateRootNumber();
            if (i == end.coordinate.x)
                node.SetIsEnd(true);
            else
                node.SetIsEnd(false);
            previous = node;
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.left);
        }
    }

    private async void SpawnRight(Node root, Node end)
    {
        int i = 0;
        Node previous = null;
        for (i = root.coordinate.x + 1; i <= end.coordinate.x; ++i)
        {
            var node = m_map[new Vector2Int(i, root.coordinate.y)];
            if (root.rootNumber == 0)
            {
                if (previous) previous.SetIsEnd(true);
                UpdateRootLineMarking(root);
                break;
            }
            if (node.IsMark())
            {
                if (node.GetRootCoordinate() == root.coordinate)
                    continue;
                else
                {
                    if (previous) previous.SetIsEnd(true);
                    UpdateRootLineMarking(root);
                    break;
                }
            }
            root.rootNumber--;
            root.UpdateRootNumber();
            if (i == end.coordinate.x)
                node.SetIsEnd(true);
            else
                node.SetIsEnd(false);
            previous = node;
            await node.SpawnAsync(root.coordinate, root.rootColor, Vector2Int.right);
        }
    }

    public void Despawn(Node node)
    {
        var linkNode = m_map[node.GetLinkNodeCoord()];
        var root = m_map[node.GetRootCoordinate()];
        linkNode.SetIsEnd(!linkNode.IsRoot());
        node.Despawn();
        root.rootNumber++;
        root.UpdateRootNumber();
        UpdateRootLineMarking(root);
    }

    public void UpdateRootLineMarking(Node root)
    {
        Vector2Int[] dir = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        bool[] hasLink = { false, false, false, false };
        for (int i = 0; i < dir.Length; i++)
        {
            Vector2Int coord = root.coordinate + dir[i];
            if (!m_map.ContainsKey(coord)) continue;

            if (root.coordinate == m_map[coord].GetRootCoordinate())
            {
                hasLink[i] = true;
            }
        }
        root.SetCenterMarking(hasLink);
    }
}
