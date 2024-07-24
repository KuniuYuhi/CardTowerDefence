using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControll : MonoBehaviour
{
    FieldOfViewDetector m_fieldOfViewDetector;

    NavMeshAgent m_navMeshAgent;

    GameObject m_targetObject;

    float m_stoppingDistance;

    Vector3 m_defaultDirection;


    float m_attackDistance;


    float m_walkAnimationBlendSpeed = 0.0f;

    bool m_isAttackable = false;


    bool m_isMove = false;


    public bool GetIsAttackable()
    {
        return m_isAttackable;
    }

    public void SetIsAttackable(bool flag)
    {
        m_isAttackable = flag;
    }


    public void SetTargetObject(GameObject targetObject)
    {
        m_fieldOfViewDetector.SetTargetGameObject(targetObject);
    }

    public GameObject GetTargetObject()
    {
        return m_targetObject;
    }

    public float GetWalkAnimationBlendSpeed()
    {
        return m_walkAnimationBlendSpeed;
    }

    public void SetmWalkAnimationBlendSpeed(float value)
    {
        m_walkAnimationBlendSpeed = value;
    }

    public bool GetIsMove()
    {
        return m_isMove;
    }

    public FieldOfViewDetector GetFieldOfViewDetector()
    {
        return m_fieldOfViewDetector;
    }


    private void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_fieldOfViewDetector = GetComponent<FieldOfViewDetector>();

        UnitBase pawnObject = GetComponent<UnitBase>();
        //�U���������X�e�[�^�X����擾
        m_attackDistance = pawnObject.GetRuntimeStatus().GetAttackRange();
        //��{�̕������擾
        m_defaultDirection = pawnObject.GetBaseObject().transform.forward;
        //��~�������X�e�[�^�X����擾
        m_stoppingDistance = pawnObject.GetRuntimeStatus().GetStopDistance();
        //�i�r���b�V���G�[�W�F���g�̃X�s�[�h�����j�b�g�̃X�s�[�h�ɕύX����
        m_navMeshAgent.speed = pawnObject.GetRuntimeStatus().GetSpeed();
    }



    public void MoveTo(float speed)
    {
        //���G�͈͓��Ō��������I�u�W�F�N�g���擾
        m_targetObject = m_fieldOfViewDetector.GetTargetGameObject();

        //���g�̍��G�͈͓��ɓ���̃I�u�W�F�N�g����������
        if (m_targetObject != null)
        {
            if(m_navMeshAgent.isStopped==false)
            {
                m_navMeshAgent.isStopped = true;
            }

            //�^�[�Q�b�g�Ɍ������Ĉړ�
            MoveToTarget(speed, m_targetObject.transform.position);
        }
        else
        {
            if (m_navMeshAgent.isStopped == true)
            {
                m_navMeshAgent.isStopped = false;
            }

            //�����łȂ���Α���̋��_�Ɍ������Ē��i
            MoveStraight(speed);
        }

    }

    public void MoveStraight(float speed)
    {
        //���̋����ɂȂ����狭���I�ɓG�̋��_�Ɍ������悤�ɂ���


        //Vector3 forwad = m_defaultDirection *  10.0f;

        Vector3 direction = m_defaultDirection;

        direction *= speed * Time.deltaTime;

        transform.position += direction;

        //m_navMeshAgent.SetDestination(forwad);

        //�A�j���[�V�����u�����h�Ŏg��
        m_walkAnimationBlendSpeed = 1f;

        m_isMove = true;
    }

    public void MoveToTarget(float speed,Vector3 target)
    {

        Vector3 direction = target - transform.position;

        //�����������قǒl���傫���A�߂Â��Ə�����


        //�x�N�g���̒������擾
        float directionToTarget = direction.magnitude;
        float moveStopDistance = CalcStoppingDistance(m_stoppingDistance);

       

        //�x�N�g���̋�������~�����������ȉ��Ȃ�ړ����Ȃ�
        if (directionToTarget <= moveStopDistance)
        {
            //�ړ����Ȃ�
            m_isMove = false;
            m_walkAnimationBlendSpeed = 0.0f;
            return;
        }

        float a = directionToTarget - moveStopDistance;
        //�A�j���[�V�����u�����h�Ŏg��
        m_walkAnimationBlendSpeed = Mathf.Clamp01(a);
        m_isMove = true;

        //���K��
        direction.Normalize();
        //�ړ�
        direction *= speed * Time.deltaTime;

        //���݂̍��W�Ɉړ��x�N�g�������Z
        transform.position += direction;

        
    }


    public bool IsAttackableTarget()
    {
        //���G�͈͓��Ō��������I�u�W�F�N�g���擾
        m_targetObject = m_fieldOfViewDetector.GetTargetGameObject();

        //�I�u�W�F�N�g�����G�͈͓��ɂ��Ȃ�������U���ł���ΏۂȂ�
        if (m_targetObject == null) return false;

        //�^�[�Q�b�g�I�u�W�F�N�g���玩�g�Ɍ������x�N�g�����v�Z
        Vector3 direction = m_targetObject.transform.position - transform.position;

        //�x�N�g���̒������擾
        float directionToTarget = direction.magnitude;
        float attackStopDistance = CalcStoppingDistance(m_attackDistance);

        //�x�N�g�����U���͈͓��Ȃ�
        if(directionToTarget <= attackStopDistance)
        {
            //�U���\
            return true;
        }
        //�U���s�\
        return false;
    }


    float CalcStoppingDistance(float stopDistance)
    {
        //�^�[�Q�b�g�I�u�W�F�N�g�Ǝ��g�̃R���C�_�[���擾
        Collider targetCollider = m_targetObject.GetComponent<Collider>();
        Collider myCollider = GetComponent<Collider>();

        //�e�R���C�_�[�̔��a���擾
        float targetRadius = targetCollider != null ? targetCollider.bounds.extents.magnitude : 1f;
        float myRadius = myCollider != null ? myCollider.bounds.extents.magnitude : 1f;

        //�R���C�_�[�̔��a�ƒ�~�����������𑫂��Č��ʓI�Ɏ~�܂肽���������v�Z
        float totalStoppingDistance = (targetRadius/2.0f) + (myRadius/2.0f) + stopDistance;

        return totalStoppingDistance;

    }


    public void Rotation()
    {
        if(m_targetObject!=null)
        {
            RotationFromTaget();
        }
        else
        {
            RotationFromForward();
        }

    }

    void RotationFromTaget()
    {
        // �G�[�W�F���g���ړI�n�ɓ��B���������m�F
        if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            // �^�[�Q�b�g�̕������v�Z
            Vector3 direction = (m_targetObject.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            //�^�[�Q�b�g�̂ق��ɉ�]
            transform.rotation = lookRotation;

            // �G�[�W�F���g�̑��x���[���ł��邱�Ƃ��m�F
            if (m_navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // �����ŃG�[�W�F���g�����S�ɒ�~���Ă��邱�Ƃ��m�F���邽�߂̏�����ǉ��ł��܂�
                // �K�v�ɉ����đ��̃A�N�V���������s���邱�Ƃ��\�ł�
            }
        }
    }

    void RotationFromForward()
    {
        Quaternion lookRotation = Quaternion.LookRotation(m_defaultDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

}
