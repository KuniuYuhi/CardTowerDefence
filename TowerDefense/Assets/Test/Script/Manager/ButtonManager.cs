using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField,Header("�Q�[���J�n�{�^��")]
    Button m_gameStartButton;

    [SerializeField, Header("�^�C�g���ɖ߂�{�^��")]
    Button m_goToTitleButton;

   

    // Start is called before the first frame update
    void Start()
    {
        SetButtonListener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetButtonListener()
    {
        //�Q�[���J�n�{�^���̐ݒ�
        if(m_gameStartButton!=null)
        {
            m_gameStartButton.onClick.RemoveAllListeners();
            m_gameStartButton.onClick.AddListener(GameManager.Instance.ChangeGameStartSceneState);
        }
        //�^�C�g���ɖ߂�ɖ߂�{�^���̐ݒ�
        if(m_goToTitleButton!=null)
        {
            m_goToTitleButton.onClick.RemoveAllListeners();
            m_goToTitleButton.onClick.AddListener(GameManager.Instance.ChangeTitleSceneState);
        }

    }
}
