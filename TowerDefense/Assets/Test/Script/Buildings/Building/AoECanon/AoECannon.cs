using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoECannon : AreaEffectType
{
    [SerializeField, Header("弾を発射するポイント")]
    Transform m_shotPoint;

    [SerializeField, Range(0F, 90F),Header("弾を発射する角度")]
    float m_throwingAngle;

    Transform m_childRotaterTRS;


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        transform.rotation = GetBaseObject().transform.rotation;

        // 子オブジェクト(タワーの大砲の部分)を取得
        m_childRotaterTRS = transform.Find("rotater");
    }

    private void FixedUpdate()
    {
        if (m_rangeObjectDetector.GetTargetGameObject() == null) return;

        //大砲の部分をターゲットのほうに回転させる
        RotationChildRotater();
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
        if (m_rangeObjectDetector.GetTargetGameObject() != null && IsAttackable() == true)
        {
            //ターゲットのオブジェクトに攻撃
            Shot(m_rangeObjectDetector.GetTargetGameObject(), m_shotPoint, m_throwingAngle);
        }
    }

    void RotationChildRotater()
    {

        // ターゲットの方向を計算
        Vector3 direction = (m_rangeObjectDetector.GetTargetGameObject().transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // ゆっくりターゲットの方を向く
        m_childRotaterTRS.transform.rotation = Quaternion.Slerp(
            m_childRotaterTRS.transform.rotation, lookRotation, Time.deltaTime * 10f
            );
    }

}
