// �I�m�}�g�y�̎�ށi�}�X�^�[���j���Ǘ�����f�[�^�N���X
using UnityEngine;

[System.Serializable]
public class OnomatopeType
{
    public string type;           // �I�m�}�g�y�̎�ޖ�
    public Texture texture;       // �I�m�}�g�y�̃e�N�X�`��
    public string animName;       // �I�m�}�g�y�p�A�j���[�V������

    public OnomatopeType(string type, Texture texture, string animName)
    {
        this.type = type;
        this.texture = texture;
        this.animName = animName;
    }
}