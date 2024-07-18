using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h�̏ڍ�
/// </summary>
public class ViewCardDetail : MonoBehaviour
{
    [SerializeField, Header("���j�b�g�̏ڍ׃p�l��")]
    ICardDetailBase m_unitDetailPanel;

    [SerializeField, Header("�����̏ڍ׃p�l��")]
    ICardDetailBase m_buildingDetailPanel;

    [SerializeField, Header("���@�̏ڍ׃p�l��")]
    ICardDetailBase m_magicDetailPanel;


    ICardDetailBase m_viewCardDetailPanel;   //�\������p�l��

    // Start is called before the first frame update
    void Start()
    {
        //m_unitDetailPanel.gameObject.SetActive(false);
        //m_buildingDetailPanel.gameObject.SetActive(false);
        //m_magicDetailPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// �J�[�h�̏ڍׂ�\������
    /// </summary>
    /// <param name="cardData">�\������J�[�h�̃f�[�^</param>
    public void ViewCardDataDetail(CardData cardData)
    {
        //m_viewCardDetailPanel = null;

        if(m_viewCardDetailPanel != null)
        {
            Destroy(m_viewCardDetailPanel.gameObject);
        }

        switch (cardData.GetCardType())
        {
            case CardData.EnCardType.enCardType_Unit:
                m_viewCardDetailPanel = Instantiate(m_unitDetailPanel);
                //m_unitDetailPanel.gameObject.SetActive(true);
                //m_viewCardDetailPanel.ViewCardDataDetail(cardData);
                break;
            case CardData.EnCardType.enCardType_Building:
                m_viewCardDetailPanel = Instantiate(m_buildingDetailPanel);
                //m_buildingDetailPanel.gameObject.SetActive(true);
                //m_viewCardDetailPanel.ViewCardDataDetail(cardData);
                break;
            case CardData.EnCardType.enCardType_Magic:
                m_viewCardDetailPanel = Instantiate(m_magicDetailPanel);
                //m_magicDetailPanel.gameObject.SetActive(true);
                //m_viewCardDetailPanel.ViewCardDataDetail(cardData);
                break;
            default:
                Debug.LogError("�ǂ̃^�C�v�ɂ������Ȃ��J�[�h");
                break;

        }

        m_viewCardDetailPanel.transform.SetParent(this.transform, false);

        m_viewCardDetailPanel.ViewCardDataDetail(cardData);
        

    }


}
