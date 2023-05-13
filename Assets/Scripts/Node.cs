using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int coordinate;
    [SerializeField] private Marking m_marking;
    public int rootNumber = 0;
    private bool m_isRoot = false;
    private bool m_isEnd = false;

    private Vector2Int m_rootCoordinate;
    public ColorName rootColor;

    private Gameplay m_gameplay => FindObjectOfType<Gameplay>();

    public bool IsRoot()
    {
        return m_isRoot;
    }

    public void DoStart()
    {
        Unmark();
        var levelConfig = m_gameplay.levelConfig;
        var data = levelConfig.GetRootData(coordinate);
        if (data != null)
        {
            Init(data);
        }

        m_isRoot = (rootNumber > 0);
        ShowMarking(m_isRoot);
        if (m_isRoot) m_marking.SetNumber(rootNumber);

        var colorConfig = m_gameplay.colorConfig;
        Material rootMat = colorConfig.GetMaterial(rootColor, 0);
        m_marking.SetMaterial(rootMat);
    }

    private void Init(RootData data)
    {
        rootNumber = data.number;
        rootColor = data.color;
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
        m_rootCoordinate = coord;
    }

    public void UpdateRootNumber()
    {
        m_marking.SetNumber(rootNumber);
    }

    public bool IsLink()
    {
        return !(m_isRoot || m_isEnd);
    }

    public void SetIsEnd(bool isEnd)
    {
        m_isEnd = isEnd;
    }

    public bool IsEnd()
    {
        return m_isEnd;
    }

    public void Unmark()
    {
        rootNumber = 0;
        m_isEnd = false;
        ShowMarking(false);
        m_rootCoordinate = new Vector2Int(-9, -9);
    }

    public Vector2Int GetRootCoordinate()
    {
        return m_rootCoordinate;
    }

    public void Spawn(Vector2Int rootCoord, ColorName color)
    {
        //TODO: Set texture, materials and play animation 
        SetRootCoordinate(rootCoord);
        ShowMarking(true);
        var colorConfig = m_gameplay.colorConfig;
        int index = (rootCoord.x - coordinate.x != 0) ? Mathf.Abs(rootCoord.x - coordinate.x) : Mathf.Abs(rootCoord.y - coordinate.y);
        Debug.Log(color.ToString() + index);
        Material mat = colorConfig.GetMaterial(color, index);
        m_marking.SetMaterial(mat);
    }
}
