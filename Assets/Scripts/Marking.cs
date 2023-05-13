using UnityEngine;
using TMPro;

public class Marking : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_numberText;
    [SerializeField] private MeshRenderer m_mesh;

    public void SetNumber(int num)
    {
        m_numberText.SetText(num.ToString());
    }

    public void SetMaterial(Material mat)
    {
        m_mesh.sharedMaterial = mat;
    }
}
