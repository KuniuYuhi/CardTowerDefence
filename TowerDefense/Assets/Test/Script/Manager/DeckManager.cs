using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


/// <summary>
/// �f�b�L���Ǘ�����}�l�[�W���[
/// </summary>
public class DeckManager : MonoBehaviour
{


    //�V���O���g���p�^�[��
    public static DeckManager instance { get; private set; }

    [SerializeField,Header("�g���f�b�L")]
    DeckData m_originalDeck;
    DeckData m_runtimeDeck;

    [SerializeField,Header("�L�����o�X")]
    Canvas canvas;

    [SerializeField,Header("�R�D�i�f�b�L�j�G���A")]
    RectTransform deckArea;

    [SerializeField,Header("��D�G���A")]
    RectTransform handArea;

    [SerializeField, Header("��n�G���A")]
    RectTransform graveyardArea;

    [SerializeField,Header("������D�̖���")]
    int m_initialHandCount;

    [SerializeField, Header("���_")]
    GameObject m_myBaseObject;

    [SerializeField,Header("�J�[�h�̏ڍׂ�\��")]
    ViewCardDetail m_cardDetail;


    List<GameObject> m_deckCardList = new();       //�R�D�ɂ���J�[�h���X�g
    List<GameObject> m_handCardList = new ();       //��D�ɂ���J�[�h���X�g
    List<GameObject> m_graveyardCardList = new ();       //��n�G���A�ɂ���J�[�h���X�g

    int m_maxCardCount;


    int m_setId = 0;

    int m_oldSelectCardId = -1;

    bool m_isCardDetailActive = false;          //�J�[�h�̏ڍ׃I�u�W�F�N�g���A�N�e�B�u����

    bool m_isSetUpHand = false;                 //��D�̃Z�b�g�A�b�v���ł�����


    public enum EnDrawCardMethod
    {
        enDrawCardMethod_Movable,
        enDrawCardMethod_Warp,
    }


    public bool GetSetUpHandFlag()
    {
        return m_isSetUpHand;
    }

    /// <summary>
    /// �J�[�h�̏ڍ׃I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public ViewCardDetail GetCardDetail()
    {
        return m_cardDetail;
    }

    public bool InvertCardDetailActive(CardBase cardBase)
    {
        //�O��N���b�N�����J�[�h�Ɠ���ID�łȂ����
        if(m_oldSelectCardId != cardBase.GetCardID())
        {
            m_isCardDetailActive = true;
            //�A�N�e�B�u��ݒ�
            m_cardDetail.gameObject.SetActive(m_isCardDetailActive);
            //���̃J�[�h�̈ʒu�𒲐�
            foreach(var handCard in m_handCardList)
            {
                CardBase handCardBase = handCard.GetComponent<CardBase>();
                handCardBase.GetCardDrager().ReturnCardToHandPosition();
            }
        }
        else
        {
            //�����J�[�h���N���b�N���Ă����甽�]������
            //�t���O�𔽓]
            m_isCardDetailActive = !m_isCardDetailActive;
            //�A�N�e�B�u��ݒ�
            m_cardDetail.gameObject.SetActive(m_isCardDetailActive);
        }

       


        //����I�������J�[�h��ID��O�ɑI�������J�[�h��ID�ɐݒ�
        m_oldSelectCardId = cardBase.GetCardID();

        return m_isCardDetailActive;
    }

    public void SetCardDetailActive(bool flag)
    {
        m_isCardDetailActive = flag;
        //�A�N�e�B�u��ݒ�
        m_cardDetail.gameObject.SetActive(m_isCardDetailActive);

    }


    /////////////////////////////////////////////////////////////////////////////////////////////
    ///�f�b�L�Ɋւ��鏈��
    /////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// �ŏ��̃f�b�L�̏��X�̐ݒ�
    /// </summary>
    public void StartDeckAndCardSetting()
    {
        //�f�b�L�̃J�[�h��ID�Ɛe�I�u�W�F�N�g��ݒ肵�Ă���
        foreach(var card in m_runtimeDeck.cardPrefabsList)
        {
            //�f�b�L�̃J�[�h�𐶐�
            GameObject cardInstance = Instantiate(card);
            //�f�b�L�G���A�̎q�I�u�W�F�N�g�ɐݒ�
            cardInstance.transform.SetParent(deckArea.transform, false);
            //�J�[�h��ID��ݒ�
            SetCardIdInDeck(cardInstance);
            //�f�b�L���X�g�ɒǉ�
            m_deckCardList.Add(cardInstance);
        }

        //�f�b�L���V���b�t��
        DeckListShuffle();
        //�f�b�L�̍ő喇�����擾
        m_maxCardCount = m_deckCardList.Count;


        //��ʉ��ɏ�����D�̐����J�[�h��ݒu
        //�ړ��s�\
        //�z�u���@�͈ړ��^
        DrawCardFromDeck(m_initialHandCount,true,EnDrawCardMethod.enDrawCardMethod_Movable);

       
    }



