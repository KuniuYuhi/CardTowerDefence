using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Knight : UnitBase
{

    bool m_isRecoveried = false;

    int m_harfHp;



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
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //最大HPを取得
        m_harfHp = GetRuntimeStatus().GetMaxHp();
        //半分のHPを計算
        m_harfHp /= 2;
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


        RecoveryHP();

    }


    /// <summary>
    /// このユニットは通常攻撃のみ
    /// </summary>
    /// <returns></returns>
    protected override bool IsActionAttack()
    {
        //攻撃中は処理しない
        if (m_isAttackActive) return false;

        //通常攻撃ができる状態なら
        if (DecideNormalAttack()) return true;


        //攻撃しない
        return false;
    }



    void RecoveryHP()
    {
        //一度回復したら処理しない
        if (m_isRecoveried) return;


        //HPが半分になったら一度だけHPを半分回復
        if(GetRuntimeStatus().GetHp() <= m_harfHp)
        {
            m_isRecoveried = true;
            //HPを半分回復
            GetRuntimeStatus().RecoverHp(m_harfHp);

        }

        

    }



}
