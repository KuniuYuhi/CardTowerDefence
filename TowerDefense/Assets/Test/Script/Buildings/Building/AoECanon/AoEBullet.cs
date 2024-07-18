using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEBullet : MonoBehaviour
{
    SphereCollider m_collider;

    BuildingAtkObject m_buildingAtkObject;

    [SerializeField,Header("�Ԃ������甚�����郌�C���[")]
    LayerMask m_groundLayer;

    [SerializeField, Header("�_���[�W��^���������C���[")]
    LayerMask m_hitLayer;

    [SerializeField, Header("�����͈͂̔��a")]
    float m_explosionRadius;

    bool isExpload = false;



    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<SphereCollider>();
        //�U���p�I�u�W�F�N�g�R���|�[�l���g���擾
        m_buildingAtkObject = GetComponent<BuildingAtkObject>();
        //�_���[�W�����������Ȃ��悤�ɂ���
        m_buildingAtkObject.SetIsDamageable(false);
        
        m_collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isExpload)
        {
            Destroy(gameObject);
        }
    }

    void Explosion()
    {
        //�_���[�W��^������悤�ɂ���
        m_buildingAtkObject.ApplyAreaDamage(m_hitLayer, m_explosionRadius);
        //�R���C�_�[�̔��a��ύX
        m_collider.radius = m_explosionRadius;
        //��������
        isExpload = true;
        //�G�t�F�N�g

    }




    private void OnTriggerEnter(Collider other)
    {
        //�Փ˂����R���C�_�[�̃��C���[������̃��C���[�Ȃ�
        if(((1<< other.gameObject.layer)& m_groundLayer) != 0)
        {
            //����������
            Explosion();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }

}
