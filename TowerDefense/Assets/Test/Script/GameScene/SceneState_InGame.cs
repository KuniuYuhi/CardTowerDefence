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
        Debug.Log("�Q�[���i�s��");

       
        if(GameManager.Instance.GetOutcome()==GameManager.EnOutcome.enOutcome_WIn)
        {
            Debug.Log("�����̏���");
            //�V�[���X�e�[�g���Q�[���N���A�ɐ؂�ւ���
            GameManager.Instance.ChangeSceneState(EnGameSceneState.EnGameSceneState_GameClear);
            return;
        }
        else if (GameManager.Instance.GetOutcome() == GameManager.EnOutcome.enOutcome_WIn)
        {
            Debug.Log("�G�̏���");
            //�V�[���X�e�[�g���Q�[���I�[�o�[�ɐ؂�ւ���
            GameManager.Instance.ChangeSceneState(EnGameSceneState.EnGameSceneState_GameOver);
            return;
        }

       
    }

    public void Exit()
    {
        //�C���Q�[���Ɋւ���UI���폜����
        InGameUiManager.Instance.SetCardFieldPanelActive(false);
    }

}
