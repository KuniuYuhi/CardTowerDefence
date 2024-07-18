using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�j���[�V�����C�x���g�̊֐�
/// </summary>
public class AnimationEventScript : MonoBehaviour
{
    [SerializeField,Header("�A�j���[�V�����C�x���g���g�����j�b�g(����̏����Ŏg��)")]
    UnitBase m_unit;

    bool m_isActive = false;

    bool m_isDie = false;

    
    /// <summary>
    /// �^�[�Q�b�g�Ƀ_���[�W��^����(���)
    /// </summary>
    public void ApplyTargetObjectDamage()
    {
        DamageProcessor.HitDamage(
            m_unit.GetCharacterController().GetTargetObject(), 
            m_unit.GetCurrentAttackPower()
            );

        ////�_���[�W�R���|�[�l���g���擾
        //IDamageable damageable = 
        //    m_unit.GetCharacterController().
        //    GetTargetObject().GetComponent<IDamageable>();
        ////�R���|�[�l���g���Ȃ���Ώ������Ȃ�
        //if (damageable == null) return;
        ////����̃_���[�W�֐����Ăяo��
        //damageable.Damage(m_unit.GetRuntimeCharacterStatus().GetAtk());
    }

    /// <summary>
    /// �s�������̃t���O���擾
    /// </summary>
    /// <returns></returns>
    public bool GetIsActive()
    {
        return m_isActive;
    }

    /// <summary>
    /// �s���J�n
    /// </summary>
    public void ActiveStart()
    {
        m_isActive = true;
    }

    /// <summary>
    /// �s���I��
    /// </summary>
    public void ActiveEnd()
    {
        m_isActive = false;
    }

    public void DieObject()
    {
        m_isDie = true;
    }

    public bool IsDie()
    {
        return m_isDie;
    }

}
