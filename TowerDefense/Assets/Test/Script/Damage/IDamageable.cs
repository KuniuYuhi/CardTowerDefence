using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="hitDamage">受けるダメージ</param>
    public void Damage(int hitDamage);

    /// <summary>
    /// 死亡時処理
    /// </summary>
    public void Die();
}



