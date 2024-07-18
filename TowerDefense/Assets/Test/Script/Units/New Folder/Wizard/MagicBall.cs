using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    Rigidbody m_rigidbody;

    Vector3 m_targetPosition = Vector3.zero;

    float m_destroyTime = 3.0f;        //オブジェクトを消去するまでの時間

    float m_speed = 7.0f;               //マジックボールのスピード

    int m_attackPower = 0;


    /// <summary>
    /// 目標の座標を設定
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
    /// 生成時の初期化処理
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

        //制限時間の処理をコルーチンで動かす
        StartCoroutine(CountDown());



    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(m_destroyTime);

        //一定時間待ったら強制的に爆発
        Explosion();
    }

    /// <summary>
    /// 爆発処理（消去）
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
