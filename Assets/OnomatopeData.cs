using UnityEngine;

[System.Serializable]
public class OnomatopeData : MonoBehaviour
{
    public string type; // オノマトペの種類名
    public Color color; // 奪った色

    public OnomatopeData(string type, Color color)
    {
        this.type = type;
        this.color = color;
    }
}