using UnityEngine;
using TMPro;

public class Marking : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_numberText;
    [SerializeField] private MeshRenderer m_mesh;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform m_root;

    public static readonly int s_open = Animator.StringToHash("open");
    public static readonly int s_close = Animator.StringToHash("close");

    [Header("Lines")]
    [SerializeField] Transform centerSprite;
    [SerializeField] Transform upSprite;
    [SerializeField] Transform downSprite;
    [SerializeField] Transform leftSprite;
    [SerializeField] Transform rightSprite;
    [SerializeField] Transform endSprite;
    [SerializeField] Transform lineSprite;

    public void SetNumber(int num)
    {
        m_numberText.SetText(num.ToString());
    }

    public void SetNumVisible(bool visible)
    {
        m_numberText.gameObject.SetActive(visible);
    }

    public void ClearSprite()
    {
        centerSprite.gameObject.SetActive(false);
        upSprite.gameObject.SetActive(false);
        downSprite.gameObject.SetActive(false);
        leftSprite.gameObject.SetActive(false);
        rightSprite.gameObject.SetActive(false);
        endSprite.gameObject.SetActive(false);
        lineSprite.gameObject.SetActive(false);
    }

    public void SetCenterSprite(bool up, bool down, bool left, bool right)
    {
        ClearSprite();
        centerSprite.gameObject.SetActive(true);
        upSprite.gameObject.SetActive(up);
        downSprite.gameObject.SetActive(down);
        leftSprite.gameObject.SetActive(left);
        rightSprite.gameObject.SetActive(right);
    }

    public void SetCenterSprite(Vector2Int direction)
    {
        if (direction == Vector2.up)
            upSprite.gameObject.SetActive(true);
        if (direction == Vector2.down)
            downSprite.gameObject.SetActive(true);
        if (direction == Vector2.left)
            leftSprite.gameObject.SetActive(true);
        if (direction == Vector2.right)
            rightSprite.gameObject.SetActive(true);
    }

    public void SetNonRootSprite(bool isEnd)
    {
        ClearSprite();
        lineSprite.gameObject.SetActive(!isEnd);
        endSprite.gameObject.SetActive(isEnd);
    }

    public void SetMaterial(Material mat)
    {
        m_mesh.sharedMaterial = mat;
    }

    public void PlayAnimation(int state, Vector2 direction)
    {
        float rotaionZ = 0f;
        if (direction == Vector2.up) rotaionZ = 90f;
        if (direction == Vector2.down) rotaionZ = -90f;
        if (direction == Vector2.left) rotaionZ = 180f;
        if (direction == Vector2.right) rotaionZ = 0f;
        m_root.rotation = Quaternion.Euler(0f, 0f, rotaionZ);
        m_animator.CrossFade(state, 0f, 0);
    }

    public void PlayAnimation(int state)
    {
        m_animator.CrossFade(state, 0f, 0);
    }

    public Vector2Int GetSpawnDirection()
    {
        return new Vector2Int((int)m_root.right.x, (int)m_root.right.y);
    }
}
