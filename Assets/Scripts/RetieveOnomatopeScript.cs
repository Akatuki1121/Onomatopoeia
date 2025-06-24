using UnityEngine;

public class RetieveOnomatopeScript : MonoBehaviour
{
    [SerializeField] private Transform onomatopeAObject; // 吹き出しを出したいオブジェクト
    [SerializeField] private RectTransform bubbleImage;   // 吹き出しImageのRectTransform
    [SerializeField] private Camera uiCamera;             // CanvasのRender Camera（Screen Space - Cameraの場合）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onomatopeAObject != null && bubbleImage != null)
        {
            // オブジェクトのワールド座標をスクリーン座標に変換
            Vector3 screenPos = Camera.main.WorldToScreenPoint(onomatopeAObject.position + (Vector3.up * 1.0f)); // 少し上にオフセット
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                bubbleImage.parent as RectTransform,
                screenPos,
                uiCamera != null ? uiCamera : Camera.main,
                out anchoredPos
            );
            bubbleImage.anchoredPosition = anchoredPos;
        }
    }
}
