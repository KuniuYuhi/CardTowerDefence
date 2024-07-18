using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    ObjectInfoManager m_objectInfoManager;

    [SerializeField]
    GameSceneContext m_gameSceneContext;        //�Q�[���V�[���X�e�[�g���Ǘ�����

    [SerializeField]
    EnGameSceneState m_startGameSceneState = EnGameSceneState.enGameSceneState_Title;


    bool m_isChangeScene = false;

    /// <summary>
    /// ���s
    /// </summary>
    public enum EnOutcome
    {
        enOutcome_None,     //�Ȃ�
        enOutcome_WIn,      //����
        enOutcome_Lose      //����
    }

    EnOutcome enOutcomeState;


    /// <summary>
    /// �Q�[���V�[���R���e�L�X�g�̎擾
    /// </summary>
    /// <returns></returns>
    public GameSceneContext GetGameSceneContext()
    {
        return m_gameSceneContext;
    }
    /// <summary>
    /// ���݂̃V�[���X�e�[�g���擾
    /// </summary>
    /// <returns></returns>
    public IGameSceneState GetCurrentGameSceneState()
    {
        return m_gameSceneContext.GetCurrentGameSceneState();
    }

    public void SetChangeSceneFlag(bool flag)
    {
        m_isChangeScene = flag;
    }

    public bool GetChangeSceneFlag()
    {
        return m_isChangeScene;
    }

    /// <summary>
    /// ���s��ݒ�
    /// </summary>
    /// <param name="outcome"></param>
    public void SetOutcome(EnOutcome outcome)
    {
        enOutcomeState = outcome;
    }

    /// <summary>
    /// ���s���擾
    /// </summary>
    /// <returns></returns>
    public EnOutcome GetOutcome()
    {
        return enOutcomeState;
    }


    //�V���O���g���p�^�[��
    //�Q�[���}�l�[�W���[��static�Ő錾
    //���̃C���X�^���X���擾���ď�������
    public static GameManager Instance { get; private set; }

   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //�V�[�����؂�ւ���Ă��폜���Ȃ�
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //���ɍ쐬����Ă���Ȃ�폜
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //����������
        //�ŏ��̃V�[���X�e�[�g��ݒ�
        m_gameSceneContext.Init(this, m_startGameSceneState);

        //���s��������
        enOutcomeState = EnOutcome.enOutcome_None;
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̃V�[���̍X�V����
        m_gameSceneContext.UpdateSceneState();
    }



    /// <summary>
    /// �V�[���X�e�[�g��؂�ւ���
    /// </summary>
    /// <param name="changeSceneState">�؂�ւ���V�[���X�e�[�g</param>
    public void ChangeSceneState(EnGameSceneState changeSceneState) 
        => m_gameSceneContext.ChangeScene(changeSceneState);

    /// <summary>
    /// �C���Q�[���ɃV�[����؂�ւ���
    /// </summary>
    public void ChangeInGameSceneState()
    {
       
        //�V�[���X�e�[�g��؂�ւ���
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_InGame);
    }

    /// <summary>
    /// �Q�[���N���A�ɃV�[����؂�ւ���
    /// </summary>
    public void ChangeGameClearSceneState()
    {
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_GameClear);
    }

    /// <summary>
    /// �Q�[���I�[�o�[�ɃV�[����؂�ւ���
    /// </summary>
    public void ChangeGameOverSceneState()
    {
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_GameOver);
    }

    /// <summary>
    /// �^�C�g���ɃV�[����؂�ւ���
    /// </summary>
    public void ChangeTitleSceneState()
    {
        //�܂��̓V�[����؂�ւ���
        SceneManager.LoadScene("TitleScene");
        //�V�[���X�e�[�g��؂�ւ���
        m_gameSceneContext.ChangeScene(EnGameSceneState.enGameSceneState_Title);
    }

    /// <summary>
    /// �Q�[���X�^�[�g�ɃV�[����؂�ւ���
    /// </summary>
    public void ChangeGameStartSceneState()
    {
        //�܂��̓V�[����؂�ւ���
        SceneManager.LoadScene("TestScene");
        //
        m_gameSceneContext.ChangeScene(EnGameSceneState.EnGameSceneState_GameStart);
    }



}
