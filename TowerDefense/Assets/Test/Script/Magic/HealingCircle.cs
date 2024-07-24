using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : MagicBase
{
    [SerializeField, Header("回復対象のレイヤー")]
    LayerMask m_healLayers;

    int m_healAmount;
    float m_radius;


    protected override void Awake()
    {
        base.Awake();
        //ステータスから回復力と半径を取得
        m_healAmount = (int)GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("回復力");
        m_radius = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("半径");
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
            transform.position, m_radius, m_healLayers
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
