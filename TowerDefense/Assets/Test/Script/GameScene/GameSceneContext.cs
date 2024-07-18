using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneContext:MonoBehaviour
{
    IGameSceneState m_currentGameSceneState;        //���݂̃V�[���X�e�[�g
    IGameSceneState m_previousGameSceneState;       //�O�̃V�[���X�e�[�g

    Dictionary<EnGameSceneState, IGameSceneState> m_sceneTable;


    public IGameSceneState GetCurrentGameSceneState()
    {
        return m_currentGameSceneState;
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="initGameSceneState">����������V�[���̃X�e�[�g</param>
    public void Init(GameManager gameManager,EnGameSceneState initGameSceneState)
    {
        if(m_sceneTable!=null)
        {
            return;
        }

        m_sceneTable = new Dictionary<EnGameSceneState, IGameSceneState>();

        //�A�^�b�`����Ă���e�V�[���̃X�e�[�g�N���X���e�[�u���Ɋi�[
        m_sceneTable.Add(EnGameSceneState.enGameSceneState_Title, gameObject.GetComponent<SceneState_Title>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_GameStart, gameObject.GetComponent<SceneState_GameStart>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_InGame, gameObject.GetComponent<SceneState_InGame>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_GameClear, gameObject.GetComponent<SceneState_GameClear>());
        m_sceneTable.Add(EnGameSceneState.EnGameSceneState_GameOver, gameObject.GetComponent<SceneState_GameOver>());

       

        ChangeScene(initGameSceneState);

    }

    /// <summary>
    /// �X�e�[�g�̐؂�ւ�
    /// </summary>
    /// <param name="changeGameSceneState">�؂�ւ������V�[���X�e�[�g</param>
    public void ChangeScene(EnGameSceneState changeGameSceneState)
    {
        //�e�[�u�����Ȃ��Ȃ珈�����Ȃ�
        if(m_sceneTable==null)
        {
            return;
        }
        //�����̃V�[���X�e�[�g���e�[�u������擾
        if(m_sceneTable.TryGetValue(changeGameSceneState,out IGameSceneState newSceneState))
        {
            //�O�̃V�[���X�e�[�g�̔����o�����̏��������s
            m_previousGameSceneState?.Exit();
            //���݂̃V�[���X�e�[�g�ɐV�����V�[���X�e�[�g����
            m_currentGameSceneState = newSceneState;
            //�V�����V�[���X�e�[�g�̍ŏ��̏��������s
            m_currentGameSceneState?.Entry();

            //�O�̃V�[���X�e�[�g�Ɍ��݂̃V�[���X�e�[�g����
            m_previousGameSceneState = m_currentGameSceneState;
        }

      
    }

    /// <summary>
    /// ���݂̃V�[���X�e�[�g�̍X�V����
    /// </summary>
    public void UpdateSceneState() => m_currentGameSceneState?.UpdateSceneState();

}
