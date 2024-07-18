using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeObjectDetector : MonoBehaviour
{
    [SerializeField, Header("���o���������C���[")]
    LayerMask m_detectionLayer;

    float m_detectionRange;         //���G�͈�

    [SerializeField,Header("�T���I�u�W�F�N�g")]
    BuildingBase m_buildingObject;  //�T���I�u�W�F�N�g

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


    // Start is called before the first frame update
    void Start()
    {
        m_targetObject = null;

        m_detectionRange = m_buildingObject.GetRuntimeStatus().GetRadius();

    }

    // Update is called once per frame
    void Update()
    {
        //�_���̃I�u�W�F�N�g���Ȃ��Ȃ�Ȃ������̓I�u�W�F�N�g��T���Ȃ�
        //if (m_targetObject != null)
        //{
        //    return;
        //}

        //�C���^�[�o���ɒB������I�u�W�F�N�g��T��
        if (IsFindIntarval())
        {
            FindHitObjectInRange(m_detectionRange);
        }
    }

    bool IsFindIntarval()
    {
        if (m_timer >= detectionInterval)
        {
            m_timer = 0.0f;
            return true;
        }

        m_timer += Time.deltaTime;
        return false;
    }

    /// <summary>
    /// �͈͓��ň�ԋ߂��I�u�W�F�N�g��T���i1�́j
    /// </summary>
    /// <param name="range">�T���͈�</param>
    /// <returns></returns>
    GameObject FindHitObjectInRange(float range)
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, range, m_detectionLayer
            );

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        //���������R���C�_�[�̐����J��Ԃ�
        foreach(var hitCollider in hitColliders)
        {
            //�R���C�_�[���玩�g�̍��W�܂ł̒��������߂�
            float distance = Vector3.Distance(
                hitCollider.transform.position, transform.position
                );

            //�������͈͓��ł����
            if(distance<= range)
            {
                //�v�Z������������ԋ߂��������Z���Ȃ�
                if(distance < closestDistance)
                {
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
    }

}
