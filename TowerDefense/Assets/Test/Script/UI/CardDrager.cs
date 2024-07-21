using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

public class CardDrager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField,Header("カードデータ")]
    CardData m_cardData;
    
    [SerializeField,Header("レイの判定をとりたいレイヤー")]
    LayerMask layerMask;

    [SerializeField,Header("自分の拠点")]
    GameObject myBaseObject;

    CardBase cardBase;


    float m_selectYUp = 20.0f;

    private RectTransform rectTransform;        //カードの座標

    private Vector2 m_handPosition; //カードの初期座標

    private CanvasGroup canvasGroup;
    private Canvas m_canvas;

    GameObject previewInstance;                 //プレビューモデル



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
    /// キャンバスを設定
    /// </summary>
    /// <param name="canvas"></param>
    public void SetCanvas(Canvas canvas)
    {
        m_canvas = canvas;
    }
    /// <summary>
    /// 拠点を設定
    /// </summary>
    /// <param name="baseObject"></param>
    public void SetMyBaseObject(GameObject baseObject)
    {
        myBaseObject = baseObject;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="canvas">キャンバス</param>
    /// <param name="baseObject">拠点オブジェクト</param>
    public void Init(Canvas canvas, GameObject baseObject)
    {
        m_canvas = canvas;
        myBaseObject = baseObject;
    }

    /// <summary>
    /// 初期位置を設定
    /// </summary>
    /// <param name="position"></param>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;
    }
    /// <summary>
    /// 手札での自分定位置を切り替える
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
    /// カードを手札の初期位置に戻す
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
    /// ドラッグ時に見せるオブジェクトをカードから設定
    /// </summary>
    void SetPreviewInstance()
    {
        previewInstance = Instantiate(m_cardData.GetPreviewModel());
        //ドラッグ時だけ見せるので非アクティブ化する
        previewInstance.SetActive(false);
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
       
        //プレビュー用インスタンスを設定
        SetPreviewInstance();
    }

    /// <summary>
    /// クリックした時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //動かせない状態なら処理しない
        if (!m_isMovable) return;

        //クリックするたび、アクティブ化を反転させる
        if (DeckManager.instance.InvertCardDetailActive(cardBase))
        {
            //アクティブ化した時ならカードの詳細を見せる
            DeckManager.instance.GetCardDetail().ViewCardDataDetail(m_cardData);

            //どのカードか分かるようにカードの位置を少し上げる

            rectTransform.anchoredPosition += new Vector2(0.0f, m_selectYUp); 
        }
        else
        {
            rectTransform.anchoredPosition = m_handPosition;
        }
    }

    /// <summary>
    /// ドラッグ開始
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        //動かせない状態なら処理しない
        if (!m_isMovable) return;

        //カードを持ったら背景が見えるように透明にする
        canvasGroup.alpha = 0.2f;
        //レイをブロックしない。
        //ドラッグしているオブジェクトが
        //他のUI要素に対する入力を妨げないようにする
        canvasGroup.blocksRaycasts = false;
        //プレビューモデルをアクティブ化
        previewInstance.SetActive(true);
        //ドラッグ開始位置を保存
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //動かせない状態なら処理しない
        if (!m_isMovable) return;

        //ドラッグしている間座標を動かす
        rectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor;
        //Debug.Log(rectTransform.anchoredPosition);
        //プレビューモデルを表示する処理
        ViewPreviewModel();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //動かせない状態なら処理しない
        if (!m_isMovable) return;

        //ドロップしたら透明度を元に戻す
        canvasGroup.alpha = 1.0f;
        //レイをブロック。自分にあたるようにする
        canvasGroup.blocksRaycasts = true;
        //プレビューモデルを非アクティブ化する
        previewInstance.SetActive(false);
        //カードのオブジェクトを生成
        CreateCardObject();
    }


    void ViewPreviewModel()
    {
        // カードの中心からレイを作成
        Ray ray = Camera.main.ScreenPointToRay(rectTransform.position);
        RaycastHit hit;
        //レイを飛ばして特定のレイヤーに当たったか確認する
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            previewInstance.SetActive(true);

            if (previewInstance != null)
            {
                //モデルの座標をレイが当たった場所に設定
                previewInstance.transform.position = hit.point;
            }
        }
        else
        {
            //当っていないのでモデルが映らないようにする
            previewInstance.SetActive(false);
        }
    }

    /// <summary>
    /// カードオブジェクトを生成
    /// </summary>
    void CreateCardObject()
    {
        // カードの中心からレイを作成
        Ray ray = Camera.main.ScreenPointToRay(rectTransform.position);
        // レイキャストの結果を格納するためのRaycastHit
        RaycastHit hit;

        // 特定のレイヤーに対してレイキャストを行う
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // レイヤーに当たった場合の処理
            //Debug.Log("Hit " + hit.collider.name + " at " + hit.point);

            //カードに設定されているオブジェクトを生成
            GameObject card = Instantiate(m_cardData.GetObjectPrefab(), hit.point, Quaternion.identity);
            //拠点を設定
            SpawnableBase castUnit = card.GetComponent<SpawnableBase>();
            castUnit.SetBaseObject(myBaseObject);
            //カードを墓地に送る
            DeckManager.instance.SendCardToGraveyard(gameObject);

            //カードの詳細が開かれているかもしれないので、強制的に非表示にする
            DeckManager.instance.SetCardDetailActive(false);
        }
        else
        {
            // レイヤーに当たらなかった場合の処理
            Debug.Log("No hit");
            //生成されなかったので定位置に戻す
            rectTransform.anchoredPosition = startPosition;
        }
    }



    public void MoveCard(Vector2 endPosiiton)
    {
        //rectTransform.DOMove(endPosiiton, 1.0f);

        rectTransform.DOAnchorPos(endPosiiton, 1.0f);
    }


}
