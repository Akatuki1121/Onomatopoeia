using UnityEngine;

// �I�m�}�g�y�I�u�W�F�N�g�̃C���^���N�g��e�N�X�`���E�����̊Ǘ����s���X�N���v�g
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType; // �I�m�}�g�y��ʁiInspector�Őݒ�j
    [SerializeField] private Texture onomatopeTexture; // �I�m�}�g�y�p�e�N�X�`��
    [SerializeField] private Texture defaultTexture; // �f�t�H���g�e�N�X�`��


    // �I�m�}�g�y��ʂ��擾
    public string OnomatopeType => onomatopeType;

    // �I�m�}�g�y�̃e�N�X�`�����擾
    public Texture TakeTexture()
    {
        var renderer = GetComponent<Renderer>();
        Texture taken = null;
        if (renderer != null)
        {
            taken = renderer.material.mainTexture;
            if (defaultTexture != null)
            {
                renderer.material.mainTexture = defaultTexture; // ������̓f�t�H���g�ɖ߂�
            }
            else
            {
                Debug.Log("�f�t�H���g�e�N�X�`�������ݒ�ł��B");
            }
        }
        else
        {
            Debug.Log("Renderer��������܂���B");
        }
        return taken; // �K���l��Ԃ��悤�ɏC��
    }

    // �I�m�}�g�y�̃e�N�X�`����\��t��
    public void ApplyTexture(Texture texture)
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            if (texture != null)
            {
                renderer.material.mainTexture = texture;
            }
            else
            {
                Debug.Log("�\��t����e�N�X�`�������ݒ�ł��B");
            }
        }
        else
        {
            Debug.Log("Renderer��������܂���B");
        }
    }

    // �I�m�}�g�y���\���ɂ���i������j
    public void HideOnomatope()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            renderer.enabled = false;
        }
        else
        {
            Debug.Log("Renderer��������܂���B");
        }
    }

    // �I�m�}�g�y��\������i�\��t�����j
    public void ShowOnomatope()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            renderer.enabled = true;
        }
        else
        {
            Debug.Log("Renderer��������܂���B");
        }
    }
}