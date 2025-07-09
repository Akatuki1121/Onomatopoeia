using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 目線インタラクトやUI表示・オノマトペの記憶管理を行うスクリプト
public class EyecastScript : MonoBehaviour
{

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
        if ((Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame))
        {
            CollectOnomatope();
        }
        // R2でオノマトペ貼り付け
        if (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
        // デバッグ用Eキー(オノマトペ貼り付け)
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
    }

    private void SearchInteractable()
    {
        throw new NotImplementedException();
    }



    // オノマトペ回収
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

        }
    }

    // オノマトペ貼り付け
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

