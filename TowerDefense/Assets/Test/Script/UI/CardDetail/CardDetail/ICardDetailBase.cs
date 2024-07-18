using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public abstract class ICardDetailBase: MonoBehaviour
{
    [SerializeField, Header("�J�[�h��")]
    protected TextMeshProUGUI m_cardNameText;

    [SerializeField, Header("�R�X�g")]
    protected TextMeshProUGUI m_cardCostText;

    /// <summary>
    /// �J�[�h�̏ڍׂ�\������
    /// </summary>
    public virtual void ViewCardDataDetail(CardData cardData) { }

   
}
