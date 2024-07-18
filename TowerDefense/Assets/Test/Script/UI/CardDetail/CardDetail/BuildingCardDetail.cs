using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class BuildingCardDetail : ICardDetailBase
{
    [SerializeField, Header("�ϋv��")]
    TextMeshProUGUI m_enduranceText;

    [SerializeField, Header("�e����^����l")]
    TextMeshProUGUI m_effectPowerText;

    [SerializeField, Header("�N�[���^�C��")]
    TextMeshProUGUI m_coolTimeText;

    [SerializeField, Header("���ʔ͈�")]
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
    /// �J�[�h�̏ڍׂ�\������
    /// </summary>
    /// <param name="cardData"></param>
    public override void ViewCardDataDetail(CardData cardData)
    {
        m_cardNameText.text = cardData.GetCardName();
        m_cardCostText.text = m_cardCostText.text.Replace("��", cardData.GetCastCost().ToString());
        //�ϋv��
        m_enduranceText.text = m_enduranceText.text.Replace("��", cardData.GetBuildingStatus().GetEndurance().ToString());
        //�e����^����l
        m_effectPowerText.text = m_effectPowerText.text.Replace("��", cardData.GetBuildingStatus().GetEffectPower().ToString());
        //�e���͈�
        float range = cardData.GetBuildingStatus().GetRadius();
        range *= 2.0f;
        m_effectRangeText.text = m_effectRangeText.text.Replace("��", range.ToString());
        //�N�[���^�C��
        m_coolTimeText.text = m_coolTimeText.text.Replace("��", cardData.GetBuildingStatus().GetIntarval().ToString());
        

    }
}
