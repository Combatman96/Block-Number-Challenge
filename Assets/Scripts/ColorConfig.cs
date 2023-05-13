using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ColorConfig", menuName = "Config/ColorConfig", order = 1)]
public class ColorConfig : ScriptableObject
{
    public List<ColorData> colorDatas = new List<ColorData>();

    public ColorData GetColorData(ColorName color)
    {
        return colorDatas.FirstOrDefault(x => x.color == color);
    }

    public Material GetMaterial(ColorName color, int index)
    {
        ColorData colorData = GetColorData(color);
        return colorData.materials[index];
    }
}
