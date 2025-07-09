using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �ڐ��C���^���N�g��UI�\���E�I�m�}�g�y�̋L���Ǘ����s���X�N���v�g
public class EyecastScript : MonoBehaviour
{

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
        if ((Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame))
        {
            CollectOnomatope();
        }
        // R2�ŃI�m�}�g�y�\��t��
        if (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
        // �f�o�b�O�pE�L�[(�I�m�}�g�y�\��t��)
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AttachOnomatope();
        }
    }

    private void SearchInteractable()
    {
        throw new NotImplementedException();
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

