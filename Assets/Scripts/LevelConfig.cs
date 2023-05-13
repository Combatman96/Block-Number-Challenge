using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig", order = 3)]
public class LevelConfig : ScriptableObject
{
    public List<RootData> datas = new List<RootData>();
    private Dictionary<Vector2Int, RootData> dict = new Dictionary<Vector2Int, RootData>();

    public RootData GetRootData(Vector2Int coordinate)
    {
        if (dict.Count == 0)
        {
            foreach (var data in datas)
            {
                dict.Add(data.coordinate, data);
            }
        }
        if (dict.ContainsKey(coordinate))
            return dict[coordinate];
        return null;
    }
}

[System.Serializable]
public class RootData
{
    public Vector2Int coordinate;
    public ColorName color;
    public int number;
}
