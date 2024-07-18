using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : SpawnableBase, IUnit,IDamageable
{
    //[SerializeField, Header("�����̏������鋒�_")]
    //GameObject m_myBaseObject;

    [SerializeField,Header("�L�����N�^�[�̃X�e�[�^�X")]
    Status_Character m_originalCharaStatus;

    Status_Character m_runtimeStatus;      //���s���Ɏg���X�e�[�^�X

    UnitStateContext m_pawnStateContext;        //�v���C���[�̏�Ԃ��Ǘ�

    AnimationEventScript m_eventScript;

    protected Animator m_animator;

    AttackTimer m_attackTimer;

    CharacterControll m_charaCon;

    protected bool m_normalAttackAction = false;

    protected bool m_skillAttackAction = false;

    protected bool m_isAttackActive = false;


    int m_addAttackPower = 0;           //�o�t�Ȃǂ̌��ʂŏ�悹����U����

    /// <summary>
    /// �t���U���͂�ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SetAddAttackPower(int value)
    {
        m_addAttackPower = value;
    }
    /// <summary>
    /// �t���U���͂��擾
    /// </summary>
    /// <returns></returns>
    public int GetAddAttackPower()
    {
        return m_addAttackPower;
    }

    ///// <summary>
    ///// ���g�̋��_�I�u�W�F�N�g��ݒ�
    ///// </summary>
    ///// <returns></returns>
    //public void SetBaseObject(GameObject baseObject)
    //{
    //    m_myBaseObject = baseObject;
    //}
    ///// <summary>
    ///// ���g�̋��_�I�u�W�F�N�g���擾
    ///// </summary>
    ///// <returns></returns>
    //public GameObject GetBaseObject()
    //{
    //    return m_myBaseObject;
    //}

    /// <summary>
    /// �U���^�C�}�[���擾
    /// </summary>
    /// <returns></returns>
    public AttackTimer GetAttackTimer()
    {
        return m_attackTimer;
    }
    /// <summary>
    /// �L�����R���̎擾
    /// </summary>
    /// <returns></returns>
    public CharacterControll GetCharacterController()
    {
        return m_charaCon;
    }
    /// <summary>
    /// �A�j���[�^�[���擾
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimator()
    {
        return m_animator;
    }
    /// <summary>
    /// ���s���̃X�e�[�^�X���擾
    /// </summary>
    /// <returns></returns>
    public Status_Character GetRuntimeStatus()
    {
        return m_runtimeStatus;
    }
    /// <summary>
    /// �U�������̃t���O��ݒ�
    /// </summary>
    /// <param name="flag"></param>
    public void SetIsAttackActive(bool flag)
    {
        m_isAttackActive = flag;
    }
    /// <summary>
    /// �U�������̃t���O���擾
    /// </summary>
    /// <returns></returns>
    public bool GetIsAttackActive()
    {
        return m_isAttackActive;
    }
    /// <summary>
    /// �A�j���[�V�����C�x���g�X�N���v�g���擾
    /// </summary>
    /// <returns></returns>
    public AnimationEventScript GetAnimationEventScript()
    {
        return m_eventScript;
    }

    /// <summary>
    /// ���݂̃X�e�[�g�ɉ������U���͂��擾
    /// </summary>
    /// <returns></returns>
    public int GetCurrentAttackPower()
    {
        //�U���͂͒ʏ�U���̍U����
        int atk = m_runtimeStatus.GetNormalAttack();
        //�X�e�[�g���X�L���U���Ȃ�΁A�U���͂��X�L���U���͂ɕύX
        if (m_pawnStateContext.GetCurrentUnitState().GetUnitState() == 
            En_UnitState.enUnitState_SkillAttack)
        {
            atk = m_runtimeStatus.GetSkillAttack();
        }

        //�t���U���͂𑫂�
        atk += m_addAttackPower;

        return atk;
    }

    /// <summary>
    /// �A�j���[�^�[�̃p�����[�^��ݒ�
    /// </summary>
    protected virtual void SetAnimatorParameters() { }

    protected virtual void Awake()
    {
        //�I���W�i���̃f�[�^����Q�[�����Ɏg���f�[�^�ɃR�s�[
        m_runtimeStatus = Instantiate(m_originalCharaStatus);
        //�X�e�[�^�X��������
        m_runtimeStatus.InitStatus();

        //�X�e�[�g�Ǘ��N���X�𐶐�
        m_pawnStateContext = new UnitStateContext();
        //�X�e�[�g��������
        m_pawnStateContext.Init(this, En_UnitState.enUnitState_Walk);   
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        //�e��R���|�[�l���g���擾
        m_animator = GetComponent<Animator>();
        //�U���C���^�[�o�����v��^�C�}�[�𐶐�
        m_attackTimer = new AttackTimer(
            m_runtimeStatus.GetNormalAtkIntarval(),
            m_runtimeStatus.GetSkillAtkIntarval());
        //�L�����R���R���|�[�l���g���擾
        m_charaCon = GetComponent<CharacterControll>();
        //�A�j���[�V�����C�x���g�R���|�[�l���g���擾
        m_eventScript = GetComponent<AnimationEventScript>();
        //�L�����N�^�[���ړ������ɉ�]
        transform.rotation = GetBaseObject().transform.rotation;
    }

    /// <summary>
    /// ���S�A�j���[�V�������I�������̏���
    /// </summary>
    public virtual void ProcessDie() { }

    //�ړ������Ȃ�
    protected virtual void FixedUpdate()
    {
        //���݂̃X�e�[�g�̍X�V����
        m_pawnStateContext.FixedUpdate();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //�U�����̓^�C�}�[�𓮂����Ȃ�
        if(!m_isAttackActive)
        {
            //�U���p�^�C�}�[���X�V
            m_attackTimer.Update();
        }
        //���݂̃X�e�[�g�̍X�V����
        m_pawnStateContext.Update();
        //�A�j���[�^�[�̃p�����[�^��ݒ�
        SetAnimatorParameters();

        Debug.Log(m_pawnStateContext.GetCurrentUnitState());
    }

    /// <summary>
    /// ���ʂ̏�ԑJ�ڏ���
    /// </summary>
    /// <returns>���݂̏�Ԃ���J�ڂ���Ȃ�true�A���Ȃ��Ȃ�false</returns>
    public bool CommonStateTransition()
    {
        //�������~�߂�

        //�^�[�Q�b�g���߂��ɂ���Ȃ�U��
        if(m_charaCon.IsAttackableTarget())
        {
            //�ʏ�U���A�X�L���U���̂ǂ��炩���g�p�����Ȃ�
            if (IsActionAttack())
            {
                //�U����
                m_isAttackActive = true;

                m_eventScript.ActiveStart();
                return true;
            }
        }

        //�ҋ@��Ԃƕ������
        //�^�[�Q�b�g�̋߂��ɂ���Ȃ�ҋ@���
        if (m_charaCon.GetIsMove())
        {
            ChangeStateWalk();
        }
        else
        {
            ChangeStateIdle();
        }

        
        //��Ԃ͕ς��Ȃ�
        return false;
    }

    protected virtual bool IsActionAttack()
    {
        //�U�����͏������Ȃ�
        if (m_isAttackActive) return false;

        //�X�L���U�����ł����ԂȂ�
        if(DecideSkillAttack()) return true;

        //�ʏ�U�����ł����ԂȂ�
        if (DecideNormalAttack()) return true;
        

        //�U�����Ȃ�
        return false;
    }

    protected bool DecideNormalAttack()
    {
        //�ʏ�U�����ł����ԂȂ�
        if (m_attackTimer.IsNormalAttackable())
        {
            m_normalAttackAction = true;
            ChangeStateNormalAttack();
            return true;
        }

        return false;
    }

    protected bool DecideSkillAttack()
    {
        //�X�L���U�����ł����ԂȂ�
        if (m_attackTimer.IsSkillAttackable())
        {
            m_skillAttackAction = true;
            ChangeStateSkillAttack();
            return true;
        }

        return false;
    }


    public void MoveTo()
    {
        //�ړ�����
        m_charaCon.MoveTo(m_runtimeStatus.GetSpeed());
    }

    public void Rotation()
    {
        //��]����
        m_charaCon.Rotation();
    }

    /// <summary>
    /// HP���񕜂���
    /// </summary>
    /// <param name="recoveryHp"></param>
    public void RecoverHp(int recoveryHp)
    {
        m_runtimeStatus.RecoverHp(recoveryHp);
    }

    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="hitDamage">�󂯂�_���[�W</param>
    public void Damage(int hitDamage)
    {
        m_runtimeStatus.ApplyDamage(hitDamage);

        Debug.Log(transform.name + "��" + hitDamage + "���󂯂�");

        if (m_runtimeStatus.GetHp()<=0)
        {
            Die();
        }
    }

    /// <summary>
    /// ���S������
    /// </summary>
    public void Die()
    {
        m_animator.SetTrigger("Die");
        //���S��Ԃɐ؂�ւ�
        ChangeStateDie();

        Debug.Log(transform.name + "�͓|���ꂽ");
    }

    ///////////////////////////////////////////////////////////////////////////////////////
    /// �e�X�e�[�g�N���X�؂�ւ�����
    ///////////////////////////////////////////////////////////////////////////////////////

    //�ҋ@��Ԃɐ؂�ւ�
    public void ChangeStateIdle() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_Idle);

    //������Ԃɐ؂�ւ�
    public void ChangeStateWalk() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_Walk);

    //�ʏ�U����Ԃɐ؂�ւ�
    public void ChangeStateNormalAttack() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_normalAttack);

    //�X�L���U����Ԃɐ؂�ւ�
    public void ChangeStateSkillAttack() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_SkillAttack);

    //���S��Ԃɐ؂�ւ�
    public void ChangeStateDie() =>
        m_pawnStateContext.ChangeState(En_UnitState.enUnitState_Die);

   
}
