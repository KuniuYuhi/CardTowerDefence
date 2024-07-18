using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : SpawnableBase, IDamageable
{
    [SerializeField, Header("ステータス")]
    Status_Building m_originalStatus;

    Status_Building m_runtimeStatus;      //実行中に使うステータス


    float m_intarvalTimer = 0.0f;

    bool m_isAttackable = false;            //攻撃可能か

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
    /// 攻撃後の処理
    /// </summary>
    public void AfterShot()
    {
        //攻撃不能にする
        SetAttacable(false);
        //タイマーをリセット
        m_intarvalTimer = 0.0f;
    }

    protected virtual void Awake()
    {
        //オリジナルのデータからゲーム中に使うデータにコピー
        m_runtimeStatus = Instantiate(m_originalStatus);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
      
        //生成したらすぐに攻撃できる
        m_intarvalTimer = m_runtimeStatus.GetIntarval();
        //攻撃不可能
        m_isAttackable = false;

    }

    // Update is called once per frame
    void Update()
    {
        //タイマーを動かす
        UpdateIntarvalTimer();


    }

    protected void UpdateIntarvalTimer()
    {
        //攻撃可能ならタイマーを動かさない
        if (m_isAttackable) return;

        //インターバルに達したら
        if(m_intarvalTimer >= m_runtimeStatus.GetIntarval())
        {
            //攻撃可能
            m_isAttackable = true;

            Debug.Log("攻撃可能！！！");
            return;
        }
        //タイマーを加算
        m_intarvalTimer += Time.deltaTime;
    }

    
    



    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="hitDamage">受けるダメージ</param>
    public void Damage(int hitDamage)
    {
        m_runtimeStatus.ApplyDamage(hitDamage);

        Debug.Log(transform.name + "は" + hitDamage + "受けた" );

        if (m_runtimeStatus.GetEndurance()<=0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡時処理
    /// </summary>
    public void Die()
    {
        
        Debug.Log(transform.name + "は倒された");

        Destroy(gameObject);

    }
}
