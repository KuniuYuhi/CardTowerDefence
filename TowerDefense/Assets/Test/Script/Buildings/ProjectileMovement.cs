using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 弾道の移動処理（爆発も）
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    Rigidbody m_rigidbody;

    float m_range;   //影響を与える範囲

    Vector3 m_targetPostion = Vector3.zero;

    Vector3 m_direction = Vector3.zero;

    float m_throwingAngle;

    float m_speed = 5.0f;

    bool isInit = false;




    /// <summary>
    /// ターゲットの座標を設定
    /// </summary>
    /// <param name="position"></param>
    public void SetTargetPosition(Vector3 position)
    {
        m_targetPostion = position;
    }

    /// <summary>
    /// 範囲を設定
    /// </summary>
    /// <param name="range">範囲</param>
    public void SetRange(float range)
    {
        m_range = range;
    }

    
    /// <summary>
    /// 生成時の初期化処理
    /// </summary>
    /// <param name="position">ターゲットの座標</param>
    /// <param name="range">影響を与える範囲</param>
    /// <param name="angle">角度</param>
    public void Init(Vector3 position, float range,float angle)
    {
        m_targetPostion = position;
        m_range = range;
        m_throwingAngle = angle;
        SettingMoveDirection();

        isInit = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        if (!isInit) SettingMoveDirection();


        // 射出速度を算出
        Vector3 velocity = CalculateVelocity(
            transform.position, m_targetPostion, m_throwingAngle
            );

        m_rigidbody.AddForce(velocity * m_rigidbody.mass, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SettingMoveDirection()
    {
        //自身からターゲットに向かうベクトルを計算
        m_direction = m_targetPostion - transform.position;
        m_direction.Normalize();

        m_direction *= m_speed;
    }


    /// <summary>
    /// 速度の計算
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3 CalculateVelocity(Vector3 start, Vector3 end,float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(
            new Vector2(start.x, end.z), new Vector2(end.x, start.z)
            );

        // 垂直方向の距離y
        float y = start.y - end.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(
            -Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y))
            );



        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(
                end.x - start.x, 
                x * Mathf.Tan(rad), 
                end.z - start.z).normalized * speed
                );
        }
    }


}
