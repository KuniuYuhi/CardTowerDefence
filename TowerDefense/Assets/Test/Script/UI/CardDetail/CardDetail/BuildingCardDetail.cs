using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class BuildingCardDetail : ICardDetailBase
{
    [SerializeField, Header("耐久力")]
    TextMeshProUGUI m_enduranceText;

    [SerializeField, Header("影響を与える値")]
    TextMeshProUGUI m_effectPowerText;

    [SerializeField, Header("クールタイム")]
    TextMeshProUGUI m_coolTimeText;

    [SerializeField, Header("効果範囲")]
    TextMeshProUGUI m_effectRangeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// カードの詳細を表示する
    /// </summary>
    /// <param name="cardData"></param>
    public override void ViewCardDataDetail(CardData cardData)
    {
        m_cardNameText.text = cardData.GetCardName();
        m_cardCostText.text = m_cardCostText.text.Replace("■", cardData.GetCastCost().ToString());
        //耐久力
        m_enduranceText.text = m_enduranceText.text.Replace("■", cardData.GetBuildingStatus().GetEndurance().ToString());
        //影響を与える値
        m_effectPowerText.text = m_effectPowerText.text.Replace("■", cardData.GetBuildingStatus().GetEffectPower().ToString());
        //影響範囲
        float range = cardData.GetBuildingStatus().GetRadius();
        range *= 2.0f;
        m_effectRangeText.text = m_effectRangeText.text.Replace("■", range.ToString());
        //クールタイム
        m_coolTimeText.text = m_coolTimeText.text.Replace("■", cardData.GetBuildingStatus().GetIntarval().ToString());
        

    }
}
