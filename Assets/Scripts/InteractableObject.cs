using UnityEngine;

// オノマトペオブジェクトのインタラクトやテクスチャ・動きの管理を行うスクリプト
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType; // オノマトペ種別（Inspectorで設定）
    [SerializeField] private Texture onomatopeTexture; // オノマトペ用テクスチャ
    [SerializeField] private Texture defaultTexture; // デフォルトテクスチャ
    [SerializeField] private Animator animator; // オブジェクトのアニメーター（任意）
    [SerializeField] private string onomatopeAnimName; // オノマトペ用アニメーション名
    [SerializeField] private string defaultAnimName; // デフォルトアニメーション名

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
        // アニメーションもデフォルトに戻す
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(defaultAnimName))
            {
                animator.Play(defaultAnimName);
            }
            else
            {
                Debug.Log("デフォルトアニメーション名が未設定です。");
            }
        }
        else
        {
            Debug.Log("Animatorが見つかりません。");
        }
        return taken;
    }

    // オノマトペのテクスチャを貼り付け
    public void ApplyTexture(Texture texture)
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
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
        // オノマトペ用アニメーション再生
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(onomatopeAnimName))
            {
                animator.Play(onomatopeAnimName);
            }
            else
            {
                Debug.Log("オノマトペ用アニメーション名が未設定です。");
            }
        }
        else
        {
            Debug.Log("Animatorが見つかりません。");
        }
    }

    // オノマトペを非表示にする（回収時）
    public void HideOnomatope()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
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
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
        else
        {
            Debug.Log("Rendererが見つかりません。");
        }
    }
}