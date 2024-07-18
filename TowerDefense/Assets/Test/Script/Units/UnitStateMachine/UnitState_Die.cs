using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Die : IUnitState
{
    UnitBase m_unit;

    //��Ԃ̐ݒ�
    public En_UnitState m_unitState => En_UnitState.enUnitState_Die;


    public UnitState_Die(UnitBase unit) => m_unit = unit;


    //��ԊJ�n���ŏ��ɌĂ΂��
    public void Entry()
    {

    }

    //���t���[���Ă΂��X�V����
    public void Update()
    {
        //���S�A�j���[�V�������I�������
        if (m_unit.GetAnimationEventScript().IsDie())
        {
            //�폜����
            m_unit.ProcessDie();
        }
    }

    /// <summary>
    /// ���̃t���[���ŌĂ΂��X�V�����i�ړ������Ȃǂ������ōs���j
    /// </summary>
    public void FixedUpdate()
    {

    }

    //��ԏI�����ɌĂ΂��
    public void Exit()
    {

    }
}
