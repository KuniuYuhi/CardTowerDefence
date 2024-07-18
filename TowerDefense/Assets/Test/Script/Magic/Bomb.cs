using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MagicBase
{
    [SerializeField, Header("攻撃力")]
    int m_attack;

    [SerializeField, Header("爆発範囲")]
    float m_range;

    [SerializeField, Header("タイムリミット")]
    int m_timeLimit;

    [SerializeField, Header("爆発が当たるレイヤー")]
    LayerMask m_hitLayers;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //コルーチンでカウントダウンの処理を始める
        StartCoroutine(CountDown());
    }

    /// <summary>
    /// カウントダウンの処理
    /// </summary>
    /// <returns></returns>
    IEnumerator  CountDown()
    {
        yield return new WaitForSeconds(m_timeLimit);

        //タイムリミットまで待ったので爆発させる
        Explosion();
    }

    /// <summary>
    /// 爆発処理
    /// </summary>
    void Explosion()
    {
        ApplyAreaDmage();

        Destroy(gameObject);
    }

    void ApplyAreaDmage()
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, m_range, m_hitLayers
            );

        foreach (var hitCollider in hitColliders)
        {
            DamageProcessor.HitDamage(hitCollider.gameObject, m_attack);
        }
    }

}
