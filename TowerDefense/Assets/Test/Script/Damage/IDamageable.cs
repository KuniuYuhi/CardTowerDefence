using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="hitDamage">�󂯂�_���[�W</param>
    public void Damage(int hitDamage);

    /// <summary>
    /// ���S������
    /// </summary>
    public void Die();
}



