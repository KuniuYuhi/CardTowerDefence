using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnhanceCircle : MagicBase
{
    [SerializeField, Header("���ʑΏۂ̃��C���[")]
    LayerMask m_additiveAttackLayers;

    int m_additiveAttack;
    float m_radius;
    float m_boostDuration;


    protected override void Awake()
    {
        base.Awake();
        //�X�e�[�^�X����t���U���͂Ɣ��a�ƌ��ʌp�����Ԃ��擾
        m_additiveAttack = (int)GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("�t���U����");
        m_radius = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("���a");
        m_boostDuration = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("���ʎ�������");
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
            transform.position, m_radius, m_additiveAttackLayers
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
            //�t���U���͂ƌp�����Ԃ�ݒ�
            attackBuff.Init(unitBase, m_additiveAttack, m_boostDuration);

        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }


}
