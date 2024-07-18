using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonType : BuildingBase
{
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
    /// 直接撃つ
    /// </summary>
    /// <param name="targetObject">撃つ対象のオブジェクト</param>
    protected void Shot(GameObject targetObject)
    {
        //ダメージ処理用インターフェースをターゲットから取得
        IDamageable damageable = targetObject.GetComponent<IDamageable>();
        //ダメージ処理用インターフェースを持っていなかったら
        if (damageable==null)
        {
            //撃てない
            return;
        }
        //ダメージを与える
        damageable.Damage(GetRuntimeStatus().GetEffectPower());

        //攻撃したので、インターバルが終わるまでは攻撃できないようにする
        AfterShot();

    }

}
