using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

public class CardDrager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField,Header("�J�[�h�f�[�^")]
    CardData m_cardData;
    
    [SerializeField,Header("���C�̔�����Ƃ肽�����C���[")]
    LayerMask layerMask;

    [SerializeField,Header("�����̋��_")]
    GameObject myBaseObject;

    CardBase cardBase;


    float m_selectYUp = 20.0f;

    private RectTransform rectTransform;        //�J�[�h�̍��W

    private Vector2 m_handPosition; //�J�[�h�̏������W

    private CanvasGroup canvasGroup;
    private Canvas m_canvas;

    GameObject previewInstance;                 //�v���r���[���f��



    private Vector2 startPosition;

    Vector2 deadPosition = new(-340.0f,-160.0f);

    bool m_isMovable = false;



    public void SetMovableFlag(bool flag)
    {
        m_isMovable = flag;
    }

    public bool GetMovableFlag()
    {
        return m_isMovable;
    }

    public void SetRectTransform(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public void SumRectTransform(Vector2 position)
    {
        rectTransform.anchoredPosition += position;
    }


    /// <summary>
    /// �L�����o�X��ݒ�
    /// </summary>
    /// <param name="canvas"></param>
    public void SetCanvas(Canvas canvas)
    {
        m_canvas = canvas;
    }
    /// <summary>
    /// ���_��ݒ�
    /// </summary>
    /// <param name="baseObject"></param>
    public void SetMyBaseObject(GameObject baseObject)
    {
        myBaseObject = baseObject;
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="canvas">�L�����o�X</param>
    /// <param name="baseObject">���_�I�u�W�F�N�g</param>
    public void Init(Canvas canvas, GameObject baseObject)
    {
        m_canvas = canvas;
        myBaseObject = baseObject;
    }

    /// <summary>
    /// �����ʒu��ݒ�
    /// </summary>
    /// <param name="position"></param>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;
    }
    /// <summary>
    /// ��D�ł̎�����ʒu��؂�ւ���
    /// </summary>
    public void ChangeHandPositionToNowPosition()
    {
        m_handPosition = rectTransform.anchoredPosition;
    }
    public void ChangeHandPositionToNowPosition(Vector2 position)
    {
        m_handPosition = position;
    }


    /// <summary>
    /// �J�[�h����D�̏����ʒu�ɖ߂�
    /// </summary>
    public void ReturnCardToHandPosition()
    {
        rectTransform.anchoredPosition = m_handPosition;
    }

    private void Start()
    {
        cardBase = GetComponent<CardBase>();
    }


    /// <summary>
    /// �h���b�O���Ɍ�����I�u�W�F�N�g���J�[�h����ݒ�
    /// </summary>
    void SetPreviewInstance()
    {
        previewInstance = Instantiate(m_cardData.GetPreviewModel());
        //�h���b�O������������̂Ŕ�A�N�e�B�u������
        previewInstance.SetActive(false);
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
       
        //�v���r���[�p�C���X�^���X��ݒ�
        SetPreviewInstance();
    }

    /// <summary>
    /// �N���b�N�������̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //�������Ȃ���ԂȂ珈�����Ȃ�
        if (!m_isMovable) return;

        //�N���b�N���邽�сA�A�N�e�B�u���𔽓]������
        if (DeckManager.instance.InvertCardDetailActive(cardBase))
        {
            //�A�N�e�B�u���������Ȃ�J�[�h�̏ڍׂ�������
            DeckManager.instance.GetCardDetail().ViewCardDataDetail(m_cardData);

            //�ǂ̃J�[�h��������悤�ɃJ�[�h�̈ʒu�������グ��

            rectTransform.anchoredPosition += new Vector2(0.0f, m_selectYUp); 
        }
        else
        {
            rectTransform.anchoredPosition = m_handPosition;
        }
    }

    /// <summary>
    /// �h���b�O�J�n
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //�������Ȃ���ԂȂ珈�����Ȃ�
        if (!m_isMovable) return;

        //�J�[�h����������w�i��������悤�ɓ����ɂ���
        canvasGroup.alpha = 0.2f;
        //���C���u���b�N���Ȃ��B
        //�h���b�O���Ă���I�u�W�F�N�g��
        //����UI�v�f�ɑ΂�����͂�W���Ȃ��悤�ɂ���
        canvasGroup.blocksRaycasts = false;
        //�v���r���[���f�����A�N�e�B�u��
        previewInstance.SetActive(true);
        //�h���b�O�J�n�ʒu��ۑ�
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�������Ȃ���ԂȂ珈�����Ȃ�
        if (!m_isMovable) return;

        //�h���b�O���Ă���ԍ��W�𓮂���
        rectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor;
        //Debug.Log(rectTransform.anchoredPosition);
        //�v���r���[���f����\�����鏈��
        ViewPreviewModel();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //�������Ȃ���ԂȂ珈�����Ȃ�
        if (!m_isMovable) return;

        //�h���b�v�����瓧���x�����ɖ߂�
        canvasGroup.alpha = 1.0f;
        //���C���u���b�N�B�����ɂ�����悤�ɂ���
        canvasGroup.blocksRaycasts = true;
        //�v���r���[���f�����A�N�e�B�u������
        previewInstance.SetActive(false);
        //�J�[�h�̃I�u�W�F�N�g�𐶐�
        CreateCardObject();
    }


    void ViewPreviewModel()
    {
        // �J�[�h�̒��S���烌�C���쐬
        Ray ray = Camera.main.ScreenPointToRay(rectTransform.position);
        RaycastHit hit;
        //���C���΂��ē���̃��C���[�ɓ����������m�F����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            previewInstance.SetActive(true);

            if (previewInstance != null)
            {
                //���f���̍��W�����C�����������ꏊ�ɐݒ�
                previewInstance.transform.position = hit.point;
            }
        }
        else
        {
            //�����Ă��Ȃ��̂Ń��f�����f��Ȃ��悤�ɂ���
            previewInstance.SetActive(false);
        }
    }

    /// <summary>
    /// �J�[�h�I�u�W�F�N�g�𐶐�
    /// </summary>
    void CreateCardObject()
    {
        // �J�[�h�̒��S���烌�C���쐬
        Ray ray = Camera.main.ScreenPointToRay(rectTransform.position);
        // ���C�L���X�g�̌��ʂ��i�[���邽�߂�RaycastHit
        RaycastHit hit;

        // ����̃��C���[�ɑ΂��ă��C�L���X�g���s��
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // ���C���[�ɓ��������ꍇ�̏���
            //Debug.Log("Hit " + hit.collider.name + " at " + hit.point);

            //�J�[�h�ɐݒ肳��Ă���I�u�W�F�N�g�𐶐�
            GameObject card = Instantiate(m_cardData.GetObjectPrefab(), hit.point, Quaternion.identity);
            //���_��ݒ�
            SpawnableBase castUnit = card.GetComponent<SpawnableBase>();
            castUnit.SetBaseObject(myBaseObject);
            //�J�[�h���n�ɑ���
            DeckManager.instance.SendCardToGraveyard(gameObject);

            //�J�[�h�̏ڍׂ��J����Ă��邩������Ȃ��̂ŁA�����I�ɔ�\���ɂ���
            DeckManager.instance.SetCardDetailActive(false);
        }
        else
        {
            // ���C���[�ɓ�����Ȃ������ꍇ�̏���
            Debug.Log("No hit");
            //��������Ȃ������̂Œ�ʒu�ɖ߂�
            rectTransform.anchoredPosition = startPosition;
        }
    }



    public void MoveCard(Vector2 endPosiiton)
    {
        //rectTransform.DOMove(endPosiiton, 1.0f);

        rectTransform.DOAnchorPos(endPosiiton, 1.0f);
    }


}
