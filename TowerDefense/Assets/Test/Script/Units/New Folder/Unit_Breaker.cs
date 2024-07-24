using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Breaker : UnitBase
{
    [SerializeField, Header("�͈͍U���̒��S")]
    Transform m_skillAttackTRS;

    [SerializeField, Header("�_���[�W��^����͈�")]
    float m_skillHitRange;

    FieldOfViewDetector m_fieldOfViewDetector;


    /// <summary>
    /// �͈͓��̃��j�b�g�Ƀ_���[�W��^����B�A�j���[�V�����C�x���g
    /// </summary>
    public void ApplyAreaDamageEvent()
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            m_skillAttackTRS.position, m_skillHitRange, m_fieldOfViewDetector.GetDetectionLayer()
            );

        foreach (var hitCollider in hitColliders)
        {
            //��_���[�W�C���^�[�t�F�[�X���p������
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();

            if (damageable == null) return;
            //�_���[�W��^����
            damageable.Damage(GetCurrentAttackPower());
        }
    }


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
