using UnityEngine;

public class RetieveOnomatopeScript : MonoBehaviour
{
    [SerializeField] private Transform onomatopeAObject; // �����o�����o�������I�u�W�F�N�g
    [SerializeField] private RectTransform bubbleImage;   // �����o��Image��RectTransform
    [SerializeField] private Camera uiCamera;             // Canvas��Render Camera�iScreen Space - Camera�̏ꍇ�j

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onomatopeAObject != null && bubbleImage != null)
        {
            // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ɕϊ�
            Vector3 screenPos = Camera.main.WorldToScreenPoint(onomatopeAObject.position + (Vector3.up * 1.0f)); // ������ɃI�t�Z�b�g
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