    /// <summary>
    /// �J�[�h���f�b�L�ɒǉ�
    /// </summary>
    /// <param name="addCardPrefab">�ǉ�����J�[�h�v���t�@�u</param>
    public void AddCardInDeckList(GameObject addCardPrefab)
    {
        //��n�G���A�I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɕύX
        addCardPrefab.transform.SetParent(deckArea, false);
        //�f�b�L���X�g�ɃJ�[�h��ǉ�
        m_deckCardList.Add(addCardPrefab);
        //�f�b�L�̍ő喇�����擾
        m_maxCardCount = m_deckCardList.Count;


    }

    /// <summary>
    /// �J�[�h���n�G���A�ɑ���
    /// </summary>
    /// <param name="sendCardPrefab">��n�ɑ���J�[�h�v���t�@�u</param>
    public void SendCardToGraveyard(GameObject sendCardPrefab)
    {
        //��n�ɑ������J�[�h���A�N�e�B�u������
        sendCardPrefab.SetActive(false);
        //�ړ����������W�����Z�b�g
        sendCardPrefab.transform.localPosition = Vector3.zero;
        //��n�G���A�I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɕύX
        sendCardPrefab.transform.SetParent(graveyardArea, false);

        //�폜�����J�[�h���n�J�[�h���X�g�ɒǉ�����
        m_graveyardCardList.Add(sendCardPrefab);
        //��D���X�g�����n�ɑ������J�[�h���폜
        if(m_handCardList.Remove(sendCardPrefab))
        {
            //������
            //��D�̃J�[�h���ꖇ�������̂ŁA�f�b�L����ꖇ�J�[�h������
            DrawCardFromDeck(1,true);
        }
        else
        {
            //�����Ȃ�����
            Debug.LogError("�J�[�h���폜�o���Ȃ�����");
            return;
        }
    }

    /// <summary>
    /// �f�b�L�̃J�[�h�̃J�[�hID��ݒ肷��
    /// </summary>
    void SetCardIdInDeck(GameObject card)
    {
        CardBase cardBase = card.GetComponent<CardBase>();
        if(cardBase == null)
        {
            Debug.LogError("�J�[�h�ł͂Ȃ�");
            return;
        }
        //ID��ݒ�
        cardBase.SetCardID(m_setId);
        //����ID�ɂ��Ă���
        m_setId++;
    }

