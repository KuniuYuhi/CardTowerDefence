using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public static class DamageProcessor
{

    /// <summary>
    /// ��_���[�W����
    /// </summary>
    /// <param name="victimObject">�_���[�W���󂯂�I�u�W�F�N�g</param>
    /// <param name="hitDamage">�󂯂�_���[�W��</param>
    public static void HitDamage(GameObject victimObject,int hitDamage)
    {
        if (victimObject == null) return;

        //��_���[�W�C���^�[�t�F�[�X���p������
        IDamageable damageable = victimObject.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.Damage(hitDamage);

    }
}
