using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : IUnitState
{

    UnitBase m_unit;

    //��Ԃ̐ݒ�
    public En_UnitState m_unitState => En_UnitState.enUnitState_Idle;


    public UnitState_Idle(UnitBase unit) => m_unit = unit;


    //��ԊJ�n���ŏ��ɌĂ΂��
    public void Entry()
    {
        m_unit.GetCharacterController().SetmWalkAnimationBlendSpeed(0.0f);
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
        ////�^�[�Q�b�g�I�u�W�F�N�g�����Ȃ��Ȃ狭���I�ɕ�����ԂɑJ�ځB�i�G�̋��_�Ɍ������j
        //if (m_unit.GetCharacterController().GetTargetObject() == null)
        //{
        //    m_unit.MoveTo();
        //}
        ////�^�[�Q�b�g�����Ď������痣��Ă����ꍇ
        //else
        //{
        //    //�U���\�������痣�ꂽ��ǂ�������
        //    m_unit.MoveTo();

        //}
        m_unit.MoveTo();
        m_unit.Rotation();
    }

    //��ԏI�����ɌĂ΂��
    public void Exit()
    {

    }

}
