using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitCardDetail : ICardDetailBase
{
    [SerializeField,Header("ヒットポイント")]
    TextMeshProUGUI m_hitPointText;
    [SerializeField, Header("通常攻撃力")]
    TextMeshProUGUI m_normalAttackPowerText;
    [SerializeField, Header("スキル攻撃力")]
    TextMeshProUGUI m_skillAttackPowerText;
    [SerializeField, Header("速度")]
    TextMeshProUGUI m_speedText;
    [SerializeField, Header("通常攻撃のクールタイム")]
    TextMeshProUGUI m_normalAtkCTText;
    [SerializeField, Header("スキル攻撃のクールタイム")]
    TextMeshProUGUI m_skillAtkCTText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// カードの詳細を表示する
    /// </summary>
    /// <param name="cardData"></param>
    public override void ViewCardDataDetail(CardData cardData)
    {
        //カード名
        m_cardNameText.text = cardData.GetCardName();
        //コスト
        m_cardCostText.text = m_cardCostText.text.Replace("■", cardData.GetCastCost().ToString());
        //ヒットポイント
        m_hitPointText.text = m_hitPointText.text.Replace("■", cardData.GetUnitStatus().GetHp().ToString());
        //通常攻撃力
        m_normalAttackPowerText.text = m_normalAttackPowerText.text.Replace("■",cardData.GetUnitStatus().GetNormalAttack().ToString());
        //スキル攻撃力
        m_skillAttackPowerText.text = m_skillAttackPowerText.text.Replace("■", cardData.GetUnitStatus().GetSkillAttack().ToString());
        //速度
        m_speedText.text = m_speedText.text.Replace("■", cardData.GetUnitStatus().GetSpeed().ToString());
        //通常攻撃のクールタイム
        m_normalAtkCTText.text = m_normalAtkCTText.text.Replace("■", cardData.GetUnitStatus().GetNormalAtkIntarval().ToString());
        //スキル攻撃のクールタイム
        m_skillAtkCTText.text = m_skillAtkCTText.text.Replace("■", cardData.GetUnitStatus().GetSkillAtkIntarval().ToString());
    }

}
