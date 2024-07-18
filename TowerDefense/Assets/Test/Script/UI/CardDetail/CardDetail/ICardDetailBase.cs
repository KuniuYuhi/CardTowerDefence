using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public abstract class ICardDetailBase: MonoBehaviour
{
    [SerializeField, Header("カード名")]
    protected TextMeshProUGUI m_cardNameText;

    [SerializeField, Header("コスト")]
    protected TextMeshProUGUI m_cardCostText;

    /// <summary>
    /// カードの詳細を表示する
    /// </summary>
    public virtual void ViewCardDataDetail(CardData cardData) { }

   
}
