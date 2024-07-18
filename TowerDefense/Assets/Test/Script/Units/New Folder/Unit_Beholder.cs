using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Beholder : UnitBase
{
    [SerializeField, Header("魔法発射地点")]
    Transform m_castPoint;

    /// <summary>
    /// 通常攻撃の魔法の詠唱。アニメーションイベントで呼ばれる
    /// </summary>
    /// <param name="MagicBallPrefab"></param>
    public void CastNormalSpellEvent(GameObject SpellPrefab)
    {
        if (SpellPrefab == null) return;
        //魔法発射地点からマジックボールを生成
        GameObject spell = Instantiate(SpellPrefab, m_castPoint.position, m_castPoint.rotation);
        MagicBall magicBall = spell.GetComponent<MagicBall>();
        //マジックボール生成時の初期化処理
        if(magicBall != null)
        {
            magicBall.Init(
           GetCharacterController().GetTargetObject().transform.position,
           GetCurrentAttackPower()
           );
        }
       
    }

    /// <summary>
    /// スキル攻撃の魔法の詠唱。アニメーションイベントで呼ばれる
    /// </summary>
    /// <param name="SpellPrefab"></param>
    public void CastSkillSpellEvent(GameObject SpellPrefab)
    {
        if (SpellPrefab == null) return;
        //魔法発射地点からマジックボールを生成
        GameObject spell = Instantiate(SpellPrefab, m_castPoint.position, m_castPoint.rotation);
        MagicBall magicBall = spell.GetComponent<MagicBall>();
        //マジックボール生成時の初期化処理
        magicBall.Init(
            GetCharacterController().GetTargetObject().transform.position,
            GetCurrentAttackPower()
            );
    }


    /// <summary>
    /// アニメーターのパラメータを設定
    /// </summary>
    protected override void SetAnimatorParameters()
    {
        m_animator.SetFloat("RunSpeed", GetCharacterController().GetWalkAnimationBlendSpeed());

        m_animator.SetBool("IsMove", GetCharacterController().GetIsMove());

        if (m_skillAttackAction)
        {
            m_animator.SetTrigger("SkillAttack");
            m_skillAttackAction = false;
        }

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
