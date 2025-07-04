using System.Collections.Generic;
using UnityEngine;
// �v���C���[���������Ă���I�m�}�g�y���Ǘ�����N���X
public class PlayerOnomatopeInventory : MonoBehaviour
{
    public static PlayerOnomatopeInventory Instance { get; private set; }

    // 1��ނɂ�1�����ێ��i�������̃I�m�}�g�y�j
    private Dictionary<string, OnomatopeType> ownedOnomatope = new();

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
    public bool AddOnomatope(OnomatopeType data)
    {
        if (data == null || string.IsNullOrEmpty(data.type))
        {
            Debug.Log("�ǉ�����I�m�}�g�y�f�[�^���s���ł��B");
            return false;
        }
        if (HasOnomatope(data.type))
        {
            Debug.Log($"{data.type}�͊��ɏ������Ă��܂��B");
            return false;
        }
        ownedOnomatope[data.type] = data;
        Debug.Log($"{data.type}�̃I�m�}�g�y���������X�g�ɒǉ����܂����B");
        return true;
    }

    // �w�肵����ނ̃I�m�}�g�y���擾
    public OnomatopeType GetOnomatope(string type)
    {
        ownedOnomatope.TryGetValue(type, out var data);
        return data;
    }

    // �S�Ă̏����I�m�}�g�y���擾
    public IEnumerable<OnomatopeType> GetAll() => ownedOnomatope.Values;

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

