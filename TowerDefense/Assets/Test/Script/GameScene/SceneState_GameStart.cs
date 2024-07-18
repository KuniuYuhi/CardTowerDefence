using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneState_GameStart : MonoBehaviour, IGameSceneState
{
    
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_GameStart;


    public void Entry()
    {
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_None);

        //シーン切り替えフラグを下げる
        GameManager.Instance.SetChangeSceneFlag(false);
    }

    public void UpdateSceneState()
    {
        //ゲームマネージャーのシーン切り替えフラグが立っていたら
        if(GameManager.Instance.GetChangeSceneFlag())
        {
            //シーン切り替えフラグを下げる
            GameManager.Instance.SetChangeSceneFlag(false);
            //ゲームシーンに切り替える
            GameManager.Instance.ChangeInGameSceneState();
        }

        Debug.Log("ゲームスタート");

    }

    public void Exit()
    {
        //シーン切り替えフラグを下げる
        GameManager.Instance.SetChangeSceneFlag(false);
    }

}

