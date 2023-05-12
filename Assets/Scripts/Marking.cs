using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Marking : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_numberText;

    public void SetNumber(int num)
    {
        m_numberText.SetText(num.ToString());
    }
}
