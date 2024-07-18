using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    UnitBase m_unit;

    /// <summary>
    /// ���g�𐶐��������j�b�g��ݒ�
    /// </summary>
    /// <param name="unit"></param>
    public void SetCreaterUnit(UnitBase unit)
    {
        m_unit = unit;
    }

    private void Start()
    {
        if (m_unit != null) return;
        //�O���Őݒ肵�Ă��Ȃ�������e�I�u�W�F�N�g����R���|�[�l���g���擾
        m_unit = GetComponentInParent<UnitBase>();
    }


    private void OnTriggerEnter(Collider other)
    {
        DamageProcessor.HitDamage(other.gameObject, m_unit.GetCurrentAttackPower());

        ////��_���[�W�C���^�[�t�F�[�X���p������
        //IDamageable damageable = other.GetComponent<IDamageable>();

        //if (damageable == null) return;

        //damageable.Damage(m_status.GetAtk());

    }
}
