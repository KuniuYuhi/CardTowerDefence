using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Walk : IUnitState
{
    UnitBase m_unit;

    //��Ԃ̐ݒ�
    public En_UnitState m_unitState => En_UnitState.enUnitState_Walk;


    public UnitState_Walk(UnitBase unit) => m_unit = unit;


    //��ԊJ�n���ŏ��ɌĂ΂��
    public void Entry()
    {

    }

    //���t���[���Ă΂��X�V����
    public void Update()
    {
        m_unit.CommonStateTransition();
    }

    /// <summary>
    /// ���̃t���[���ŌĂ΂��X�V�����i�ړ������Ȃǂ������ōs���j
    /// </summary>
    public void FixedUpdate()
    {
        m_unit.MoveTo();
        m_unit.Rotation();
    }

    //��ԏI�����ɌĂ΂��
    public void Exit()
    {

    }
}
