using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : MagicBase
{
    [SerializeField, Header("�񕜗�")]
    int m_healAmount;

    [SerializeField, Header("�񕜔͈�")]
    float m_range;

    [SerializeField, Header("�񕜑Ώۂ̃��C���[")]
    LayerMask m_healLayers;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //�񕜂���
        Heal();

        Destroy(gameObject);
    }


    void Heal()
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, m_range, m_healLayers
            );

        foreach (var hitCollider in hitColliders)
        {
            UnitBase unitBase = hitCollider.GetComponent<UnitBase>();
            //���j�b�g�łȂ��Ȃ珈�����Ȃ�
            if (unitBase == null) continue;
            //���j�b�g��HP���񕜂���
            unitBase.RecoverHp(m_healAmount);

        }

    }

   
}
