using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �ڐ��C���^���N�g��UI�\���E�I�m�}�g�y�̋L���Ǘ����s���X�N���v�g
public class EyecastScript : MonoBehaviour
{
    [SerializeField] private GameObject bubbleImageUI;
    [SerializeField] private GameObject bubblePrefab;    //�����o��    
    private InteractableObject currentObj;

    // �I�m�}�g�y��ʂ��Ƃ�1�����L���i�e�N�X�`���ۑ��ɕύX�j
    private Dictionary<string, Texture> onomatopeCollection = new();

    void Update()
    {
        //A�{�^��/Q�L�[�Œ��ׂ�
        if ((Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame))
        {
            SearchInteractable();
        }

        // L2�ŃI�m�}�g�y���
        if (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            CollectOnomatope();
        }
        // R2�ŃI�m�}�g�y�\��t��
        if (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
        // �f�o�b�O�pQ/E�L�[
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
    }

    // ���ׂ�F�ڐ����InteractableObject��currentObj�ɃZ�b�g��UI�\��
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

    // �I�m�}�g�y����F�e�N�X�`���ۑ�����\��
    void CollectOnomatope()
    {
        if (currentObj != null)
        {
            string type = currentObj.OnomatopeType;
            if (onomatopeCollection.ContainsKey(type))
            {
                Debug.Log("���ɂ��̎�ނ̃I�m�}�g�y�͋L���ς݂ł��B");
                return;
            }
            Texture tex = currentObj.TakeTexture();
            onomatopeCollection[type] = tex;
            currentObj.HideOnomatope(); // ��\��
            if (bubbleImageUI != null) bubbleImageUI.SetActive(false);
            Debug.Log($"{type}�̃I�m�}�g�y��������܂���");
        }
    }

    // �I�m�}�g�y�\��t���F�ۑ������e�N�X�`����currentObj�ɓ\��t�����\��
    void AttachOnomatope()
    {
        if (currentObj != null)
        {
            string type = currentObj.OnomatopeType;
            if (onomatopeCollection.TryGetValue(type, out Texture tex))
            {
                currentObj.ApplyTexture(tex);
                currentObj.ShowOnomatope();
                Debug.Log($"{type}�̃I�m�}�g�y��\��t���܂���");
            }
        }
    }
}

