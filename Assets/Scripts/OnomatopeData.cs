using UnityEngine;

[System.Serializable]
public class OnomatopeData : MonoBehaviour
{
    public string type; // �I�m�}�g�y�̎�ޖ�
    public Color color; // �D�����F

    public OnomatopeData(string type, Color color)
    {
        this.type = type;
        this.color = color;
    }
}