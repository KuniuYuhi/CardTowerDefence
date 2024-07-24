using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


/// <summary>
/// デッキを管理するマネージャー
/// </summary>
public class DeckManager : MonoBehaviour
{


    //シングルトンパターン
    public static DeckManager instance { get; private set; }

    [SerializeField,Header("使うデッキ")]
    DeckData m_originalDeck;
    DeckData m_runtimeDeck;

    [SerializeField,Header("キャンバス")]
    Canvas canvas;

    [SerializeField,Header("山札（デッキ）エリア")]
    RectTransform deckArea;

    [SerializeField,Header("手札エリア")]
    RectTransform handArea;

    [SerializeField, Header("墓地エリア")]
    RectTransform graveyardArea;

    [SerializeField,Header("初期手札の枚数")]
    int m_initialHandCount;

    [SerializeField, Header("拠点")]
    GameObject m_myBaseObject;

    [SerializeField,Header("カードの詳細を表示")]
    ViewCardDetail m_cardDetail;


    List<GameObject> m_deckCardList = new();       //山札にあるカードリスト
    List<GameObject> m_handCardList = new ();       //手札にあるカードリスト
    List<GameObject> m_graveyardCardList = new ();       //墓地エリアにあるカードリスト

    int m_maxCardCount;


    int m_setId = 0;

    int m_oldSelectCardId = -1;

    bool m_isCardDetailActive = false;          //カードの詳細オブジェクトがアクティブ化か

    bool m_isSetUpHand = false;                 //手札のセットアップができたか


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
    /// カードの詳細オブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public ViewCardDetail GetCardDetail()
    {
        return m_cardDetail;
    }

    public bool InvertCardDetailActive(CardBase cardBase)
    {
        //前回クリックしたカードと同じIDでなければ
        if(m_oldSelectCardId != cardBase.GetCardID())
        {
            m_isCardDetailActive = true;
            //アクティブを設定
            m_cardDetail.gameObject.SetActive(m_isCardDetailActive);
            //他のカードの位置を調整
            foreach(var handCard in m_handCardList)
            {
                CardBase handCardBase = handCard.GetComponent<CardBase>();
                handCardBase.GetCardDrager().ReturnCardToHandPosition();
            }
        }
        else
        {
            //同じカードをクリックしていたら反転させる
            //フラグを反転
            m_isCardDetailActive = !m_isCardDetailActive;
            //アクティブを設定
            m_cardDetail.gameObject.SetActive(m_isCardDetailActive);
        }

       


        //今回選択したカードのIDを前に選択したカードのIDに設定
        m_oldSelectCardId = cardBase.GetCardID();

        return m_isCardDetailActive;
    }

    public void SetCardDetailActive(bool flag)
    {
        m_isCardDetailActive = flag;
        //アクティブを設定
        m_cardDetail.gameObject.SetActive(m_isCardDetailActive);

    }


    /////////////////////////////////////////////////////////////////////////////////////////////
    ///デッキに関する処理
    /////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 最初のデッキの諸々の設定
    /// </summary>
    public void StartDeckAndCardSetting()
    {
        //デッキのカードにIDと親オブジェクトを設定していく
        foreach(var card in m_runtimeDeck.cardPrefabsList)
        {
            //デッキのカードを生成
            GameObject cardInstance = Instantiate(card);
            //デッキエリアの子オブジェクトに設定
            cardInstance.transform.SetParent(deckArea.transform, false);
            //カードのIDを設定
            SetCardIdInDeck(cardInstance);
            //デッキリストに追加
            m_deckCardList.Add(cardInstance);
        }

        //デッキをシャッフル
        DeckListShuffle();
        //デッキの最大枚数を取得
        m_maxCardCount = m_deckCardList.Count;


        //画面下に初期手札の数分カードを設置
        //移動不可能
        //配置方法は移動型
        DrawCardFromDeck(m_initialHandCount,true,EnDrawCardMethod.enDrawCardMethod_Movable);

       
    }



    /// <summary>
    /// カードをデッキに追加
    /// </summary>
    /// <param name="addCardPrefab">追加するカードプレファブ</param>
    public void AddCardInDeckList(GameObject addCardPrefab)
    {
        //墓地エリアオブジェクトの子オブジェクトに変更
        addCardPrefab.transform.SetParent(deckArea, false);
        //デッキリストにカードを追加
        m_deckCardList.Add(addCardPrefab);
        //デッキの最大枚数を取得
        m_maxCardCount = m_deckCardList.Count;


    }

    /// <summary>
    /// カードを墓地エリアに送る
    /// </summary>
    /// <param name="sendCardPrefab">墓地に送るカードプレファブ</param>
    public void SendCardToGraveyard(GameObject sendCardPrefab)
    {
        //墓地に送ったカードを非アクティブ化する
        sendCardPrefab.SetActive(false);
        //移動した分座標をリセット
        sendCardPrefab.transform.localPosition = Vector3.zero;
        //墓地エリアオブジェクトの子オブジェクトに変更
        sendCardPrefab.transform.SetParent(graveyardArea, false);

        //削除したカードを墓地カードリストに追加する
        m_graveyardCardList.Add(sendCardPrefab);
        //手札リストから墓地に送ったカードを削除
        if(m_handCardList.Remove(sendCardPrefab))
        {
            //消せた
            //手札のカードが一枚減ったので、デッキから一枚カードを引く
            DrawCardFromDeck(1,true);
        }
        else
        {
            //消せなかった
            Debug.LogError("カードを削除出来なかった");
            return;
        }
    }

