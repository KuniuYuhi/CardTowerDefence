using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    int m_cardID;       //カード識別用のカードID

    CardDrager m_cardDrager;


    /// <summary>
    /// カードのIDを設定
    /// </summary>
    /// <param name="id"></param>
    public void SetCardID(int id)
    {
        m_cardID = id;
    }

    /// <summary>
    /// カードのIDを取得
    /// </summary>
    /// <returns></returns>
    public int GetCardID()
    {
        return m_cardID;
    }

    /// <summary>
    /// カードドラッガーを取得
    /// </summary>
    /// <returns></returns>
    public CardDrager GetCardDrager()
    {
        return m_cardDrager;
    }


    private void Awake()
    {
        m_cardDrager = GetComponent<CardDrager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
