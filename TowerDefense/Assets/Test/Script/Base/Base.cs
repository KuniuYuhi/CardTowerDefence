using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour,IDamageable
{
    [SerializeField, Header("ステータス")]
    Status_Base m_originalBaseStatus;

    Status_Base m_runtimeBaseStatus;      //実行中に使うステータス


    bool m_isDestroyed = false;


    public Status_Base GetStatus()
    {
        return m_runtimeBaseStatus;
    }

    /// <summary>
    /// 破壊されたか
    /// </summary>
    /// <returns></returns>
    public bool IsDestroyed()
    {
        return m_isDestroyed;
    }



    // Start is called before the first frame update
    void Start()
    {
        //オリジナルのデータからゲーム中に使うデータにコピー
        m_runtimeBaseStatus = Instantiate(m_originalBaseStatus);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="hitDamage">受けるダメージ</param>
    public void Damage(int hitDamage)
    {
        m_runtimeBaseStatus.ApplyDamage(hitDamage);

        if(m_runtimeBaseStatus.GetEndurance()<=0)
        {
            Die();
        }

        Debug.Log(transform.name + "痛い"+ m_runtimeBaseStatus.GetEndurance());
    }

    /// <summary>
    /// 死亡時処理
    /// </summary>
    public void Die()
    {
        Debug.Log(transform.name + "は崩壊した");
        m_isDestroyed = true;
    }
}
