using UnityEngine;

// �I�m�}�g�y�I�u�W�F�N�g�̃C���^���N�g��e�N�X�`���E�����o��UI�Ǘ����s���X�N���v�g
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string onomatopeType;     // �I�m�}�g�y��ʁiInspector�Őݒ�j
    [SerializeField] private Texture onomatopeTexture; // �I�m�}�g�y�p�e�N�X�`��
    [SerializeField] private Texture defaultTexture;   // �f�t�H���g�e�N�X�`��
    private GameObject currentBubble;                  // ���ݕ\�����̐����o��UI

    // �O������Q�Ƃł���v���p�e�B
    public string OnomatopeType => onomatopeType;
    public Texture OnomatopeTexture => onomatopeTexture;

    /// <summary>
    /// �I�m�}�g�y���������Ƃ��ɌĂԁB
    /// ���݂̃}�e���A���� defaultTexture �ɖ߂��A
    /// onomatopeTexture ��Ԃ��B
    /// </summary>
    public Texture TakeTexture()
    {
        if (TryGetComponent<Renderer>(out var rend))
        {
            rend.material.mainTexture = defaultTexture != null
                ? defaultTexture
                : rend.material.mainTexture;
            return onomatopeTexture;
        }

        Debug.LogError("Renderer ��������܂���B");
        return null;
    }

    /// <summary>
    /// �w��̃e�N�X�`����\��t����iAttach ���Ɏg�p�j
    /// </summary>
    public void ApplyTexture(Texture texture)
    {
        if (texture == null)
        {
            Debug.LogError("�\��t����e�N�X�`���� null �ł��B");
            return;
        }

        if (TryGetComponent<Renderer>(out var rend))
        {
            rend.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Renderer ��������܂���B");
        }
    }


    /// <summary>
    /// �����o����\������ibubblePrefab �͊O������n���j
    /// </summary>
    public void ShowBubble(GameObject bubblePrefab)
    {
        if (currentBubble == null && bubblePrefab != null)
        {
            currentBubble = Instantiate(
                bubblePrefab,
                transform.position + (Vector3.up * 1.5f),
                Quaternion.identity,
                transform
            );
        }
    }

    /// <summary>
    /// �����o�����\���ɂ���
    /// </summary>
    public void HideBubble()
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
            currentBubble = null;
        }
    }
}