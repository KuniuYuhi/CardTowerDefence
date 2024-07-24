using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Breaker : UnitBase
{
    [SerializeField, Header("範囲攻撃の中心")]
    Transform m_skillAttackTRS;

    [SerializeField, Header("ダメージを与える範囲")]
    float m_skillHitRange;

    FieldOfViewDetector m_fieldOfViewDetector;


    /// <summary>
    /// 範囲内のユニットにダメージを与える。アニメーションイベント
    /// </summary>
    public void ApplyAreaDamageEvent()
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            m_skillAttackTRS.position, m_skillHitRange, m_fieldOfViewDetector.GetDetectionLayer()
            );

        foreach (var hitCollider in hitColliders)
        {
            //被ダメージインターフェースを継承した
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();

            if (damageable == null) return;
            //ダメージを与える
            damageable.Damage(GetCurrentAttackPower());
        }
    }


    /// <summary>
    /// アニメーターのパラメータを設定
    /// </summary>
    protected override void SetAnimatorParameters()
    {
        m_animator.SetFloat("RunSpeed", GetCharacterController().GetWalkAnimationBlendSpeed());

        m_animator.SetBool("IsMove", GetCharacterController().GetIsMove());

        if (m_normalAttackAction)
        {
            m_animator.SetTrigger("NormalAttack");
            m_normalAttackAction = false;
        }


    }

    /// <summary>
    /// 死亡アニメーションが終わった後の処理
    /// </summary>
    public override void ProcessDie()
    {
        Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();

        m_fieldOfViewDetector = GetComponent<FieldOfViewDetector>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
    }
}
