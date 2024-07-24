using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �C���Q�[����UI���Ǘ�����}�l�[�W���[�B�V���O���g���p�^�[��
/// </summary>
public class InGameUiManager : MonoBehaviour
{
    //�V���O���g���p�^�[��
    //���̃C���X�^���X���擾���ď�������
    public static InGameUiManager Instance { get; private set; }

    GameStartUI m_gameStartUI;
    GameResultUI m_gameResultUI;

    [SerializeField,Header("�J�[�h�Ɋւ���UI")]
    GameObject m_CardFieldUI;


    void SetUp()
    {
        //�eUI�R���|�[�l���g���擾
        m_gameStartUI = GetComponent<GameStartUI>();
        m_gameResultUI = GetComponent<GameResultUI>();


    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //�C���X�^���X
            SetUp();
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
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���U���g�̉摜��L��������
    /// </summary>
    public void ActiveResultPanel()
    {
        m_gameResultUI.ActiveResultPanel();
    }

    /// <summary>
    /// �Q�[���X�^�[�g�̉摜��L��������
    /// </summary>
    public void ActiveGameStartPanel()
    {
        m_gameStartUI.ActiveGameStartPanel();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flag"></param>
    public void SetCardFieldPanelActive(bool flag)
    {
        if (m_CardFieldUI == null) return;
        m_CardFieldUI.SetActive(flag);
    }

}
