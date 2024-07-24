using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : MagicBase
{
    [SerializeField, Header("�񕜑Ώۂ̃��C���[")]
    LayerMask m_healLayers;

    int m_healAmount;
    float m_radius;


    protected override void Awake()
    {
        base.Awake();
        //�X�e�[�^�X����񕜗͂Ɣ��a���擾
        m_healAmount = (int)GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("�񕜗�");
        m_radius = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("���a");
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
            transform.position, m_radius, m_healLayers
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
