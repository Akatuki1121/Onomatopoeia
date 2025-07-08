// オノマトペの種類（マスター情報）を管理するデータクラス
using UnityEngine;

[System.Serializable]
public class OnomatopeType
{
    public string type;           // オノマトペの種類名
    public Texture texture;       // オノマトペのテクスチャ
    public string animName;       // オノマトペ用アニメーション名

    public OnomatopeType(string type, Texture texture, string animName)
    {
        this.type = type;
        this.texture = texture;
        this.animName = animName;
    }
}