using UnityEngine;

// �I�m�}�g�y�I�u�W�F�N�g�̃C���^���N�g��e�N�X�`���E�����̊Ǘ����s���X�N���v�g
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType; // �I�m�}�g�y��ʁiInspector�Őݒ�j
    [SerializeField] private Texture onomatopeTexture; // �I�m�}�g�y�p�e�N�X�`��
    [SerializeField] private Texture defaultTexture; // �f�t�H���g�e�N�X�`��
    [SerializeField] private Animator animator; // �I�u�W�F�N�g�̃A�j���[�^�[�i�C�Ӂj
    [SerializeField] private string onomatopeAnimName; // �I�m�}�g�y�p�A�j���[�V������
    [SerializeField] private string defaultAnimName; // �f�t�H���g�A�j���[�V������

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
        // �A�j���[�V�������f�t�H���g�ɖ߂�
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(defaultAnimName))
            {
                animator.Play(defaultAnimName);
            }
            else
            {
                Debug.Log("�f�t�H���g�A�j���[�V�����������ݒ�ł��B");
            }
        }
        else
        {
            Debug.Log("Animator��������܂���B");
        }
        return taken;
    }

    // �I�m�}�g�y�̃e�N�X�`����\��t��
    public void ApplyTexture(Texture texture)
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
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
        // �I�m�}�g�y�p�A�j���[�V�����Đ�
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(onomatopeAnimName))
            {
                animator.Play(onomatopeAnimName);
            }
            else
            {
                Debug.Log("�I�m�}�g�y�p�A�j���[�V�����������ݒ�ł��B");
            }
        }
        else
        {
            Debug.Log("Animator��������܂���B");
        }
    }

    // �I�m�}�g�y���\���ɂ���i������j
    public void HideOnomatope()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
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
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
        else
        {
            Debug.Log("Renderer��������܂���B");
        }
    }
}