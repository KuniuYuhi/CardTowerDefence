using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnhanceCircle : MagicBase
{
    [SerializeField, Header("付加攻撃力")]
    int m_additiveAttack;

    [SerializeField, Header("効果範囲")]
    float m_range;

    [SerializeField, Header("効果対象のレイヤー")]
    LayerMask m_additiveAttackLayers;

    [SerializeField, Header("効果持続時間")]
    float boostDuration=10.0f;


    protected override void Awake()
    {
        base.Awake();
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
            transform.position, m_range, m_additiveAttackLayers
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
            attackBuff.Init(unitBase, m_additiveAttack, boostDuration);

        }
    }


}
