using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField, Header("�Q�[���X�^�[�g�p�l��")]
    GameObject m_gameStartPanel;        //�Q�[���X�^�[�g�p�l��

    [SerializeField,Header("UI��\�������鎞��")]
    float m_viewTimer = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        //�eUI���A�N�e�B�u������
        m_gameStartPanel.SetActive(false);

        
    }

   /// <summary>
   /// �Q�[���X�^�[�g�p�l����\������
   /// </summary>
    public void ActiveGameStartPanel()
    {

        DeckOpen();

        //�eUI���A�N�e�B�u������
        //m_gameStartPanel.SetActive(true);

        //�R���[�`��
        StartCoroutine(PanelViewTimer());
    }

    IEnumerator PanelViewTimer()
    {
        while (true)
        {
            if (DeckManager.instance.GetSetUpHandFlag())
            {
                break;
            }

            //������x�҂�
            yield return new WaitForSeconds(1.0f);
        }

        //������x�҂�
        yield return new WaitForSeconds(1.0f);

        //�eUI���A�N�e�B�u������
        m_gameStartPanel.SetActive(true);

        //������x�҂�
        yield return new WaitForSeconds(m_viewTimer);

        //�Q�[���}�l�[�W���[��game�V�[����؂�ւ��Ă��������}�𑗂�
        GameManager.Instance.SetChangeSceneFlag(true);

        //��D�̃J�[�h�𓮂�����悤�ɂ���
        DeckManager.instance.SetHandCardsMovableFlag(true);

        //�p�l�����A�N�e�B�u������
        m_gameStartPanel.SetActive(false);
    }


    void DeckOpen()
    {
        //�f�b�L����ʂɃZ�b�g
        DeckManager.instance.StartDeckAndCardSetting();
    }

}