    /// <summary>
    /// デッキのカードのカードIDを設定する
    /// </summary>
    void SetCardIdInDeck(GameObject card)
    {
        CardBase cardBase = card.GetComponent<CardBase>();
        if(cardBase == null)
        {
            Debug.LogError("カードではない");
            return;
        }
        //IDを設定
        cardBase.SetCardID(m_setId);
        //次のIDにしておく
        m_setId++;
    }

    void DeepCopyDeckData()
    {
        //オリジナルのデッキデータをランタイム用のデッキデータに生成、代入
        m_runtimeDeck = ScriptableObject.CreateInstance<DeckData>();
        //オリジナルのカードリストの配列の数分のリストを生成
        m_runtimeDeck.cardPrefabsList =
            new List<GameObject>(this.m_originalDeck.cardPrefabsList.Count);
        //オリジナルデータのカードリストをランタイムのカードリストに追加する
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
        //IDの初期化
        m_setId = 0;

        //カードの詳細は非表示
        m_cardDetail.gameObject.SetActive(m_isCardDetailActive);

        //最初のカードのセッティング
        StartDeckAndCardSetting();
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        

    }

    /// <summary>
    /// デッキリストの要素をシャッフルする
    /// </summary>
    void DeckListShuffle()
    {
        for (int i = 0; i < m_deckCardList.Count; i++)
        {
            var temp = m_deckCardList[i];
            //現在のカードの番号からリストの総数からランダムな番号を取得
            int randomIndex = Random.Range(i, m_deckCardList.Count);
            //対象のカードとランダムに選ばれたカードと入れ替える
            m_deckCardList[i] = m_deckCardList[randomIndex];
            //ランダムに選ばれたカードに確保しておいたカードを代入
            m_deckCardList[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 墓地エリアからカードをデッキに戻す
    /// </summary>
    void ReclaimCardFromGraveyard()
    {
        //墓地から回収できなかったら

        foreach(var graveyardCard in m_graveyardCardList)
        {
            //非アクティブ化されているなら
            if(graveyardCard.activeSelf==false)
            {
                //アクティブ化する
                graveyardCard.SetActive(true);
            }

            //山札に入るので、移動処理などができないようにする
            //生成したカードとデッキのカードのCardBaseコンポーネントを取得
            CardBase cardBase = graveyardCard.GetComponent<CardBase>();
            //カードが移動できないようにする
            cardBase.GetCardDrager().SetMovableFlag(false);

            //墓地のカードをデッキリストに追加(戻していく)
            AddCardInDeckList(graveyardCard);
        }
        //墓地のカードリストをクリアする
        m_graveyardCardList.Clear();
        //戻したらシャッフル
        DeckListShuffle();
    }

    /// <summary>
    /// デッキからカードを引いて手札に加える
    /// </summary>
    /// <param name="drawAmount">ドローするカードの枚数</param>
    /// <param name="movableFlag">カード移動可能フラグ</param>
    /// <param name="drawCardMethod">カードをドローする際の方法</param>
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
                Debug.Log("墓地からカードを回収");
                ReclaimCardFromGraveyard();
            }

            //デッキリストから手札リストにカードを加える
            m_handCardList.Add(m_deckCardList[0]);
            //手札にカードが渡ったので、デッキリストから同じカードを削除
            m_deckCardList.Remove(m_deckCardList[0]);

            GameObject cardInstance = m_handCardList[m_handCardList.Count - 1];

            //手札のカードの親オブジェクトを変更
            cardInstance.transform.SetParent(handArea.transform, true);
            //生成したカードとデッキのカードのCardBaseコンポーネントを取得
            CardBase cardBase = cardInstance.GetComponent<CardBase>();
            //カードドラッガーの初期化処理を実行
            cardBase.GetCardDrager().Init(canvas, m_myBaseObject);
        }

        //手札エリアにカードを配置
        PlaceCardInHand(drawCardMethod, movableFlag);
    }


    /// <summary>
    /// カードを手札エリアに配置する
    /// </summary>
    async void PlaceCardInHand(EnDrawCardMethod drawCardMethod,bool flag)
    {
        m_isSetUpHand = false;

        //手札エリアの幅を手札の枚数で割ってカードの間隔を計算
        float distance = handArea.rect.width / m_handCardList.Count;
        //手札エリアの半分の幅　―　カードの間隔/2で最初のカードの開始位置を計算
        float startPosX = (handArea.rect.width / 2.0f) - (distance / 2.0f);
        float subDistance = 0.0f;

        foreach (var card in m_handCardList)
        {
            CardBase cardBase = card.GetComponent<CardBase>();

            if(drawCardMethod==EnDrawCardMethod.enDrawCardMethod_Movable)
            {
                //カードを手札の定位置に移動させる
                cardBase.GetCardDrager().MoveCard(new Vector2(subDistance - startPosX, 0.0f));
                //一秒待ってループ再開
                await Task.Delay(100);
            }
            else
            {
                //直接移動させるなら
                cardBase.GetCardDrager().SetRectTransform(Vector2.zero);
                cardBase.GetCardDrager().SumRectTransform(new Vector2(subDistance - startPosX, 0.0f));
                //一秒待ってループ再開
                await Task.Delay(50);
            }

            //手札での初期位置をカードの到達地点の座標に設定
            cardBase.GetCardDrager().ChangeHandPositionToNowPosition(new Vector2(subDistance - startPosX, 0.0f));

            //手札に入ったので、カードを動かせるようにする
            cardBase.GetCardDrager().SetMovableFlag(flag);

            //次のカードの距離にする
            subDistance += distance;

            

        }

        //手札にカードを設置完了
        m_isSetUpHand = true;

    }




    /// <summary>
    /// 手札のカードの移動可能フラグを設定
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
