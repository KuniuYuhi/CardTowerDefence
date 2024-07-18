using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneState_GameClear : MonoBehaviour, IGameSceneState
{
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_GameClear;


    public void Entry()
    {
        Debug.Log("�Q�[���N���A");

        //�Q�[���}�l�[�W���[�ɏ��s��������
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_WIn);
    }

    public void UpdateSceneState()
    {

        

    }

    /// <summary>
    /// �^�C�g���V�[���ɐ؂�ւ���
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
