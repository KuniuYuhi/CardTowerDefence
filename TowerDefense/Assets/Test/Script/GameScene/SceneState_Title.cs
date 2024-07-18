using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState_Title : MonoBehaviour, IGameSceneState
{
    public EnGameSceneState enGameSceneState => EnGameSceneState.enGameSceneState_Title;


    public void Entry()
    {
        
    }

    public void UpdateSceneState()
    {
       


    }

    /// <summary>
    /// インゲームシーンに切り替える
    /// </summary>
    public void ChangeInGameScene()
    {
        GameManager.Instance.ChangeGameStartSceneState();
    }

    public void Exit()
    {
        
    }

}

