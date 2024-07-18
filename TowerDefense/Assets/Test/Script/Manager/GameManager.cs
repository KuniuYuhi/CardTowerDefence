using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    ObjectInfoManager m_objectInfoManager;

    [SerializeField]
    GameSceneContext m_gameSceneContext;        //ゲームシーンステートを管理する

    [SerializeField]
    EnGameSceneState m_startGameSceneState = EnGameSceneState.enGameSceneState_Title;


    bool m_isChangeScene = false;

    /// <summary>
    /// 勝敗
    /// </summary>
    public enum EnOutcome
    {
        enOutcome_None,     //なし
        enOutcome_WIn,      //勝ち
        enOutcome_Lose      //負け
    }

    EnOutcome enOutcomeState;


    /// <summary>
    /// ゲームシーンコンテキストの取得
    /// </summary>
    /// <returns></returns>
    public GameSceneContext GetGameSceneContext()
    {
        return m_gameSceneContext;
    }
    /// <summary>
    /// 現在のシーンステートを取得
    /// </summary>
    /// <returns></returns>
    public IGameSceneState GetCurrentGameSceneState()
    {
        return m_gameSceneContext.GetCurrentGameSceneState();
    }

    public void SetChangeSceneFlag(bool flag)
    {
        m_isChangeScene = flag;
    }

    public bool GetChangeSceneFlag()
    {
        return m_isChangeScene;
    }

    /// <summary>
    /// 勝敗を設定
    /// </summary>
    /// <param name="outcome"></param>
    public void SetOutcome(EnOutcome outcome)
    {
        enOutcomeState = outcome;
    }

    /// <summary>
    /// 勝敗を取得
    /// </summary>
    /// <returns></returns>
    public EnOutcome GetOutcome()
    {
        return enOutcomeState;
    }


    //シングルトンパターン
    //ゲームマネージャーはstaticで宣言
    //このインスタンスを取得して処理する
    public static GameManager Instance { get; private set; }

   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //シーンが切り替わっても削除しない
            DontDestroyOnLoad(gameObject);
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
        //初期化処理
        //最初のシーンステートを設定
        m_gameSceneContext.Init(this, m_startGameSceneState);

        //勝敗を初期化
        enOutcomeState = EnOutcome.enOutcome_None;
    }

    // Update is called once per frame
    void Update()
    {
        //現在のシーンの更新処理
        m_gameSceneContext.UpdateSceneState();
    }



    /// <summary>
    /// シーンステートを切り替える
    /// </summary>
    /// <param name="changeSceneState">切り替えるシーンステート</param>
    public void ChangeSceneState(EnGameSceneState changeSceneState) 
        => m_gameSceneContext.ChangeScene(changeSceneState);

    /// <summary>
    /// インゲームにシーンを切り替える
    /// </summary>
    public void ChangeInGameSceneState()
    {
       
        //シーンステートを切り替える
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_InGame);
    }

    /// <summary>
    /// ゲームクリアにシーンを切り替える
    /// </summary>
    public void ChangeGameClearSceneState()
    {
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_GameClear);
    }

    /// <summary>
    /// ゲームオーバーにシーンを切り替える
    /// </summary>
    public void ChangeGameOverSceneState()
    {
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_GameOver);
    }

    /// <summary>
    /// タイトルにシーンを切り替える
    /// </summary>
    public void ChangeTitleSceneState()
    {
        //まずはシーンを切り替える
        SceneManager.LoadScene("TitleScene");
        //シーンステートを切り替える
        m_gameSceneContext.ChangeScene(EnGameSceneState.enGameSceneState_Title);
    }

    /// <summary>
    /// ゲームスタートにシーンを切り替える
    /// </summary>
    public void ChangeGameStartSceneState()
    {
        //まずはシーンを切り替える
        SceneManager.LoadScene("TestScene");
        //
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_GameStart);
    }



}
