using UnityEngine;
using UnityEngine.InputSystem;

// �ڐ��C���^���N�g��I�m�}�g�y�̋L���Ǘ����Ăяo���X�N���v�g
public class EyecastScript : MonoBehaviour
{
    public GameObject bubblePrefab; // �C���X�y�N�^����n�����߂Ɍ��J

    private InteractableObject currentObj;


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
        var cam = Camera.main;
        var ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out var hit, 3f))
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out var interactable))
            {
                currentObj = interactable;
                Debug.Log("�ڂ̑O�ɃI�u�W�F�N�g��������܂���");
                currentObj.ShowBubble(bubblePrefab); // �I�u�W�F�N�g���o����bubble�\��
                return true;
            }
        }

        currentObj = null;
        return false;
    }

    private void CollectOnomatope()
    {
        if (currentObj == null) return;

        var type = currentObj.OnomatopeType;

        // �L���ς݂Ȃ牽�����Ȃ�
        if (PlayerOnomatopeInventory.Instance.HasOnomatope(type))
        {
            Debug.Log($"�u{type}�v�͊��ɋL���ς݂ł��B");
            return;
        }

        // �L�����X�g�ɒǉ��ł��邩����
        if (PlayerOnomatopeInventory.Instance.TryAddOnomatope(type, out var errorMessage))
        {
            // �ǉ�OK�Ȃ�I�m�}�g�y���������\��
            currentObj.TakeTexture();   // �����Ńe�N�X�`�����L���b�V������z��
            currentObj.HideBubble();
        }
        else
        {
            // ���s���R�����O
            Debug.LogWarning(errorMessage);
        }
    }

    private void AttachOnomatope()
    {
        if (currentObj == null) return;

        var type = currentObj.OnomatopeType;

        // �L�����Ă��Ȃ���Γ\��t���s��
        if (!PlayerOnomatopeInventory.Instance.HasOnomatope(type))
        {
            Debug.Log($"�u{type}�v�͂܂��L������Ă��܂���B");
            return;
        }

        // �C���X�y�N�^�ݒ�� onomatopeTexture �𒼐ړ\��t��
        currentObj.ApplyTexture(currentObj.OnomatopeTexture);


        Debug.Log($"�u{type}�v�̃I�m�}�g�y��\��t���܂���");
    }
}

