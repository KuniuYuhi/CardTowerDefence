using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MagicBase
{
    [SerializeField, Header("�����������郌�C���[")]
    LayerMask m_hitLayers;


    int m_attackPower;
    float m_radius;
    float m_countDown;

    protected override void Awake()
    {
        base.Awake();
        //�X�e�[�^�X����U���͂Ɣ����͈͂̔��a�ƃJ�E���g�_�E�����擾
        m_attackPower = (int)GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("�U����");
        m_radius = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("���a");
        m_countDown = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("�J�E���g�_�E��");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //�R���[�`���ŃJ�E���g�_�E���̏������n�߂�
        StartCoroutine(CountDown());
    }

    /// <summary>
    /// �J�E���g�_�E���̏���
    /// </summary>
    /// <returns></returns>
    IEnumerator  CountDown()
    {
        yield return new WaitForSeconds(m_countDown);

        //�^�C�����~�b�g�܂ő҂����̂Ŕ���������
        Explosion();
    }

    /// <summary>
    /// ��������
    /// </summary>
    void Explosion()
    {
        ApplyAreaDmage();

        Destroy(gameObject);
    }

    void ApplyAreaDmage()
    {
        //�͈͓��Ńq�b�g��������̃��C���[�����R���C�_�[���i�[����
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, m_radius, m_hitLayers
            );

        foreach (var hitCollider in hitColliders)
        {
            DamageProcessor.HitDamage(hitCollider.gameObject, m_attackPower);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

}
