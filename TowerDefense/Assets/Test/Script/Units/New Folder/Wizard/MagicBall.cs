using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    Rigidbody m_rigidbody;

    Vector3 m_targetPosition = Vector3.zero;

    float m_destroyTime = 3.0f;        //�I�u�W�F�N�g����������܂ł̎���

    float m_speed = 7.0f;               //�}�W�b�N�{�[���̃X�s�[�h

    int m_attackPower = 0;


    /// <summary>
    /// �ڕW�̍��W��ݒ�
    /// </summary>
    /// <param name="targetposition"></param>
    public void SetTargetPosition(Vector3 targetposition)
    {
        m_targetPosition = targetposition;
    }

    public void SetAttackPower(int attackPower)
    {
        m_attackPower = attackPower;
    }

    /// <summary>
    /// �������̏���������
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="attackPower"></param>
    public void Init(Vector3 targetPosition,int attackPower)
    {
        SetTargetPosition(targetPosition);
        SetAttackPower(attackPower);
    }


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
       
        if(m_targetPosition.magnitude==0.0f)
        {
            m_targetPosition = transform.forward;
        }
        else
        {
            m_targetPosition -= transform.position;
        }

        m_rigidbody.velocity = m_targetPosition.normalized * m_speed;

        //�������Ԃ̏������R���[�`���œ�����
        StartCoroutine(CountDown());



    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(m_destroyTime);

        //��莞�ԑ҂����狭���I�ɔ���
        Explosion();
    }

    /// <summary>
    /// ���������i�����j
    /// </summary>
    void Explosion()
    {
        //

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageProcessor.HitDamage(other.gameObject, m_attackPower);


        //Explosion();
    }

}
