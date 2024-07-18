using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonType : BuildingBase
{
    protected RangeObjectDetector m_rangeObjectDetector;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        m_rangeObjectDetector = GetComponent<RangeObjectDetector>();

    }

    /// <summary>
    /// ���ڌ���
    /// </summary>
    /// <param name="targetObject">���Ώۂ̃I�u�W�F�N�g</param>
    protected void Shot(GameObject targetObject)
    {
        //�_���[�W�����p�C���^�[�t�F�[�X���^�[�Q�b�g����擾
        IDamageable damageable = targetObject.GetComponent<IDamageable>();
        //�_���[�W�����p�C���^�[�t�F�[�X�������Ă��Ȃ�������
        if (damageable==null)
        {
            //���ĂȂ�
            return;
        }
        //�_���[�W��^����
        damageable.Damage(GetRuntimeStatus().GetEffectPower());

        //�U�������̂ŁA�C���^�[�o�����I���܂ł͍U���ł��Ȃ��悤�ɂ���
        AfterShot();

    }

}
