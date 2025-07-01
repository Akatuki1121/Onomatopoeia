using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyecastrScript : MonoBehaviour
{
    [SerializeField] private GameObject bubbleImageUI;
    [SerializeField] private GameObject bubblePrefab;    //吹き出し    
    private InteractableObject currentObj;

    // オノマトペ種別ごとに1つだけ記憶
    private Dictionary<string, OnomatopeData> onomatopeCollection = new();

    void Update()
    {
        //Aで調べる

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
        //Fで調べる
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("Fキーが押されました");
            SearchObject();

        }
        // QでImage回収
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Qキーが押されました");
            CollectImage();
        }
        // EでImageを付ける
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Eキーが押されました");
            AttachImage();
        }
    }

    void SearchObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            SearchableObjectScript searchable = hit.collider.GetComponent<SearchableObjectScript>();
            if (searchable != null)
            {
                searchable.ShowBubble(bubblePrefab);
                Debug.Log("調べる処理を実行しました");
            }
            else
            {
                Debug.Log("調べられるオブジェクトがありません");
            }
        }
    }

    void CollectImage()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

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

                //吹き出し削除
                Destroy(interactable.gameObject);
                Debug.Log($"「{type}」を回収してプレハブを削除しました");

                bubbleImageUI.SetActive(false);
                currentObj = null;
            }
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