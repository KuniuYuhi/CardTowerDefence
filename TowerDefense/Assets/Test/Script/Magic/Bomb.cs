using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MagicBase
{
    [SerializeField, Header("爆発が当たるレイヤー")]
    LayerMask m_hitLayers;


    int m_attackPower;
    float m_radius;
    float m_countDown;

    protected override void Awake()
    {
        base.Awake();
        //ステータスから攻撃力と爆発範囲の半径とカウントダウンを取得
        m_attackPower = (int)GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("攻撃力");
        m_radius = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("半径");
        m_countDown = GetRuntimeStatus().GetKeyValuePairs().GetValueOrDefault("カウントダウン");
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
        yield return new WaitForSeconds(m_countDown);

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
