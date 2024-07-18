using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : MagicBase
{
    [SerializeField, Header("回復力")]
    int m_healAmount;

    [SerializeField, Header("回復範囲")]
    float m_range;

    [SerializeField, Header("回復対象のレイヤー")]
    LayerMask m_healLayers;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //回復する
        Heal();

        Destroy(gameObject);
    }


    void Heal()
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, m_range, m_healLayers
            );

        foreach (var hitCollider in hitColliders)
        {
            UnitBase unitBase = hitCollider.GetComponent<UnitBase>();
            //ユニットでないなら処理しない
            if (unitBase == null) continue;
            //ユニットのHPを回復する
            unitBase.RecoverHp(m_healAmount);

        }

    }

   
}
