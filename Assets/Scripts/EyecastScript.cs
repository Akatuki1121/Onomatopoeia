using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyecastrScript : MonoBehaviour
{
    [SerializeField] private GameObject bubbleImageUI;
    private InteractableObject currentObj;

    // オノマトペ種別ごとに1つだけ記憶
    private Dictionary<string, OnomatopeData> onomatopeCollection = new();

    void Update()
    {
        // L2でImage回収
        if (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            CollectImage();
        }

        // R2でImageを付ける
        if (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            AttachImage();
        }
        //デバッグ用にQ/Eキーにも割り当て
        // QでImage回収
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            CollectImage();
        }
        // EでImageを付ける
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttachImage();
        }
    }


    void CollectImage()
    {
        if (bubbleImageUI != null && bubbleImageUI.activeSelf && currentObj != null)
        {
            string type = currentObj.OnomatopeType; // InteractableObjectにOnomatopeTypeプロパティが必要

            // 既に同じ種類を持っていたら奪えない
            if (onomatopeCollection.ContainsKey(type))
            {
                Debug.Log("既にこの種類のオノマトペは記憶済みです。");
                return;
            }

            Color color = currentObj.TakeColor(); // 色を奪うメソッド（要実装）
            onomatopeCollection[type] = gameObject.AddComponent<OnomatopeData>();

            bubbleImageUI.SetActive(false);
            currentObj = null;
        }
    }

    void AttachImage()
    {
        if (currentObj != null && bubbleImageUI != null && !bubbleImageUI.activeSelf)
        {
            bubbleImageUI.SetActive(true);
            // 必要に応じてcurrentObjに何かする
        }
    }
}