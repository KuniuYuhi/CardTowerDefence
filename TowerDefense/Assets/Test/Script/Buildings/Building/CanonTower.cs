using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : CanonType
{
    Transform m_childTRS;

    
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //拠点と同じ回転に設定
        transform.rotation = GetBaseObject().transform.rotation;

        //子オブジェクト(タワーの大砲の部分)を取得
        m_childTRS = transform.Find("Tower_Top");


    }

    private void FixedUpdate()
    {
        if (m_rangeObjectDetector.GetTargetGameObject() == null) return;

        //大砲の部分をターゲットのほうに回転させる
        RotationChild();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntarvalTimer();

        Attack();



    }

    void Attack()
    {
        //ターゲットのオブジェクトが存在し、攻撃可能なら
        if (m_rangeObjectDetector.GetTargetGameObject() != null&& IsAttackable() == true)
        {
            //ターゲットのオブジェクトに攻撃
            Shot(m_rangeObjectDetector.GetTargetGameObject());
        }
    }

    void RotationChild()
    {

        // ターゲットの方向を計算
        Vector3 direction = (m_rangeObjectDetector.GetTargetGameObject().transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // ゆっくりターゲットの方を向く
        m_childTRS.transform.rotation = Quaternion.Slerp(
            m_childTRS.transform.rotation, lookRotation, Time.deltaTime * 10f
            );
    }


}
