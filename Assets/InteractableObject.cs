using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType; // オノマトペ種別（Inspectorで設定）

    // オノマトペ種別を取得
    public string OnomatopeType => onomatopeType;

    // 色を奪い、オブジェクトの色を白にする
    public Color TakeColor()
    {
        var renderer = GetComponent<Renderer>();
        Color originalColor = Color.white;
        if (renderer != null)
        {
            originalColor = renderer.material.color;
            renderer.material.color = Color.white; // 奪った後は白に
        }
        return originalColor;
    }
}