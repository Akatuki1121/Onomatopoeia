using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 目線インタラクトやUI表示・オノマトペの記憶管理を行うスクリプト
public class EyecastScript : MonoBehaviour
{
    [SerializeField] private GameObject bubbleImageUI;
    [SerializeField] private GameObject bubblePrefab;    //吹き出し    
    private InteractableObject currentObj;

    // オノマトペ種別ごとに1つだけ記憶（テクスチャ保存に変更）
    private Dictionary<string, Texture> onomatopeCollection = new();

    void Update()
    {
        //Aボタン/Qキーで調べる
        if ((Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame))
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
        // デバッグ用Q/Eキー
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
    }

    // 調べる：目線先のInteractableObjectをcurrentObjにセットしUI表示
    void SearchInteractable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            var interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                currentObj = interactable;
                if (bubbleImageUI != null) bubbleImageUI.SetActive(true);
            }
        }
    }

    // オノマトペ回収：テクスチャ保存＆非表示
    void CollectOnomatope()
    {
        if (currentObj != null)
        {
            string type = currentObj.OnomatopeType;
            if (onomatopeCollection.ContainsKey(type))
            {
                Debug.Log("既にこの種類のオノマトペは記憶済みです。");
                return;
            }
            Texture tex = currentObj.TakeTexture();
            onomatopeCollection[type] = tex;
            currentObj.HideOnomatope(); // 非表示
            if (bubbleImageUI != null) bubbleImageUI.SetActive(false);
            Debug.Log($"{type}のオノマトペを回収しました");
        }
    }

    // オノマトペ貼り付け：保存したテクスチャをcurrentObjに貼り付け＆表示
    void AttachOnomatope()
    {
        if (currentObj != null)
        {
            string type = currentObj.OnomatopeType;
            if (onomatopeCollection.TryGetValue(type, out Texture tex))
            {
                currentObj.ApplyTexture(tex);
                currentObj.ShowOnomatope();
                Debug.Log($"{type}のオノマトペを貼り付けました");
            }
        }
    }
}

