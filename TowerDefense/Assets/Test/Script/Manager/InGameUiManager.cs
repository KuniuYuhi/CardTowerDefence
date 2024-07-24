using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// インゲームのUIを管理するマネージャー。シングルトンパターン
/// </summary>
public class InGameUiManager : MonoBehaviour
{
    //シングルトンパターン
    //このインスタンスを取得して処理する
    public static InGameUiManager Instance { get; private set; }

    GameStartUI m_gameStartUI;
    GameResultUI m_gameResultUI;

    [SerializeField,Header("カードに関するUI")]
    GameObject m_CardFieldUI;


    void SetUp()
    {
        //各UIコンポーネントを取得
        m_gameStartUI = GetComponent<GameStartUI>();
        m_gameResultUI = GetComponent<GameResultUI>();


    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //インスタンス
            SetUp();
        }
        else
        {
            //既に作成されているなら削除
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// リザルトの画像を有効化する
    /// </summary>
    public void ActiveResultPanel()
    {
        m_gameResultUI.ActiveResultPanel();
    }

    /// <summary>
    /// ゲームスタートの画像を有効化する
    /// </summary>
    public void ActiveGameStartPanel()
    {
        m_gameStartUI.ActiveGameStartPanel();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flag"></param>
    public void SetCardFieldPanelActive(bool flag)
    {
        if (m_CardFieldUI == null) return;
        m_CardFieldUI.SetActive(flag);
    }

}
