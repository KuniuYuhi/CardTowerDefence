using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControll : MonoBehaviour
{
    FieldOfViewDetector m_fieldOfViewDetector;

    NavMeshAgent m_navMeshAgent;

    GameObject m_targetObject;

    float m_stoppingDistance;

    Vector3 m_defaultDirection;


    float m_attackDistance;


    float m_walkAnimationBlendSpeed = 0.0f;

    bool m_isAttackable = false;


    bool m_isMove = false;


    public bool GetIsAttackable()
    {
        return m_isAttackable;
    }

    public void SetIsAttackable(bool flag)
    {
        m_isAttackable = flag;
    }


    public void SetTargetObject(GameObject targetObject)
    {
        m_fieldOfViewDetector.SetTargetGameObject(targetObject);
    }

    public GameObject GetTargetObject()
    {
        return m_targetObject;
    }

    public float GetWalkAnimationBlendSpeed()
    {
        return m_walkAnimationBlendSpeed;
    }

    public void SetmWalkAnimationBlendSpeed(float value)
    {
        m_walkAnimationBlendSpeed = value;
    }

    public bool GetIsMove()
    {
        return m_isMove;
    }

    public FieldOfViewDetector GetFieldOfViewDetector()
    {
        return m_fieldOfViewDetector;
    }


    private void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_fieldOfViewDetector = GetComponent<FieldOfViewDetector>();

        UnitBase pawnObject = GetComponent<UnitBase>();
        //攻撃距離をステータスから取得
        m_attackDistance = pawnObject.GetRuntimeStatus().GetAttackRange();
        //基本の方向を取得
        m_defaultDirection = pawnObject.GetBaseObject().transform.forward;
        //停止距離をステータスから取得
        m_stoppingDistance = pawnObject.GetRuntimeStatus().GetStopDistance();
        //ナビメッシュエージェントのスピードをユニットのスピードに変更する
        m_navMeshAgent.speed = pawnObject.GetRuntimeStatus().GetSpeed();
    }



    public void MoveTo(float speed)
    {
        //索敵範囲内で見つかったオブジェクトを取得
        m_targetObject = m_fieldOfViewDetector.GetTargetGameObject();

        //自身の索敵範囲内に特定のオブジェクトがあったら
        if (m_targetObject != null)
        {
            if(m_navMeshAgent.isStopped==false)
            {
                m_navMeshAgent.isStopped = true;
            }

            //ターゲットに向かって移動
            MoveToTarget(speed, m_targetObject.transform.position);
        }
        else
        {
            if (m_navMeshAgent.isStopped == true)
            {
                m_navMeshAgent.isStopped = false;
            }

            //そうでなければ相手の拠点に向かって直進
            MoveStraight(speed);
        }

    }

    public void MoveStraight(float speed)
    {
        //一定の距離になったら強制的に敵の拠点に向かうようにする


        //Vector3 forwad = m_defaultDirection *  10.0f;

        Vector3 direction = m_defaultDirection;

        direction *= speed * Time.deltaTime;

        transform.position += direction;

        //m_navMeshAgent.SetDestination(forwad);

        //アニメーションブレンドで使う
        m_walkAnimationBlendSpeed = 1f;

        m_isMove = true;
    }

    public void MoveToTarget(float speed,Vector3 target)
    {

        Vector3 direction = target - transform.position;

        //距離が遠いほど値が大きく、近づくと小さく


        //ベクトルの長さを取得
        float directionToTarget = direction.magnitude;
        float moveStopDistance = CalcStoppingDistance(m_stoppingDistance);

       

        //ベクトルの距離が停止したい距離以下なら移動しない
        if (directionToTarget <= moveStopDistance)
        {
            //移動しない
            m_isMove = false;
            m_walkAnimationBlendSpeed = 0.0f;
            return;
        }

        float a = directionToTarget - moveStopDistance;
        //アニメーションブレンドで使う
        m_walkAnimationBlendSpeed = Mathf.Clamp01(a);
        m_isMove = true;

        //正規化
        direction.Normalize();
        //移動
        direction *= speed * Time.deltaTime;

        //現在の座標に移動ベクトルを加算
        transform.position += direction;

        
    }


    public bool IsAttackableTarget()
    {
        //索敵範囲内で見つかったオブジェクトを取得
        m_targetObject = m_fieldOfViewDetector.GetTargetGameObject();

        //オブジェクトが索敵範囲内にいなかったら攻撃できる対象なし
        if (m_targetObject == null) return false;

        //ターゲットオブジェクトから自身に向かうベクトルを計算
        Vector3 direction = m_targetObject.transform.position - transform.position;

        //ベクトルの長さを取得
        float directionToTarget = direction.magnitude;
        float attackStopDistance = CalcStoppingDistance(m_attackDistance);

        //ベクトルが攻撃範囲内なら
        if(directionToTarget <= attackStopDistance)
        {
            //攻撃可能
            return true;
        }
        //攻撃不可能
        return false;
    }


    float CalcStoppingDistance(float stopDistance)
    {
        //ターゲットオブジェクトと自身のコライダーを取得
        Collider targetCollider = m_targetObject.GetComponent<Collider>();
        Collider myCollider = GetComponent<Collider>();

        //各コライダーの半径を取得
        float targetRadius = targetCollider != null ? targetCollider.bounds.extents.magnitude : 1f;
        float myRadius = myCollider != null ? myCollider.bounds.extents.magnitude : 1f;

        //コライダーの半径と停止したい距離を足して結果的に止まりたい距離を計算
        float totalStoppingDistance = (targetRadius/2.0f) + (myRadius/2.0f) + stopDistance;

        return totalStoppingDistance;

    }


    public void Rotation()
    {
        if(m_targetObject!=null)
        {
            RotationFromTaget();
        }
        else
        {
            RotationFromForward();
        }

    }

    void RotationFromTaget()
    {
        // エージェントが目的地に到達したかを確認
        if (!m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            // ターゲットの方向を計算
            Vector3 direction = (m_targetObject.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            //ターゲットのほうに回転
            transform.rotation = lookRotation;

            // エージェントの速度がゼロであることを確認
            if (m_navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // ここでエージェントが完全に停止していることを確認するための処理を追加できます
                // 必要に応じて他のアクションを実行することも可能です
            }
        }
    }

    void RotationFromForward()
    {
        Quaternion lookRotation = Quaternion.LookRotation(m_defaultDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

}
