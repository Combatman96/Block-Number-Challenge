using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int coordinate;
    [SerializeField] private Marking m_marking;
    public int rootNumber = 0;
    private bool m_isRoot = false;

    private Vector2Int rootCoordinate;

    public bool IsRoot()
    {
        return m_isRoot;
    }

    public void DoStart()
    {
        m_isRoot = (rootNumber > 0);
        ShowMarking(m_isRoot);
        if (m_isRoot) m_marking.SetNumber(rootNumber);
    }

    private void ShowMarking(bool visible)
    {
        m_marking.gameObject.SetActive(visible);

    }

    public bool IsMark()
    {
        return m_marking.gameObject.activeInHierarchy;
    }

    public void SetRootCoordinate(Vector2Int coord)
    {
        rootCoordinate = coord;
        ShowMarking(true);
    }

    public void UpdateRootNumber()
    {
        m_marking.SetNumber(rootNumber);
    }
}
