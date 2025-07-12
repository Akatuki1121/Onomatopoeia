using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// プレイヤーが記憶したオノマトペの種類だけを管理するクラス
public class PlayerOnomatopeInventory : MonoBehaviour
{
    public static PlayerOnomatopeInventory Instance { get; private set; }

    // ただ種類だけを保持
    private HashSet<string> ownedOnomatope = new();

    // UI や他システムが購読できるイベント
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

    // 記憶済みかだけチェック
    public bool HasOnomatope(string type)
    {
        return ownedOnomatope.Contains(type);
    }

    /// <summary>
    /// オノマトペの種類を追加しようとする。
    /// type が null/空、あるいは既に存在する場合は失敗し、
    /// out で理由を返す。
    /// </summary>
    public bool TryAddOnomatope(string type, out string errorMessage)
    {
        errorMessage = null;

        // 1) 入力チェック
        if (string.IsNullOrWhiteSpace(type))
        {
            errorMessage = "オノマトペの種類が指定されていません。";
            Debug.LogError(errorMessage);
            OnOnomatopeAddFailed?.Invoke(errorMessage);
            return false;
        }

        // 2) 重複チェック
        if (ownedOnomatope.Contains(type))
        {
            errorMessage = $"{type} は既に記憶済みです。";
            Debug.LogWarning(errorMessage);
            OnOnomatopeAddFailed?.Invoke(errorMessage);
            return false;
        }

        // 成功時
        ownedOnomatope.Add(type);
        Debug.Log($"{type} を記憶リストに追加しました。");
        OnOnomatopeAdded?.Invoke(type);
        return true;
    }

    // 記憶済みの全種類を取得
    public IEnumerable<string> GetAll()
    {
        foreach (var type in ownedOnomatope)
            yield return type;
    }

    // 削除処理
    public bool RemoveOnomatope(string type)
    {
        if (ownedOnomatope.Remove(type))
        {
            Debug.Log($"{type} を記憶リストから削除しました。");
            return true;
        }
        Debug.LogWarning($"{type} は記憶されていません。");
        return false;
    }
}