using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class Node : MonoBehaviour
{
    public Vector2Int coordinate;
    [SerializeField] private Marking m_marking;
    public int rootNumber = 0;
    [SerializeField] private bool m_isRoot = false;
    [SerializeField] private bool m_isEnd = false;

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
        if (m_isRoot)
        {
            m_marking.SetNumber(rootNumber);
            SetCenterMarking(new[] { false, false, false, false });
        }
        else m_marking.SetNumVisible(false);

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
        m_marking.SetNonRootSprite(m_isEnd);
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

    public async Task SpawnAsync(Vector2Int rootCoord, ColorName color, Vector2Int direction)
    {
        SetRootCoordinate(rootCoord);
        ShowMarking(true);

        var colorConfig = m_gameplay.colorConfig;
        int index = (rootCoord.x - coordinate.x != 0) ? Mathf.Abs(rootCoord.x - coordinate.x) : Mathf.Abs(rootCoord.y - coordinate.y);
        Material mat = colorConfig.GetMaterial(color, index);
        m_marking.SetMaterial(mat);
        m_marking.PlayAnimation(Marking.s_open, direction);

        await Task.Delay(100);
    }

    public Vector2Int GetLinkNodeCoord()
    {
        Vector2Int foldDir = m_marking.GetSpawnDirection();
        return coordinate - foldDir;
    }

    public void Despawn()
    {
        m_rootCoordinate = new Vector2Int(-9, -9);
        m_isEnd = false;
        m_marking.PlayAnimation(Marking.s_close);
        StartCoroutine(SetMarkingAfter(false, 0.1f));
    }

    IEnumerator SetMarkingAfter(bool visible, float time)
    {
        yield return new WaitForSeconds(time);
        ShowMarking(visible);
    }

    public void SetCenterMarking(bool[] direcions)
    {
        m_marking.SetCenterSprite(direcions[0], direcions[1], direcions[2], direcions[3]);
    }

    public void SetCenterMarking(Vector2Int direction)
    {
        m_marking.SetCenterSprite(direction);
    }

}
