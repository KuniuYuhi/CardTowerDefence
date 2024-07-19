using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewDetector : MonoBehaviour
{
    [SerializeField,Header("���o���������C���[")]
    LayerMask m_detectionLayer;

    float m_detectionRange;         //���G�͈�
    float m_fieldOfViewAngle;       //����p

    GameObject m_targetObject;      //�������I�u�W�F�N�g

    [SerializeField]
    float detectionInterval = 1.0f;

    float m_timer = 0.0f;


    /// <summary>
    /// �^�[�Q�b�g�̃I�u�W�F�N�g��Ԃ�
    /// </summary>
    /// <returns></returns>
    public GameObject GetTargetGameObject()
    {
        return m_targetObject;
    }

    public void SetTargetGameObject(GameObject targetObject)
    {
        m_targetObject = targetObject;
    }

    /// <summary>
    /// ���o���������C���[���擾
    /// </summary>
    /// <returns></returns>
    public LayerMask GetDetectionLayer()
    {
        return m_detectionLayer;
    }


    // Start is called before the first frame update
    void Start()
    {
        m_targetObject = null;

        UnitBase pawnBase = GetComponent<UnitBase>();

        m_detectionRange = pawnBase.GetRuntimeStatus().GetSearchRange();

        m_fieldOfViewAngle = pawnBase.GetRuntimeStatus().GetFieldOfViewAngle();


        m_timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //�_���̃I�u�W�F�N�g���Ȃ��Ȃ�Ȃ������̓I�u�W�F�N�g��T���Ȃ�
        if(m_targetObject!=null)
        {
            //todo ���߂��G�����Ȃ�������T���Ȃ�

            return;
        }

        //�C���^�[�o���ɒB������I�u�W�F�N�g��T��
        if(IsFindIntarval())
        {
            FindHitObjectInFieldOfView(m_detectionRange, m_fieldOfViewAngle);
        }

    }


    bool IsFindIntarval()
    {
        if(m_timer>= detectionInterval)
        {
            m_timer = 0.0f;
            return true;
        }

        m_timer += Time.deltaTime;
        return false;
    }

    GameObject FindHitObjectInFieldOfView(float range, float fOVAngle)
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, range, m_detectionLayer
            );

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        //���������R���C�_�[�̐��J��Ԃ�
        foreach (var hitCollider in hitColliders)
        {
            //���C���΂��ďՓ˓_�𒲂ׂ�


            //�R���C�_�[���玩�g�̍��W�܂ł̃x�N�g�������߂�
            Vector3 directionToTarget = hitCollider.transform.position - transform.position;
            //�O��������x�N�g���Ɍ������p�x�����߂�
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            //���߂��p�x���ݒ肵������p���Ȃ�
            if (angleToTarget <= fOVAngle / 2)
            {
                //�q�b�g�����R���C�_�[���m���ׂĎ��g�����ԋ߂��R���C�_�[��T��
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    //�����̍X�V
                    closestDistance = distance;
                    //�R���C�_�[����I�u�W�F�N�g�����擾
                    closestObject = hitCollider.gameObject;
                }
            }
        }

        //�_���̃I�u�W�F�N�g�ɐݒ�
        m_targetObject = closestObject;

        return closestObject;

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -m_fieldOfViewAngle / 2, 0) * transform.forward * m_detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, m_fieldOfViewAngle / 2, 0) * transform.forward * m_detectionRange;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
