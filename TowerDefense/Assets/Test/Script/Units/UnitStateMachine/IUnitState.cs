using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��N���X�̃X�e�[�g�̊��N���X
/// </summary>
public interface IUnitState
{
   
    En_UnitState m_unitState { get; }

    public En_UnitState GetUnitState()
    {
        return m_unitState;
    }

    //��ԊJ�n���ŏ��ɌĂ΂��
    void Entry();

    //���t���[���Ă΂��X�V����
    void Update();

    /// <summary>
    /// ���̃t���[���ŌĂ΂��X�V�����i�ړ������Ȃǂ������ōs���j
    /// </summary>
    void FixedUpdate();

    //��ԏI�����ɌĂ΂��
    void Exit();


}

/// <summary>
/// ��̃X�e�[�g
/// </summary>
public enum En_UnitState
{
    enUnitState_Idle,                   //�ҋ@���
    enUnitState_Walk,                   //�������
    enUnitState_normalAttack,           //�ʏ�U�����
    enUnitState_Die,                    //���S���
}