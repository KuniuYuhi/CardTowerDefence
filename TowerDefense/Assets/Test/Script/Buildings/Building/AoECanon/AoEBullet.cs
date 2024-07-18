using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEBullet : MonoBehaviour
{
    SphereCollider m_collider;

    BuildingAtkObject m_buildingAtkObject;

    [SerializeField,Header("ぶつかったら爆発するレイヤー")]
    LayerMask m_groundLayer;

    [SerializeField, Header("ダメージを与えたいレイヤー")]
    LayerMask m_hitLayer;

    [SerializeField, Header("爆発範囲の半径")]
    float m_explosionRadius;

    bool isExpload = false;



    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<SphereCollider>();
        //攻撃用オブジェクトコンポーネントを取得
        m_buildingAtkObject = GetComponent<BuildingAtkObject>();
        //ダメージをあたえられないようにする
        m_buildingAtkObject.SetIsDamageable(false);
        
        m_collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isExpload)
        {
            Destroy(gameObject);
        }
    }

    void Explosion()
    {
        //ダメージを与えられるようにする
        m_buildingAtkObject.ApplyAreaDamage(m_hitLayer, m_explosionRadius);
        //コライダーの半径を変更
        m_collider.radius = m_explosionRadius;
        //爆発した
        isExpload = true;
        //エフェクト

    }




    private void OnTriggerEnter(Collider other)
    {
        //衝突したコライダーのレイヤーが特定のレイヤーなら
        if(((1<< other.gameObject.layer)& m_groundLayer) != 0)
        {
            //爆発させる
            Explosion();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }

}
