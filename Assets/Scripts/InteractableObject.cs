using UnityEngine;

// オノマトペオブジェクトのインタラクトやテクスチャ・吹き出しUI管理を行うスクリプト
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType;     // オノマトペ種別（Inspectorで設定）
    [SerializeField] private Texture onomatopeTexture; // オノマトペ用テクスチャ
    [SerializeField] private Texture defaultTexture;   // デフォルトテクスチャ
    private GameObject currentBubble;                  // 現在表示中の吹き出しUI

    // 外部から参照できるプロパティ
    public string OnomatopeType => onomatopeType;
    public Texture OnomatopeTexture => onomatopeTexture;

    /// <summary>
    /// オノマトペを回収するときに呼ぶ。
    /// 現在のマテリアルを defaultTexture に戻し、
    /// onomatopeTexture を返す。
    /// </summary>
    public Texture TakeTexture()
    {
        if (TryGetComponent<Renderer>(out var rend))
        {
            rend.material.mainTexture = defaultTexture != null
                ? defaultTexture
                : rend.material.mainTexture;
            return onomatopeTexture;
        }

        Debug.LogError("Renderer が見つかりません。");
        return null;
    }

    /// <summary>
    /// 指定のテクスチャを貼り付ける（Attach 時に使用）
    /// </summary>
    public void ApplyTexture(Texture texture)
    {
        if (texture == null)
        {
            Debug.LogError("貼り付けるテクスチャが null です。");
            return;
        }

        if (TryGetComponent<Renderer>(out var rend))
        {
            rend.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Renderer が見つかりません。");
        }
    }


    /// <summary>
    /// 吹き出しを表示する（bubblePrefab は外部から渡す）
    /// </summary>
    public void ShowBubble(GameObject bubblePrefab)
    {
        if (currentBubble == null && bubblePrefab != null)
        {
            currentBubble = Instantiate(
                bubblePrefab,
                transform.position + (Vector3.up * 1.5f),
                Quaternion.identity,
                transform
            );
        }
    }

    /// <summary>
    /// 吹き出しを非表示にする
    /// </summary>
    public void HideBubble()
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
            currentBubble = null;
        }
    }
}