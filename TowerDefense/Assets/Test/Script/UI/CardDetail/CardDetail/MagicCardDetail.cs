using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicCardDetail : ICardDetailBase
{
    [SerializeField, Header("説明文")]
    TextMeshProUGUI m_explanatoryNoteText;

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
        m_cardNameText.text = cardData.GetCardName();
        m_cardCostText.text = m_cardCostText.text.Replace("■", cardData.GetCastCost().ToString());
        //説明文
        m_explanatoryNoteText.text = m_explanatoryNoteText.text.Replace("■", cardData.GetMagicStatus().GetExplanatoryNote());

    }

}
