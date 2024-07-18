using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneContext:MonoBehaviour
{
    IGameSceneState m_currentGameSceneState;        //現在のシーンステート
    IGameSceneState m_previousGameSceneState;       //前のシーンステート

    Dictionary<EnGameSceneState, IGameSceneState> m_sceneTable;


    public IGameSceneState GetCurrentGameSceneState()
    {
        return m_currentGameSceneState;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="initGameSceneState">初期化するシーンのステート</param>
    public void Init(GameManager gameManager,EnGameSceneState initGameSceneState)
    {
        if(m_sceneTable!=null)
        {
            return;
        }

        m_sceneTable = new Dictionary<EnGameSceneState, IGameSceneState>();

        //アタッチされている各シーンのステートクラスをテーブルに格納
        m_sceneTable.Add(EnGameSceneState.enGameSceneState_Title, gameObject.GetComponent<SceneState_Title>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_GameStart, gameObject.GetComponent<SceneState_GameStart>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_InGame, gameObject.GetComponent<SceneState_InGame>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_GameClear, gameObject.GetComponent<SceneState_GameClear>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_GameOver, gameObject.GetComponent<SceneState_GameOver>());

       

        ChangeScene(initGameSceneState);

    }

    /// <summary>
    /// ステートの切り替え
    /// </summary>
    /// <param name="changeGameSceneState">切り替えたいシーンステート</param>
    public void ChangeScene(EnGameSceneState changeGameSceneState)
    {
        //テーブルがないなら処理しない
        if(m_sceneTable==null)
        {
            return;
        }
        //引数のシーンステートをテーブルから取得
        if(m_sceneTable.TryGetValue(changeGameSceneState,out IGameSceneState newSceneState))
        {
            //前のシーンステートの抜け出す時の処理を実行
            m_previousGameSceneState?.Exit();
            //現在のシーンステートに新しいシーンステートを代入
            m_currentGameSceneState = newSceneState;
            //新しいシーンステートの最初の処理を実行
            m_currentGameSceneState?.Entry();

            //前のシーンステートに現在のシーンステートを代入
            m_previousGameSceneState = m_currentGameSceneState;
        }

      
    }

    /// <summary>
    /// 現在のシーンステートの更新処理
    /// </summary>
    public void UpdateSceneState() => m_currentGameSceneState?.UpdateSceneState();

}
