using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneState_GameOver : MonoBehaviour, IGameSceneState
{
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_GameOver;

    

    public void Entry()
    {
        Debug.Log("�Q�[���I�[�o�[");

        //�Q�[���}�l�[�W���[�ɏ��s��������
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
    /// �^�C�g���V�[���ɐ؂�ւ���
    /// </summary>
    public void ChangeTitleScene()
    {
        GameManager.Instance.ChangeTitleSceneState();
    }

}
