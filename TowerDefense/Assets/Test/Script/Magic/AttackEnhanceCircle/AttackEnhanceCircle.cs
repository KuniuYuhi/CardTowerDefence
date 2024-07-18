using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnhanceCircle : MagicBase
{
    [SerializeField, Header("�t���U����")]
    int m_additiveAttack;

    [SerializeField, Header("���ʔ͈�")]
    float m_range;

    [SerializeField, Header("���ʑΏۂ̃��C���[")]
    LayerMask m_additiveAttackLayers;

    [SerializeField, Header("���ʎ�������")]
    float boostDuration=10.0f;


    protected override void Awake()
    {
        base.Awake();
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        AttackBoost();

        Destroy(gameObject);
    }

   
    void AttackBoost()
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, m_range, m_additiveAttackLayers
            );

        //�q�b�g�����R���C�_�[�ɍU���͂𑝉�������R���|�[�l���g��ǉ�����
        foreach (var hitCollider in hitColliders)
        {
            UnitBase unitBase = hitCollider.gameObject.GetComponent<UnitBase>();
            //���j�b�g�łȂ��Ȃ珈�����΂�
            if (unitBase == null) continue;
            //���j�b�g�Ƀo�t�R���|�[�l���g��ǉ�
            AttackBuffEffect attackBuff = unitBase.gameObject.AddComponent<AttackBuffEffect>();
            //�ǉ������R���|�[�l���g�̏���������
            attackBuff.Init(unitBase, m_additiveAttack, boostDuration);

        }
    }


}
