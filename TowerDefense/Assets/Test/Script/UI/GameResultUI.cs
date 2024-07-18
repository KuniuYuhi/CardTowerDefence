using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultUI : MonoBehaviour
{
    [SerializeField, Header("�����p�l��")]
    GameObject m_victoryPanel;        // �����p�l���i�������ɕ\������UI�j
    [SerializeField, Header("�s�k�p�l��")]
    GameObject m_defeatPanel;         // �s�k�p�l���i�s�k���ɕ\������UI�j

    [SerializeField, Header("�^�C�g���ɖ߂�{�^��")]
    GameObject m_goToTitleButton;

    // Start is called before the first frame update
    void Start()
    {
        //�eUI���A�N�e�B�u������
        m_victoryPanel.SetActive(false);
        m_defeatPanel.SetActive(false);
        m_goToTitleButton.SetActive(false);
    }

    /// <summary>
    /// ���s�p�l����L��������
    /// </summary>
    public void ActiveResultPanel()
    {
        //�Q�[���}�l�[�W���[���珟�s�������Ă��炤
        switch (GameManager.Instance.GetOutcome())
        {
            //����
            case GameManager.EnOutcome.enOutcome_WIn:
                m_victoryPanel.SetActive(true);
                break;
            //�s�k
            case GameManager.EnOutcome.enOutcome_Lose:
                m_defeatPanel.SetActive(true);
                break;

            case GameManager.EnOutcome.enOutcome_None:
                Debug.LogError("���s�͌��܂��Ă��Ȃ�");
                break;
        }

        //�^�C�g���ɖ߂�{�^�����A�N�e�B�u��
        m_goToTitleButton.SetActive(true);
    }


}
