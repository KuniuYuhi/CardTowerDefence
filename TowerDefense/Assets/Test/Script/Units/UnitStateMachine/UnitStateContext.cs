using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateContext
{
    IUnitState m_currentUnitState;      //���݂̏��
    IUnitState m_previousUnitState;     //���O�̏��

    Dictionary<En_UnitState, IUnitState> m_unitTable;   //��̑S��Ԃ��i�[��������

    public IUnitState GetCurrentUnitState()
    {
        return m_currentUnitState;
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="firstUnitState"></param>
    public void Init(UnitBase unit,En_UnitState firstUnitState)
    {
        if(m_unitTable!=null)
        {
            return;
        }

        Dictionary<En_UnitState, IUnitState> table = new()
        {
            { En_UnitState.enUnitState_Idle, new UnitState_Idle(unit) },
            { En_UnitState.enUnitState_Walk, new UnitState_Walk(unit) },
            { En_UnitState.enUnitState_normalAttack, new UnitState_NomalAttack(unit) },
            { En_UnitState.enUnitState_Die, new UnitState_Die(unit) },
        };
        //�e�[�u����ݒ�
        m_unitTable = table;
        //���݂̃X�e�[�g��ݒ�
        m_currentUnitState = m_unitTable[firstUnitState];

        ChangeState(firstUnitState);
    }


    public void ChangeState(En_UnitState changeState)
    {
        if(m_unitTable == null || m_currentUnitState==null)
        {
            Debug.LogError("�X�e�[�g��؂�ւ����܂���");
            return;
        }

        var newState = m_unitTable[changeState];

        //�O�̃X�e�[�g�̔����o�����̏���������
        m_previousUnitState = m_currentUnitState;
        m_previousUnitState.Exit();
        //�V�����X�e�[�g�̍ŏ��̏���������
        m_currentUnitState = newState;
        m_currentUnitState.Entry();
    }


    public void Update() => m_currentUnitState?.Update();

    public void FixedUpdate() => m_currentUnitState?.FixedUpdate();



}
