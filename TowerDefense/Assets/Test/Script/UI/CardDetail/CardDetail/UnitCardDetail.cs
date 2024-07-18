using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitCardDetail : ICardDetailBase
{
    [SerializeField,Header("�q�b�g�|�C���g")]
    TextMeshProUGUI m_hitPointText;
    [SerializeField, Header("�ʏ�U����")]
    TextMeshProUGUI m_normalAttackPowerText;
    [SerializeField, Header("�X�L���U����")]
    TextMeshProUGUI m_skillAttackPowerText;
    [SerializeField, Header("���x")]
    TextMeshProUGUI m_speedText;
    [SerializeField, Header("�ʏ�U���̃N�[���^�C��")]
    TextMeshProUGUI m_normalAtkCTText;
    [SerializeField, Header("�X�L���U���̃N�[���^�C��")]
    TextMeshProUGUI m_skillAtkCTText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// �J�[�h�̏ڍׂ�\������
    /// </summary>
    /// <param name="cardData"></param>
    public override void ViewCardDataDetail(CardData cardData)
    {
        //�J�[�h��
        m_cardNameText.text = cardData.GetCardName();
        //�R�X�g
        m_cardCostText.text = m_cardCostText.text.Replace("��", cardData.GetCastCost().ToString());
        //�q�b�g�|�C���g
        m_hitPointText.text = m_hitPointText.text.Replace("��", cardData.GetUnitStatus().GetHp().ToString());
        //�ʏ�U����
        m_normalAttackPowerText.text = m_normalAttackPowerText.text.Replace("��",cardData.GetUnitStatus().GetNormalAttack().ToString());
        //�X�L���U����
        m_skillAttackPowerText.text = m_skillAttackPowerText.text.Replace("��", cardData.GetUnitStatus().GetSkillAttack().ToString());
        //���x
        m_speedText.text = m_speedText.text.Replace("��", cardData.GetUnitStatus().GetSpeed().ToString());
        //�ʏ�U���̃N�[���^�C��
        m_normalAtkCTText.text = m_normalAtkCTText.text.Replace("��", cardData.GetUnitStatus().GetNormalAtkIntarval().ToString());
        //�X�L���U���̃N�[���^�C��
        m_skillAtkCTText.text = m_skillAtkCTText.text.Replace("��", cardData.GetUnitStatus().GetSkillAtkIntarval().ToString());
    }

}
