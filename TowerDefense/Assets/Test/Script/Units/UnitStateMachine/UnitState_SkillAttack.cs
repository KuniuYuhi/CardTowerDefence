using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_SkillAttack : IUnitState
{
    UnitBase m_unit;


  
    //��Ԃ̐ݒ�
    public En_UnitState m_unitState => En_UnitState.enUnitState_SkillAttack;


    public UnitState_SkillAttack(UnitBase unit) => m_unit = unit;


    //��ԊJ�n���ŏ��ɌĂ΂��
    public void Entry()
    {
        
    }

    //���t���[���Ă΂��X�V����
    public void Update()
    {
        

        if(!m_unit.GetAnimationEventScript().GetIsActive())
        {
            m_unit.CommonStateTransition();
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
        m_unit.GetAttackTimer().ResetSkillAttackTimer();
        //�U�����I������̂Ńt���O��������
        m_unit.SetIsAttackActive(false);
    }
}
