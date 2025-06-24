using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType; // �I�m�}�g�y��ʁiInspector�Őݒ�j

    // �I�m�}�g�y��ʂ��擾
    public string OnomatopeType => onomatopeType;

    // �F��D���A�I�u�W�F�N�g�̐F�𔒂ɂ���
    public Color TakeColor()
    {
        var renderer = GetComponent<Renderer>();
        Color originalColor = Color.white;
        if (renderer != null)
        {
            originalColor = renderer.material.color;
            renderer.material.color = Color.white; // �D������͔���
        }
        return originalColor;
    }
}