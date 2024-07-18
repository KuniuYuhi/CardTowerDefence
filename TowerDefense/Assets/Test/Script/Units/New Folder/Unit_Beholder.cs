using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Beholder : UnitBase
{
    [SerializeField, Header("���@���˒n�_")]
    Transform m_castPoint;

    /// <summary>
    /// �ʏ�U���̖��@�̉r���B�A�j���[�V�����C�x���g�ŌĂ΂��
    /// </summary>
    /// <param name="MagicBallPrefab"></param>
    public void CastNormalSpellEvent(GameObject SpellPrefab)
    {
        if (SpellPrefab == null) return;
        //���@���˒n�_����}�W�b�N�{�[���𐶐�
        GameObject spell = Instantiate(SpellPrefab, m_castPoint.position, m_castPoint.rotation);
        MagicBall magicBall = spell.GetComponent<MagicBall>();
        //�}�W�b�N�{�[���������̏���������
        if(magicBall != null)
        {
            magicBall.Init(
           GetCharacterController().GetTargetObject().transform.position,
           GetCurrentAttackPower()
           );
        }
       
    }

    /// <summary>
    /// �X�L���U���̖��@�̉r���B�A�j���[�V�����C�x���g�ŌĂ΂��
    /// </summary>
    /// <param name="SpellPrefab"></param>
    public void CastSkillSpellEvent(GameObject SpellPrefab)
    {
        if (SpellPrefab == null) return;
        //���@���˒n�_����}�W�b�N�{�[���𐶐�
        GameObject spell = Instantiate(SpellPrefab, m_castPoint.position, m_castPoint.rotation);
        MagicBall magicBall = spell.GetComponent<MagicBall>();
        //�}�W�b�N�{�[���������̏���������
        magicBall.Init(
            GetCharacterController().GetTargetObject().transform.position,
            GetCurrentAttackPower()
            );
    }


    /// <summary>
    /// �A�j���[�^�[�̃p�����[�^��ݒ�
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
