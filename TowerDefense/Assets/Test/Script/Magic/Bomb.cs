using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MagicBase
{
    [SerializeField, Header("�U����")]
    int m_attack;

    [SerializeField, Header("�����͈�")]
    float m_range;

    [SerializeField, Header("�^�C�����~�b�g")]
    int m_timeLimit;

    [SerializeField, Header("�����������郌�C���[")]
    LayerMask m_hitLayers;

    protected override void Awake()
    {
        base.Awake();
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
        yield return new WaitForSeconds(m_timeLimit);

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
            transform.position, m_range, m_hitLayers
            );

        foreach (var hitCollider in hitColliders)
        {
            DamageProcessor.HitDamage(hitCollider.gameObject, m_attack);
        }
    }

}
