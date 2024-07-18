using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneState_GameOver : MonoBehaviour, IGameSceneState
{
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_GameOver;

    

    public void Entry()
    {
        Debug.Log("ゲームオーバー");

        //ゲームマネージャーに勝敗を教える
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_Lose);
    }

    public void UpdateSceneState()
    {

    }

    public void Exit()
    {
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_None);
    }

    /// <summary>
    /// タイトルシーンに切り替える
    /// </summary>
    public void ChangeTitleScene()
    {
        GameManager.Instance.ChangeTitleSceneState();
    }

}
