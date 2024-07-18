using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public static class DamageProcessor
{

    /// <summary>
    /// 被ダメージ処理
    /// </summary>
    /// <param name="victimObject">ダメージを受けるオブジェクト</param>
    /// <param name="hitDamage">受けるダメージ量</param>
    public static void HitDamage(GameObject victimObject,int hitDamage)
    {
        if (victimObject == null) return;

        //被ダメージインターフェースを継承した
        IDamageable damageable = victimObject.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.Damage(hitDamage);

    }
}
