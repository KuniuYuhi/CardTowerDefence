using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : CanonType
{
    Transform m_childTRS;

    
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //���_�Ɠ�����]�ɐݒ�
        transform.rotation = GetBaseObject().transform.rotation;

        //�q�I�u�W�F�N�g(�^���[�̑�C�̕���)���擾
        m_childTRS = transform.Find("Tower_Top");


    }

    private void FixedUpdate()
    {
        if (m_rangeObjectDetector.GetTargetGameObject() == null) return;

        //��C�̕������^�[�Q�b�g�̂ق��ɉ�]������
        RotationChild();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntarvalTimer();

        Attack();



    }

    void Attack()
    {
        //�^�[�Q�b�g�̃I�u�W�F�N�g�����݂��A�U���\�Ȃ�
        if (m_rangeObjectDetector.GetTargetGameObject() != null&& IsAttackable() == true)
        {
            //�^�[�Q�b�g�̃I�u�W�F�N�g�ɍU��
            Shot(m_rangeObjectDetector.GetTargetGameObject());
        }
    }

    void RotationChild()
    {

        // �^�[�Q�b�g�̕������v�Z
        Vector3 direction = (m_rangeObjectDetector.GetTargetGameObject().transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // �������^�[�Q�b�g�̕�������
        m_childTRS.transform.rotation = Quaternion.Slerp(
            m_childTRS.transform.rotation, lookRotation, Time.deltaTime * 10f
            );
    }


}
