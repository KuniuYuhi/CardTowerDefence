using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : AreaEffectType
{

    [SerializeField,Header("�_���[�W��^������I�u�W�F�N�g�̃��C���[")]
    LayerMask m_hitLayer;

    BuildingAtkObject m_buildingAtkObject;


    Transform m_crystalTRS;

    [SerializeField]
    float m_rotateSpeed;

    float m_defaultRotateSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // �q�I�u�W�F�N�g(�N���X�^��)���擾
        m_crystalTRS = transform.Find("Crystal");

        //�U���p�I�u�W�F�N�g�R���|�[�l���g���擾
        m_buildingAtkObject = GetComponent<BuildingAtkObject>();
        //�����蔻��Ń_���[�W�����������Ȃ��悤�ɂ���
        m_buildingAtkObject.SetIsDamageable(false);

        m_defaultRotateSpeed = m_rotateSpeed;
    }
    private void FixedUpdate()
    {
        RotationCrystal();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateIntarvalTimer();

        Attack();
    }

    void Attack()
    {
        //�U���\�ɂȂ�����
        if(IsAttackable())
        {
            //�͈͓��̓G�Ƀ_���[�W��^����
            m_buildingAtkObject.ApplyAreaDamage(
                m_hitLayer, 
                GetRuntimeStatus().GetRadius()
                );

            m_rotateSpeed *= m_rotateSpeed*2.0f;

            //�U����̏����i�^�C�}�[�A�t���O���Z�b�g�j
            AfterShot();
        }
    }


    void RotationCrystal()
    {
        if (m_rotateSpeed > m_defaultRotateSpeed)
        {
            m_rotateSpeed -= m_rotateSpeed * Time.deltaTime*2.0f;
        }
        else m_rotateSpeed = m_defaultRotateSpeed;

        m_crystalTRS.Rotate(0.0f, 0.0f, m_rotateSpeed * Time.deltaTime);
    }


}
