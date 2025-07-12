using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// �v���C���[���L�������I�m�}�g�y�̎�ނ������Ǘ�����N���X
public class PlayerOnomatopeInventory : MonoBehaviour
{
    public static PlayerOnomatopeInventory Instance { get; private set; }

    // ������ނ�����ێ�
    private HashSet<string> ownedOnomatope = new();

    // UI �⑼�V�X�e�����w�ǂł���C�x���g
    [Serializable]
    public class OnomatopeEvent : UnityEvent<string> { }
    public OnomatopeEvent OnOnomatopeAdded;
    public OnomatopeEvent OnOnomatopeAddFailed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �L���ς݂������`�F�b�N
    public bool HasOnomatope(string type)
    {
        return ownedOnomatope.Contains(type);
    }

    /// <summary>
    /// �I�m�}�g�y�̎�ނ�ǉ����悤�Ƃ���B
    /// type �� null/��A���邢�͊��ɑ��݂���ꍇ�͎��s���A
    /// out �ŗ��R��Ԃ��B
    /// </summary>
    public bool TryAddOnomatope(string type, out string errorMessage)
    {
        errorMessage = null;

        // 1) ���̓`�F�b�N
        if (string.IsNullOrWhiteSpace(type))
        {
            errorMessage = "�I�m�}�g�y�̎�ނ��w�肳��Ă��܂���B";
            Debug.LogError(errorMessage);
            OnOnomatopeAddFailed?.Invoke(errorMessage);
            return false;
        }

        // 2) �d���`�F�b�N
        if (ownedOnomatope.Contains(type))
        {
            errorMessage = $"{type} �͊��ɋL���ς݂ł��B";
            Debug.LogWarning(errorMessage);
            OnOnomatopeAddFailed?.Invoke(errorMessage);
            return false;
        }

        // ������
        ownedOnomatope.Add(type);
        Debug.Log($"{type} ���L�����X�g�ɒǉ����܂����B");
        OnOnomatopeAdded?.Invoke(type);
        return true;
    }

    // �L���ς݂̑S��ނ��擾
    public IEnumerable<string> GetAll()
    {
        foreach (var type in ownedOnomatope)
            yield return type;
    }

    // �폜����
    public bool RemoveOnomatope(string type)
    {
        if (ownedOnomatope.Remove(type))
        {
            Debug.Log($"{type} ���L�����X�g����폜���܂����B");
            return true;
        }
        Debug.LogWarning($"{type} �͋L������Ă��܂���B");
        return false;
    }
}