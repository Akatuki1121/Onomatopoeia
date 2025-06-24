using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyecastrScript : MonoBehaviour
{
    [SerializeField] private GameObject bubbleImageUI;
    private InteractableObject currentObj;

    // �I�m�}�g�y��ʂ��Ƃ�1�����L��
    private Dictionary<string, OnomatopeData> onomatopeCollection = new();

    void Update()
    {
        // L2��Image���
        if (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            CollectImage();
        }

        // R2��Image��t����
        if (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            AttachImage();
        }
        //�f�o�b�O�p��Q/E�L�[�ɂ����蓖��
        // Q��Image���
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            CollectImage();
        }
        // E��Image��t����
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttachImage();
        }
    }


    void CollectImage()
    {
        if (bubbleImageUI != null && bubbleImageUI.activeSelf && currentObj != null)
        {
            string type = currentObj.OnomatopeType; // InteractableObject��OnomatopeType�v���p�e�B���K�v

            // ���ɓ�����ނ������Ă�����D���Ȃ�
            if (onomatopeCollection.ContainsKey(type))
            {
                Debug.Log("���ɂ��̎�ނ̃I�m�}�g�y�͋L���ς݂ł��B");
                return;
            }

            Color color = currentObj.TakeColor(); // �F��D�����\�b�h�i�v�����j
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
            // �K�v�ɉ�����currentObj�ɉ�������
        }
    }
}