using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Deck/CardData")]
public class CardData : ScriptableObject
{
    [SerializeField, Header("カードのコスト")]
    int castCost;

    [SerializeField, Header("カードのオブジェクト")]
    GameObject objectPrefab;

    [SerializeField, Header("モデルのプレビュー")]
    GameObject previewModel;

    [SerializeField, Header("カード名")]
    string cardName;

    [SerializeField, Header("カードのステータス。適したものをセット")]
    Status_Character unitStatus;
    [SerializeField]
    Status_Building buildingStatus;
    [SerializeField]
    Status_Magic magicStatus;

    /// <summary>
    /// カードのタイプ
    /// </summary>
    public enum EnCardType
    {
        enCardType_Unit,
        enCardType_Building,
        enCardType_Magic
    };

    [SerializeField, Header("カードのタイプ")]
    EnCardType enCardType;


    /// <summary>
    /// 詠唱コストを取得
    /// </summary>
    /// <returns></returns>
    public int GetCastCost()
    {
        return castCost;
    }

    /// <summary>
    /// 召喚するプレファブを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectPrefab()
    {
        return objectPrefab;
    }

    /// <summary>
    /// プレビューモデルの取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetPreviewModel()
    {
        return previewModel;
    }

    /// <summary>
    /// カードの名前を取得
    /// </summary>
    /// <returns></returns>
    public string GetCardName()
    {
        return cardName;
    }

    /// <summary>
    /// カードのタイプを取得
    /// </summary>
    /// <returns></returns>
    public EnCardType GetCardType()
    {
        return enCardType;
    }

    /// <summary>
    /// ユニットのステータスを取得
    /// </summary>
    /// <returns></returns>
    public Status_Character GetUnitStatus()
    {
        return unitStatus;
    }
    /// <summary>
    /// 建物のステータスを取得
    /// </summary>
    /// <returns></returns>
    public Status_Building GetBuildingStatus()
    {
        return buildingStatus;
    }
    /// <summary>
    /// 魔法のステータスを取得
    /// </summary>
    /// <returns></returns>
    public Status_Magic GetMagicStatus()
    {
        return magicStatus;
    }

}
