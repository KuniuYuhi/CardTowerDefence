using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneState_InGame : MonoBehaviour, IGameSceneState
{

    ObjectInfoManager m_objectInfoManager;

    
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_InGame;

    

    private AsyncOperation asyncOperation;

    

    public void Entry()
    {
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_None);

        
    }

    public void UpdateSceneState()
    {
        Debug.Log("ゲーム進行中");

       
        if(GameManager.Instance.GetOutcome()==GameManager.EnOutcome.enOutcome_WIn)
        {
            Debug.Log("味方の勝ち");
            //シーンステートをゲームクリアに切り替える
            GameManager.Instance.ChangeSceneState(EnGameSceneState.EnGameSceneState_GameClear);
            return;
        }
        else if (GameManager.Instance.GetOutcome() == GameManager.EnOutcome.enOutcome_WIn)
        {
            Debug.Log("敵の勝ち");
            //シーンステートをゲームオーバーに切り替える
            GameManager.Instance.ChangeSceneState(EnGameSceneState.EnGameSceneState_GameOver);
            return;
        }

       
    }

    public void Exit()
    {
        //インゲームに関するUIを削除する
        InGameUiManager.Instance.SetCardFieldPanelActive(false);
    }

}
