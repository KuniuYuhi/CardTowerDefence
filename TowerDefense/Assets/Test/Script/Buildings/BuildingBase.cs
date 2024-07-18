using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : SpawnableBase, IDamageable
{
    [SerializeField, Header("�X�e�[�^�X")]
    Status_Building m_originalStatus;

    Status_Building m_runtimeStatus;      //���s���Ɏg���X�e�[�^�X


    float m_intarvalTimer = 0.0f;

    bool m_isAttackable = false;            //�U���\��

    public Status_Building GetRuntimeStatus()
    {
        return m_runtimeStatus;
    }


    public bool IsAttackable()
    {
        return m_isAttackable;
    }

    public void SetAttacable(bool flag)
    {
        m_isAttackable = flag;
    }

    /// <summary>
    /// �U����̏���
    /// </summary>
    public void AfterShot()
    {
        //�U���s�\�ɂ���
        SetAttacable(false);
        //�^�C�}�[�����Z�b�g
        m_intarvalTimer = 0.0f;
    }

    protected virtual void Awake()
    {
        //�I���W�i���̃f�[�^����Q�[�����Ɏg���f�[�^�ɃR�s�[
        m_runtimeStatus = Instantiate(m_originalStatus);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
      
        //���������炷���ɍU���ł���
        m_intarvalTimer = m_runtimeStatus.GetIntarval();
        //�U���s�\
        m_isAttackable = false;

    }

    // Update is called once per frame
    void Update()
    {
        //�^�C�}�[�𓮂���
        UpdateIntarvalTimer();


    }

    protected void UpdateIntarvalTimer()
    {
        //�U���\�Ȃ�^�C�}�[�𓮂����Ȃ�
        if (m_isAttackable) return;

        //�C���^�[�o���ɒB������
        if(m_intarvalTimer >= m_runtimeStatus.GetIntarval())
        {
            //�U���\
            m_isAttackable = true;

            Debug.Log("�U���\�I�I�I");
            return;
        }
        //�^�C�}�[�����Z
        m_intarvalTimer += Time.deltaTime;
    }

    
    



    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="hitDamage">�󂯂�_���[�W</param>
    public void Damage(int hitDamage)
    {
        m_runtimeStatus.ApplyDamage(hitDamage);

        Debug.Log(transform.name + "��" + hitDamage + "�󂯂�" );

        if (m_runtimeStatus.GetEndurance()<=0)
        {
            Die();
        }
    }

    /// <summary>
    /// ���S������
    /// </summary>
    public void Die()
    {
        
        Debug.Log(transform.name + "�͓|���ꂽ");

        Destroy(gameObject);

    }
}
