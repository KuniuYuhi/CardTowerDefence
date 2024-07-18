using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAtkObject : MonoBehaviour
{
    [SerializeField, Header("オブジェクトを持つ建物のステータス")]
    Status_Building status;

    bool m_isDamageable = true;
   

    public void SetIsDamageable(bool flag)
    {
        m_isDamageable = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        //ダメージを与えられないなら処理しない
        if (!m_isDamageable) return;

        //被ダメージインターフェースを継承した
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.Damage(status.GetEffectPower());

    }

    /// <summary>
    /// 自身を中心に範囲内の特定のレイヤーのオブジェクトにダメージを与える
    /// </summary>
    /// <param name="includeLayer">調べたいレイヤー</param>
    /// <param name="radius">調べる半径</param>
    public void ApplyAreaDamage(LayerMask includeLayer,float radius)
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, radius*2.0f, includeLayer
            );

        foreach (var hitCollider in hitColliders)
        {
            //被ダメージインターフェースを継承した
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();

            if (damageable == null) return;

            damageable.Damage(status.GetEffectPower());
        }
    }

}
