using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ColorConfig", menuName = "Config/ColorData", order = 2)]
public class ColorData : ScriptableObject
{
    public ColorName color;
    public List<Material> materials = new List<Material>();
}

public enum ColorName
{
    Red,
    Lavender,
    Yellow
}
