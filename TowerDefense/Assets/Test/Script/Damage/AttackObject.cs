using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    UnitBase m_unit;

    /// <summary>
    /// 自身を生成したユニットを設定
    /// </summary>
    /// <param name="unit"></param>
    public void SetCreaterUnit(UnitBase unit)
    {
        m_unit = unit;
    }

    private void Start()
    {
        if (m_unit != null) return;
        //外部で設定していなかったら親オブジェクトからコンポーネントを取得
        m_unit = GetComponentInParent<UnitBase>();
    }


    private void OnTriggerEnter(Collider other)
    {
        DamageProcessor.HitDamage(other.gameObject, m_unit.GetCurrentAttackPower());

        ////被ダメージインターフェースを継承した
        //IDamageable damageable = other.GetComponent<IDamageable>();

        //if (damageable == null) return;

        //damageable.Damage(m_status.GetAtk());

    }
}
