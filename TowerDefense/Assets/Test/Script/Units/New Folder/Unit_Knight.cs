using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Knight : UnitBase
{

    bool m_isRecoveried = false;

    int m_harfHp;



    /// <summary>
    /// �A�j���[�^�[�̃p�����[�^��ݒ�
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
    /// ���S�A�j���[�V�������I�������̏���
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

        //�ő�HP���擾
        m_harfHp = GetRuntimeStatus().GetMaxHp();
        //������HP���v�Z
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
    /// ���̃��j�b�g�͒ʏ�U���̂�
    /// </summary>
    /// <returns></returns>
    protected override bool IsActionAttack()
    {
        //�U�����͏������Ȃ�
        if (m_isAttackActive) return false;

        //�ʏ�U�����ł����ԂȂ�
        if (DecideNormalAttack()) return true;


        //�U�����Ȃ�
        return false;
    }



    void RecoveryHP()
    {
        //��x�񕜂����珈�����Ȃ�
        if (m_isRecoveried) return;


        //HP�������ɂȂ������x����HP�𔼕���
        if(GetRuntimeStatus().GetHp() <= m_harfHp)
        {
            m_isRecoveried = true;
            //HP�𔼕���
            GetRuntimeStatus().RecoverHp(m_harfHp);

        }

        

    }



}
