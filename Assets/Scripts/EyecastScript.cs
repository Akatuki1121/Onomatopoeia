using UnityEngine;
using UnityEngine.InputSystem;

// 目線インタラクトやオノマトペの記憶管理を呼び出すスクリプト
public class EyecastScript : MonoBehaviour
{
    public GameObject bubblePrefab; // インスペクタから渡すために公開

    private InteractableObject currentObj;


    void Update()
    {
        // 入力モードで分岐
        if (CheckInputmodeScript.CurrentInputMode == CheckInputmodeScript.InputMode.Gamepad)
        {
            //Aボタンで調べる
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                SearchInteractable();
            }
            // L2でオノマトペ回収
            if (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame)
            {
                CollectOnomatope();
            }
            // R2でオノマトペ貼り付け
            if (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                AttachOnomatope();
            }
        }
        else // Keyboard
        {
            //Qキーで調べる
            if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            {
                SearchInteractable();
            }
            // Rキーでオノマトペ回収
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                CollectOnomatope();
            }
            // Eキーでオノマトペ貼り付け
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                AttachOnomatope();
            }
        }
    }

    private bool SearchInteractable()
    {
        var cam = Camera.main;
        var ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out var hit, 3f))
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out var interactable))
            {
                currentObj = interactable;
                Debug.Log("目の前にオブジェクトが見つかりました");
                currentObj.ShowBubble(bubblePrefab); // オブジェクト検出時にbubble表示
                return true;
            }
        }

        currentObj = null;
        return false;
    }

    private void CollectOnomatope()
    {
        if (currentObj == null) return;

        var type = currentObj.OnomatopeType;

        // 記憶済みなら何もしない
        if (PlayerOnomatopeInventory.Instance.HasOnomatope(type))
        {
            Debug.Log($"「{type}」は既に記憶済みです。");
            return;
        }

        // 記憶リストに追加できるか試す
        if (PlayerOnomatopeInventory.Instance.TryAddOnomatope(type, out var errorMessage))
        {
            // 追加OKならオノマトペを回収＆非表示
            currentObj.TakeTexture();   // 内部でテクスチャをキャッシュする想定
            currentObj.HideBubble();
        }
        else
        {
            // 失敗理由をログ
            Debug.LogWarning(errorMessage);
        }
    }

    private void AttachOnomatope()
    {
        if (currentObj == null) return;

        var type = currentObj.OnomatopeType;

        // 記憶していなければ貼り付け不可
        if (!PlayerOnomatopeInventory.Instance.HasOnomatope(type))
        {
            Debug.Log($"「{type}」はまだ記憶されていません。");
            return;
        }

        // インスペクタ設定の onomatopeTexture を直接貼り付け
        currentObj.ApplyTexture(currentObj.OnomatopeTexture);


        Debug.Log($"「{type}」のオノマトペを貼り付けました");
    }
}

