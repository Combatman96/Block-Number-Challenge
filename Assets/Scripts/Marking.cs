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

    public void SetNumber(int num)
    {
        m_numberText.SetText(num.ToString());
    }

    public void SetNumVisible(bool visible)
    {
        m_numberText.gameObject.SetActive(visible);
    }

    public void SetMaterial(Material mat)
    {
        m_mesh.sharedMaterial = mat;
    }

    public void PlayAnimation(int state, Vector2 direction)
    {
        m_root.right = direction;
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
