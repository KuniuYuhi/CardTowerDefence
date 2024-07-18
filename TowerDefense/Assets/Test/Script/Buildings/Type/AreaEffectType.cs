using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectType : BuildingBase
{
    [SerializeField, Header("発射する弾")]
    GameObject m_bullet;

    protected RangeObjectDetector m_rangeObjectDetector;



    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        m_rangeObjectDetector = GetComponent<RangeObjectDetector>();

    }
   
    /// <summary>
    /// 弾を撃つ(オブジェクトの弾)
    /// </summary>
    /// <param name="targetObject">ターゲットのオブジェクト</param>
    /// <param name="shotPointTRS">弾を発射するポイント</param>
    /// <param name="angle">発射角度</param>
    protected void Shot(GameObject targetObject,Transform shotPointTRS,float angle)
    {
        //大砲の弾を爆発させる座標
        Vector3 targetPos = targetObject.transform.position;

        //弾を生成
        GameObject bullet = Instantiate(
            m_bullet, shotPointTRS.position, shotPointTRS.rotation);
        //弾道コンポーネントを取得する
        ProjectileMovement bulletProjectible = bullet.GetComponent<ProjectileMovement>();

        //弾が向かうターゲットの座標と影響を与える範囲を設定
        bulletProjectible.Init(targetPos,GetRuntimeStatus().GetRadius(), angle);

        //攻撃したので、インターバルが終わるまでは攻撃できないようにする
        AfterShot();
    }


}
