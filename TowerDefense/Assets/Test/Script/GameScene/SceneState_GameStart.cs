using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneState_GameStart : MonoBehaviour, IGameSceneState
{
    
    public EnGameSceneState enGameSceneState => EnGameSceneState.EnGameSceneState_GameStart;


    public void Entry()
    {
        GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_None);

        //�V�[���؂�ւ��t���O��������
        GameManager.Instance.SetChangeSceneFlag(false);
    }

    public void UpdateSceneState()
    {
        //�Q�[���}�l�[�W���[�̃V�[���؂�ւ��t���O�������Ă�����
        if(GameManager.Instance.GetChangeSceneFlag())
        {
            //�V�[���؂�ւ��t���O��������
            GameManager.Instance.SetChangeSceneFlag(false);
            //�Q�[���V�[���ɐ؂�ւ���
            GameManager.Instance.ChangeInGameSceneState();
        }

        Debug.Log("�Q�[���X�^�[�g");

    }

    public void Exit()
    {
        //�V�[���؂�ւ��t���O��������
        GameManager.Instance.SetChangeSceneFlag(false);
    }

}

