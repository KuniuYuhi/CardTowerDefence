using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnhanceCircle : MagicBase
{
    [SerializeField, Header("効果対象のレイヤー")]
    LayerMask m_additiveAttackLayers;

    int m_additiveAttack;
    float m_radius;
    float m_boostDuration;


    protected override void Awake()
    {
        base.Awake();
        //ステータスから付加攻撃力と半径と効果継続時間を取得
        m_additiveAttack = (int)GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("付加攻撃力");
        m_radius = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("半径");
        m_boostDuration = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("効果持続時間");
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        AttackBoost();

        Destroy(gameObject);
    }

   
    void AttackBoost()
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, m_radius, m_additiveAttackLayers
            );

        //ヒットしたコライダーに攻撃力を増加させるコンポーネントを追加する
        foreach (var hitCollider in hitColliders)
        {
            UnitBase unitBase = hitCollider.gameObject.GetComponent<UnitBase>();
            //ユニットでないなら処理を飛ばす
            if (unitBase == null) continue;
            //ユニットにバフコンポーネントを追加
            AttackBuffEffect attackBuff = unitBase.gameObject.AddComponent<AttackBuffEffect>();
            //追加したコンポーネントの初期化処理
            //付加攻撃力と継続時間を設定
            attackBuff.Init(unitBase, m_additiveAttack, m_boostDuration);

        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }


}
