using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 目線インタラクトやUI表示・オノマトペの記憶管理を行うスクリプト
public class EyecastScript : MonoBehaviour
{
    private InteractableObject currentObj;
    private Dictionary<string, Texture> onomatopeCollection = new();

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
        Camera cam = Camera.main;
        Ray ray = new(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f)) // 3メートル以内で判定
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out var interactable))
            {
                currentObj = interactable;
                Debug.Log("目の前にオブジェクトが見つかりました");
                return true;
            }
            else
            {
                currentObj = null;
                return false;
            }
        }
        else
        {
            currentObj = null;
            return false;
        }
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

