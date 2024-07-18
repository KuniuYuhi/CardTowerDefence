using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoECannon : AreaEffectType
{
    [SerializeField, Header("�e�𔭎˂���|�C���g")]
    Transform m_shotPoint;

    [SerializeField, Range(0F, 90F),Header("�e�𔭎˂���p�x")]
    float m_throwingAngle;

    Transform m_childRotaterTRS;


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        transform.rotation = GetBaseObject().transform.rotation;

        // �q�I�u�W�F�N�g(�^���[�̑�C�̕���)���擾
        m_childRotaterTRS = transform.Find("rotater");
    }

    private void FixedUpdate()
    {
        if (m_rangeObjectDetector.GetTargetGameObject() == null) return;

        //��C�̕������^�[�Q�b�g�̂ق��ɉ�]������
        RotationChildRotater();
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
        if (m_rangeObjectDetector.GetTargetGameObject() != null && IsAttackable() == true)
        {
            //�^�[�Q�b�g�̃I�u�W�F�N�g�ɍU��
            Shot(m_rangeObjectDetector.GetTargetGameObject(), m_shotPoint, m_throwingAngle);
        }
    }

    void RotationChildRotater()
    {

        // �^�[�Q�b�g�̕������v�Z
        Vector3 direction = (m_rangeObjectDetector.GetTargetGameObject().transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // �������^�[�Q�b�g�̕�������
        m_childRotaterTRS.transform.rotation = Quaternion.Slerp(
            m_childRotaterTRS.transform.rotation, lookRotation, Time.deltaTime * 10f
            );
    }

}
