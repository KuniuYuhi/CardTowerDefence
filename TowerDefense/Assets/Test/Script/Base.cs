using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour,IDamageable
{
    [SerializeField, Header("�X�e�[�^�X")]
    Status_Base m_originalBaseStatus;

    Status_Base m_runtimeBaseStatus;      //���s���Ɏg���X�e�[�^�X


    bool m_isDestroyed = false;


    public Status_Base GetStatus()
    {
        return m_runtimeBaseStatus;
    }

    /// <summary>
    /// �j�󂳂ꂽ��
    /// </summary>
    /// <returns></returns>
    public bool IsDestroyed()
    {
        return m_isDestroyed;
    }



    // Start is called before the first frame update
    void Start()
    {
        //�I���W�i���̃f�[�^����Q�[�����Ɏg���f�[�^�ɃR�s�[
        m_runtimeBaseStatus = Instantiate(m_originalBaseStatus);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="hitDamage">�󂯂�_���[�W</param>
    public void Damage(int hitDamage)
    {
        m_runtimeBaseStatus.ApplyDamage(hitDamage);

        if(m_runtimeBaseStatus.GetEndurance()<=0)
        {
            Die();
        }

        Debug.Log(transform.name + "�ɂ�"+ m_runtimeBaseStatus.GetEndurance());
    }

    /// <summary>
    /// ���S������
    /// </summary>
    public void Die()
    {
        Debug.Log(transform.name + "�͕��󂵂�");
        m_isDestroyed = true;
    }
}
