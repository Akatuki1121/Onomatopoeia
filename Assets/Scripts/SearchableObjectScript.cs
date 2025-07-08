using UnityEngine;

// 調べられるオブジェクトに吹き出しUIを表示・非表示するスクリプト
public class SearchableObjectScript : MonoBehaviour
{
    private GameObject currentBubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //吹き出しを出す
    public void ShowBubble(GameObject bubblePrefab)
    {
        if (currentBubble == null)
        {
            currentBubble = Instantiate(bubblePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            currentBubble.transform.SetParent(transform);
        }
    }

    //吹き出しを消す
    public void HideBubble()
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
            currentBubble = null;
        }
    }
}
