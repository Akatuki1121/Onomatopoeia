using System.Collections.Generic;
using UnityEngine;
// プレイヤーが所持しているオノマトペを管理するクラス
public class PlayerOnomatopeInventory : MonoBehaviour
{
    public static PlayerOnomatopeInventory Instance { get; private set; }

    // 1種類につき1つだけ保持（所持中のオノマトペ）
    private Dictionary<string, (Texture texture, string animName)> ownedOnomatope = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも保持したい場合
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 指定した種類のオノマトペを所持しているか
    public bool HasOnomatope(string type) => ownedOnomatope.ContainsKey(type);

    // オノマトペを追加（1種類につき1つだけ）
    public bool AddOnomatope(string type, Texture texture, string animName)
    {
        if (string.IsNullOrEmpty(type) || texture == null)
        {
            Debug.Log("追加するオノマトペデータが不正です。");
            return false;
        }
        if (HasOnomatope(type))
        {
            Debug.Log($"{type}は既に所持しています。");
            return false;
        }
        ownedOnomatope[type] = (texture, animName);
        Debug.Log($"{type}のオノマトペを所持リストに追加しました。");
        return true;
    }

    // 指定した種類のオノマトペを取得
    public (Texture texture, string animName)? GetOnomatope(string type)
    {
        if (ownedOnomatope.TryGetValue(type, out var data))
            return data;
        return null;
    }

    // 全ての所持オノマトペを取得
    public IEnumerable<(string type, Texture texture, string animName)> GetAll()
    {
        foreach (var kv in ownedOnomatope)
            yield return (kv.Key, kv.Value.texture, kv.Value.animName);
    }

    // オノマトペを削除
    public bool RemoveOnomatope(string type)
    {
        if (ownedOnomatope.Remove(type))
        {
            Debug.Log($"{type}のオノマトペを所持リストから削除しました。");
            return true;
        }
        Debug.Log($"{type}のオノマトペは所持していません。");
        return false;
    }
}

