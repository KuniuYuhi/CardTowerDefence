using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAtkObject : MonoBehaviour
{
    [SerializeField, Header("�I�u�W�F�N�g���������̃X�e�[�^�X")]
    Status_Building status;

    bool m_isDamageable = true;
   

    public void SetIsDamageable(bool flag)
    {
        m_isDamageable = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�_���[�W��^�����Ȃ��Ȃ珈�����Ȃ�
        if (!m_isDamageable) return;

        //��_���[�W�C���^�[�t�F�[�X���p������
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.Damage(status.GetEffectPower());

    }

    /// <summary>
    /// ���g�𒆐S�ɔ͈͓��̓���̃��C���[�̃I�u�W�F�N�g�Ƀ_���[�W��^����
    /// </summary>
    /// <param name="includeLayer">���ׂ������C���[</param>
    /// <param name="radius">���ׂ锼�a</param>
    public void ApplyAreaDamage(LayerMask includeLayer,float radius)
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, radius*2.0f, includeLayer
            );

        foreach (var hitCollider in hitColliders)
        {
            //��_���[�W�C���^�[�t�F�[�X���p������
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();

            if (damageable == null) return;

            damageable.Damage(status.GetEffectPower());
        }
    }

}
