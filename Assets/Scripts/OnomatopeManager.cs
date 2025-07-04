using System.Collections.Generic;
using UnityEngine;
// プレイヤーが所持しているオノマトペを管理するクラス
public class PlayerOnomatopeInventory : MonoBehaviour
{
    public static PlayerOnomatopeInventory Instance { get; private set; }

    // 1種類につき1つだけ保持（所持中のオノマトペ）
    private Dictionary<string, OnomatopeType> ownedOnomatope = new();

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
    public bool AddOnomatope(OnomatopeType data)
    {
        if (data == null || string.IsNullOrEmpty(data.type))
        {
            Debug.Log("追加するオノマトペデータが不正です。");
            return false;
        }
        if (HasOnomatope(data.type))
        {
            Debug.Log($"{data.type}は既に所持しています。");
            return false;
        }
        ownedOnomatope[data.type] = data;
        Debug.Log($"{data.type}のオノマトペを所持リストに追加しました。");
        return true;
    }

    // 指定した種類のオノマトペを取得
    public OnomatopeType GetOnomatope(string type)
    {
        ownedOnomatope.TryGetValue(type, out var data);
        return data;
    }

    // 全ての所持オノマトペを取得
    public IEnumerable<OnomatopeType> GetAll() => ownedOnomatope.Values;

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

