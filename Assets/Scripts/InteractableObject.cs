using UnityEngine;

// オノマトペオブジェクトのインタラクトやテクスチャ・動きの管理を行うスクリプト
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType; // オノマトペ種別（Inspectorで設定）
    [SerializeField] private Texture onomatopeTexture; // オノマトペ用テクスチャ
    [SerializeField] private Texture defaultTexture; // デフォルトテクスチャ


    // オノマトペ種別を取得
    public string OnomatopeType => onomatopeType;

    // オノマトペのテクスチャを取得
    public Texture TakeTexture()
    {
        var renderer = GetComponent<Renderer>();
        Texture taken = null;
        if (renderer != null)
        {
            taken = renderer.material.mainTexture;
            if (defaultTexture != null)
            {
                renderer.material.mainTexture = defaultTexture; // 回収時はデフォルトに戻す
            }
            else
            {
                Debug.Log("デフォルトテクスチャが未設定です。");
            }
        }
        else
        {
            Debug.Log("Rendererが見つかりません。");
        }
        return taken; // 必ず値を返すように修正
    }

    // オノマトペのテクスチャを貼り付け
    public void ApplyTexture(Texture texture)
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            if (texture != null)
            {
                renderer.material.mainTexture = texture;
            }
            else
            {
                Debug.Log("貼り付けるテクスチャが未設定です。");
            }
        }
        else
        {
            Debug.Log("Rendererが見つかりません。");
        }
    }

    // オノマトペを非表示にする（回収時）
    public void HideOnomatope()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            renderer.enabled = false;
        }
        else
        {
            Debug.Log("Rendererが見つかりません。");
        }
    }

    // オノマトペを表示する（貼り付け時）
    public void ShowOnomatope()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            renderer.enabled = true;
        }
        else
        {
            Debug.Log("Rendererが見つかりません。");
        }
    }
}