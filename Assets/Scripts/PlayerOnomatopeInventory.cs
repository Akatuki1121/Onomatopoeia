using System.Collections.Generic;
using UnityEngine;
// �v���C���[���������Ă���I�m�}�g�y���Ǘ�����N���X
public class PlayerOnomatopeInventory : MonoBehaviour
{
    public static PlayerOnomatopeInventory Instance { get; private set; }

    // 1��ނɂ�1�����ێ��i�������̃I�m�}�g�y�j
    private Dictionary<string, (Texture texture, string animName)> ownedOnomatope = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��ێ��������ꍇ
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �w�肵����ނ̃I�m�}�g�y���������Ă��邩
    public bool HasOnomatope(string type) => ownedOnomatope.ContainsKey(type);

    // �I�m�}�g�y��ǉ��i1��ނɂ�1�����j
    public bool AddOnomatope(string type, Texture texture, string animName)
    {
        if (string.IsNullOrEmpty(type) || texture == null)
        {
            Debug.Log("�ǉ�����I�m�}�g�y�f�[�^���s���ł��B");
            return false;
        }
        if (HasOnomatope(type))
        {
            Debug.Log($"{type}�͊��ɏ������Ă��܂��B");
            return false;
        }
        ownedOnomatope[type] = (texture, animName);
        Debug.Log($"{type}�̃I�m�}�g�y���������X�g�ɒǉ����܂����B");
        return true;
    }

    // �w�肵����ނ̃I�m�}�g�y���擾
    public (Texture texture, string animName)? GetOnomatope(string type)
    {
        if (ownedOnomatope.TryGetValue(type, out var data))
            return data;
        return null;
    }

    // �S�Ă̏����I�m�}�g�y���擾
    public IEnumerable<(string type, Texture texture, string animName)> GetAll()
    {
        foreach (var kv in ownedOnomatope)
            yield return (kv.Key, kv.Value.texture, kv.Value.animName);
    }

    // �I�m�}�g�y���폜
    public bool RemoveOnomatope(string type)
    {
        if (ownedOnomatope.Remove(type))
        {
            Debug.Log($"{type}�̃I�m�}�g�y���������X�g����폜���܂����B");
            return true;
        }
        Debug.Log($"{type}�̃I�m�}�g�y�͏������Ă��܂���B");
        return false;
    }
}

