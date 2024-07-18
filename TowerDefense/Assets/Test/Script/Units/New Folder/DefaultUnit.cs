using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnit : UnitBase
{

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
