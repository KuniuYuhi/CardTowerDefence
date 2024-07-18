using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSceneState
{
    EnGameSceneState enGameSceneState { get; }


   
    void Entry();

    void UpdateSceneState();

    void Exit();

}

public enum EnGameSceneState
{
    enGameSceneState_Title,
    EnGameSceneState_GameStart,
    EnGameSceneState_InGame,
    EnGameSceneState_GameClear,
    EnGameSceneState_GameOver,

}