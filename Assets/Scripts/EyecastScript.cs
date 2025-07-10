using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �ڐ��C���^���N�g��UI�\���E�I�m�}�g�y�̋L���Ǘ����s���X�N���v�g
public class EyecastScript : MonoBehaviour
{
    private InteractableObject currentObj;
    private Dictionary<string, Texture> onomatopeCollection = new();

    void Update()
    {
        // ���̓��[�h�ŕ���
        if (CheckInputmodeScript.CurrentInputMode == CheckInputmodeScript.InputMode.Gamepad)
        {
            //A�{�^���Œ��ׂ�
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
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
        }
        else // Keyboard
        {
            //Q�L�[�Œ��ׂ�
            if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            {
                SearchInteractable();
            }
            // R�L�[�ŃI�m�}�g�y���
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                CollectOnomatope();
            }
            // E�L�[�ŃI�m�}�g�y�\��t��
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

        if (Physics.Raycast(ray, out RaycastHit hit, 3f)) // 3���[�g���ȓ��Ŕ���
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out var interactable))
            {
                currentObj = interactable;
                Debug.Log("�ڂ̑O�ɃI�u�W�F�N�g��������܂���");
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

    // �I�m�}�g�y���
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
        }
    }

    // �I�m�}�g�y�\��t��
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

