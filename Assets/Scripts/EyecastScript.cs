using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyecastrScript : MonoBehaviour
{
    [SerializeField] private GameObject bubbleImageUI;
    [SerializeField] private GameObject bubblePrefab;    //�����o��    
    private InteractableObject currentObj;

    // �I�m�}�g�y��ʂ��Ƃ�1�����L��
    private Dictionary<string, OnomatopeData> onomatopeCollection = new();

    void Update()
    {
        //A�Œ��ׂ�

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
        //F�Œ��ׂ�
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("F�L�[��������܂���");
            SearchObject();

        }
        // Q��Image���
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Q�L�[��������܂���");
            CollectImage();
        }
        // E��Image��t����
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E�L�[��������܂���");
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
                Debug.Log("���ׂ鏈�������s���܂���");
            }
            else
            {
                Debug.Log("���ׂ���I�u�W�F�N�g������܂���");
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
                string type = currentObj.OnomatopeType; // InteractableObject��OnomatopeType�v���p�e�B���K�v

                // ���ɓ�����ނ������Ă�����D���Ȃ�
                if (onomatopeCollection.ContainsKey(type))
                {
                    Debug.Log("���ɂ��̎�ނ̃I�m�}�g�y�͋L���ς݂ł��B");
                    return;
                }

                Color color = currentObj.TakeColor(); // �F��D�����\�b�h�i�v�����j
                onomatopeCollection[type] = gameObject.AddComponent<OnomatopeData>();

                //�����o���폜
                Destroy(interactable.gameObject);
                Debug.Log($"�u{type}�v��������ăv���n�u���폜���܂���");

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
            // �K�v�ɉ�����currentObj�ɉ�������
        }
    }
}