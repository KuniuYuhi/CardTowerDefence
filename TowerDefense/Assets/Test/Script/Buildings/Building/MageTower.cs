using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : AreaEffectType
{

    [SerializeField,Header("ダメージを与えられるオブジェクトのレイヤー")]
    LayerMask m_hitLayer;

    BuildingAtkObject m_buildingAtkObject;


    Transform m_crystalTRS;

    [SerializeField]
    float m_rotateSpeed;

    float m_defaultRotateSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // 子オブジェクト(クリスタル)を取得
        m_crystalTRS = transform.Find("Crystal");

        //攻撃用オブジェクトコンポーネントを取得
        m_buildingAtkObject = GetComponent<BuildingAtkObject>();
        //当たり判定でダメージをあたえられないようにする
        m_buildingAtkObject.SetIsDamageable(false);

        m_defaultRotateSpeed = m_rotateSpeed;
    }
    private void FixedUpdate()
    {
        RotationCrystal();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateIntarvalTimer();

        Attack();
    }

    void Attack()
    {
        //攻撃可能になったら
        if(IsAttackable())
        {
            //範囲内の敵にダメージを与える
            m_buildingAtkObject.ApplyAreaDamage(
                m_hitLayer, 
                GetRuntimeStatus().GetRadius()
                );

            m_rotateSpeed *= m_rotateSpeed*2.0f;

            //攻撃後の処理（タイマー、フラグリセット）
            AfterShot();
        }
    }


    void RotationCrystal()
    {
        if (m_rotateSpeed > m_defaultRotateSpeed)
        {
            m_rotateSpeed -= m_rotateSpeed * Time.deltaTime*2.0f;
        }
        else m_rotateSpeed = m_defaultRotateSpeed;

        m_crystalTRS.Rotate(0.0f, 0.0f, m_rotateSpeed * Time.deltaTime);
    }


}