    void DeepCopyDeckData()
    {
        //�I���W�i���̃f�b�L�f�[�^�������^�C���p�̃f�b�L�f�[�^�ɐ����A���
        m_runtimeDeck = ScriptableObject.CreateInstance<DeckData>();
        //�I���W�i���̃J�[�h���X�g�̔z��̐����̃��X�g�𐶐�
        m_runtimeDeck.cardPrefabsList =
            new List<GameObject>(this.m_originalDeck.cardPrefabsList.Count);
        //�I���W�i���f�[�^�̃J�[�h���X�g�������^�C���̃J�[�h���X�g�ɒǉ�����
        foreach (var card in m_originalDeck.cardPrefabsList)
        {
            m_runtimeDeck.cardPrefabsList.Add(card);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DeepCopyDeckData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ID�̏�����
        m_setId = 0;

        //�J�[�h�̏ڍׂ͔�\��
        m_cardDetail.gameObject.SetActive(m_isCardDetailActive);

        //�ŏ��̃J�[�h�̃Z�b�e�B���O
        StartDeckAndCardSetting();
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        

    }

    /// <summary>
    /// �f�b�L���X�g�̗v�f���V���b�t������
    /// </summary>
    void DeckListShuffle()
    {
        for (int i = 0; i < m_deckCardList.Count; i++)
        {
            var temp = m_deckCardList[i];
            //���݂̃J�[�h�̔ԍ����烊�X�g�̑������烉���_���Ȕԍ����擾
            int randomIndex = Random.Range(i, m_deckCardList.Count);
            //�Ώۂ̃J�[�h�ƃ����_���ɑI�΂ꂽ�J�[�h�Ɠ���ւ���
            m_deckCardList[i] = m_deckCardList[randomIndex];
            //�����_���ɑI�΂ꂽ�J�[�h�Ɋm�ۂ��Ă������J�[�h����
            m_deckCardList[randomIndex] = temp;
        }
    }

    /// <summary>
    /// ��n�G���A����J�[�h���f�b�L�ɖ߂�
    /// </summary>
    void ReclaimCardFromGraveyard()
    {
        //��n�������ł��Ȃ�������

        foreach(var graveyardCard in m_graveyardCardList)
        {
            //��A�N�e�B�u������Ă���Ȃ�
            if(graveyardCard.activeSelf==false)
            {
                //�A�N�e�B�u������
                graveyardCard.SetActive(true);
            }

            //�R�D�ɓ���̂ŁA�ړ������Ȃǂ��ł��Ȃ��悤�ɂ���
            //���������J�[�h�ƃf�b�L�̃J�[�h��CardBase�R���|�[�l���g���擾
            CardBase cardBase = graveyardCard.GetComponent<CardBase>();
            //�J�[�h���ړ��ł��Ȃ��悤�ɂ���
            cardBase.GetCardDrager().SetMovableFlag(false);

            //��n�̃J�[�h���f�b�L���X�g�ɒǉ�(�߂��Ă���)
            AddCardInDeckList(graveyardCard);
        }
        //��n�̃J�[�h���X�g���N���A����
        m_graveyardCardList.Clear();
        //�߂�����V���b�t��
        DeckListShuffle();
    }

    /// <summary>
    /// �f�b�L����J�[�h�������Ď�D�ɉ�����
    /// </summary>
    /// <param name="drawAmount">�h���[����J�[�h�̖���</param>
    /// <param name="movableFlag">�J�[�h�ړ��\�t���O</param>
    /// <param name="drawCardMethod">�J�[�h���h���[����ۂ̕��@</param>
    public void DrawCardFromDeck(
        int drawAmount = 1, 
        bool movableFlag = false, 
        EnDrawCardMethod drawCardMethod = EnDrawCardMethod.enDrawCardMethod_Warp)
    {
        //
        for (int i = 0; i < drawAmount; i++)
        {
            if(m_deckCardList.Count== 0)
            {
                Debug.Log("��n����J�[�h�����");
                ReclaimCardFromGraveyard();
            }

            //�f�b�L���X�g�����D���X�g�ɃJ�[�h��������
            m_handCardList.Add(m_deckCardList[0]);
            //��D�ɃJ�[�h���n�����̂ŁA�f�b�L���X�g���瓯���J�[�h���폜
            m_deckCardList.Remove(m_deckCardList[0]);

            GameObject cardInstance = m_handCardList[m_handCardList.Count - 1];

            //��D�̃J�[�h�̐e�I�u�W�F�N�g��ύX
            cardInstance.transform.SetParent(handArea.transform, true);
            //���������J�[�h�ƃf�b�L�̃J�[�h��CardBase�R���|�[�l���g���擾
            CardBase cardBase = cardInstance.GetComponent<CardBase>();
            //�J�[�h�h���b�K�[�̏��������������s
            cardBase.GetCardDrager().Init(canvas, m_myBaseObject);
        }

        //��D�G���A�ɃJ�[�h��z�u
        PlaceCardInHand(drawCardMethod, movableFlag);
    }


    /// <summary>
    /// �J�[�h����D�G���A�ɔz�u����
    /// </summary>
    async void PlaceCardInHand(EnDrawCardMethod drawCardMethod,bool flag)
    {
        m_isSetUpHand = false;

        //��D�G���A�̕�����D�̖����Ŋ����ăJ�[�h�̊Ԋu���v�Z
        float distance = handArea.rect.width / m_handCardList.Count;
        //��D�G���A�̔����̕��@�\�@�J�[�h�̊Ԋu/2�ōŏ��̃J�[�h�̊J�n�ʒu���v�Z
        float startPosX = (handArea.rect.width / 2.0f) - (distance / 2.0f);
        float subDistance = 0.0f;

        foreach (var card in m_handCardList)
        {
            CardBase cardBase = card.GetComponent<CardBase>();

            if(drawCardMethod==EnDrawCardMethod.enDrawCardMethod_Movable)
            {
                //�J�[�h����D�̒�ʒu�Ɉړ�������
                cardBase.GetCardDrager().MoveCard(new Vector2(subDistance - startPosX, 0.0f));
                //��b�҂��ă��[�v�ĊJ
                await Task.Delay(100);
            }
            else
            {
                //���ڈړ�������Ȃ�
                cardBase.GetCardDrager().SetRectTransform(Vector2.zero);
                cardBase.GetCardDrager().SumRectTransform(new Vector2(subDistance - startPosX, 0.0f));
                //��b�҂��ă��[�v�ĊJ
                await Task.Delay(50);
            }

            //��D�ł̏����ʒu���J�[�h�̓��B�n�_�̍��W�ɐݒ�
            cardBase.GetCardDrager().ChangeHandPositionToNowPosition(new Vector2(subDistance - startPosX, 0.0f));

            //��D�ɓ������̂ŁA�J�[�h�𓮂�����悤�ɂ���
            cardBase.GetCardDrager().SetMovableFlag(flag);

            //���̃J�[�h�̋����ɂ���
            subDistance += distance;

            

        }

        //��D�ɃJ�[�h��ݒu����
        m_isSetUpHand = true;

    }




    /// <summary>
    /// ��D�̃J�[�h�̈ړ��\�t���O��ݒ�
    /// </summary>
    /// <param name="flag"></param>
    public void SetHandCardsMovableFlag(bool flag)
    {
        foreach (var card in m_handCardList)
        {
            CardBase cardBase = card.GetComponent<CardBase>();
            cardBase.GetCardDrager().SetMovableFlag(flag);
        }
    }



}
