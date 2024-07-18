using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneState_GameClear : MonoBehaviour, IGameSceneState
{
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_GameClear;


    public void Entry()
    {
        Debug.Log("ゲームクリア");

        //ゲームマネージャーに勝敗を教える
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_WIn);
    }

    public void UpdateSceneState()
    {

        

    }

    /// <summary>
    /// タイトルシーンに切り替える
    /// </summary>
    public void ChangeTitleScene()
    {
        GameManager.Instance.ChangeTitleSceneState();
    }

    public void Exit()
    {
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_None);
    }
}
