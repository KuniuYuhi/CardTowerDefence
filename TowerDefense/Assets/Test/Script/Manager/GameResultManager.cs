using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultManager : MonoBehaviour
{
    [SerializeField,Header("�Q�[���̏����Ǘ�����}�l�[�W���[")]
    ObjectInfoManager m_objectInfoManager;

    bool m_isBattleOver = false;

    // Update is called once per frame
    void Update()
    {
        //���s�����܂��āAUI��\��������A���̐揈�����Ȃ�
        if (m_isBattleOver) return;

        CheckBattleOver();

    }






    void CheckBattleOver()
    {
        //���s�͌��܂�����
        if (CheckGameOutcome())
        {
            //UI�̏��s�p�l����\��������
            InGameUiManager.Instance.ActiveResultPanel();

            //�o�g���͏I�����
            m_isBattleOver = true;
            return;
        }

        //�o�g���͏I����Ă��Ȃ�
    }

    bool CheckGameOutcome()
    {
        //�����̋��_���󂳂ꂽ��
        if (m_objectInfoManager.GetAlliedBase().IsDestroyed())
        {
            //���s��s�k�ɐݒ肷��
            GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_Lose);
            return true;
        }
        //�G�̋��_����ꂽ��
        else if (m_objectInfoManager.GetEnemyBase().IsDestroyed())
        {
            //���s�������ɐݒ肷��
            GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_WIn);
            return true;
        }

        //���s�͌��܂��Ă��Ȃ�
        return false;
    }

    

}
