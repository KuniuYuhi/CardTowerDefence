using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewDetector : MonoBehaviour
{
    [SerializeField,Header("検出したいレイヤー")]
    LayerMask m_detectionLayer;

    float m_detectionRange;         //索敵範囲
    float m_fieldOfViewAngle;       //視野角

    GameObject m_targetObject;      //見つけたオブジェクト

    [SerializeField]
    float detectionInterval = 1.0f;

    float m_timer = 0.0f;


    /// <summary>
    /// ターゲットのオブジェクトを返す
    /// </summary>
    /// <returns></returns>
    public GameObject GetTargetGameObject()
    {
        return m_targetObject;
    }

    public void SetTargetGameObject(GameObject targetObject)
    {
        m_targetObject = targetObject;
    }

    /// <summary>
    /// 検出したいレイヤーを取得
    /// </summary>
    /// <returns></returns>
    public LayerMask GetDetectionLayer()
    {
        return m_detectionLayer;
    }


    // Start is called before the first frame update
    void Start()
    {
        m_targetObject = null;

        UnitBase pawnBase = GetComponent<UnitBase>();

        m_detectionRange = pawnBase.GetRuntimeStatus().GetSearchRange();

        m_fieldOfViewAngle = pawnBase.GetRuntimeStatus().GetFieldOfViewAngle();


        m_timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //狙いのオブジェクトがなくならないうちはオブジェクトを探さない
        if(m_targetObject!=null)
        {
            //todo より近い敵がいないかぎり探さない

            return;
        }

        //インターバルに達したらオブジェクトを探す
        if(IsFindIntarval())
        {
            FindHitObjectInFieldOfView(m_detectionRange, m_fieldOfViewAngle);
        }

    }


    bool IsFindIntarval()
    {
        if(m_timer>= detectionInterval)
        {
            m_timer = 0.0f;
            return true;
        }

        m_timer += Time.deltaTime;
        return false;
    }

    GameObject FindHitObjectInFieldOfView(float range, float fOVAngle)
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, range, m_detectionLayer
            );

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        //当たったコライダーの数繰り返す
        foreach (var hitCollider in hitColliders)
        {
            //レイを飛ばして衝突点を調べる


            //コライダーから自身の座標までのベクトルを求める
            Vector3 directionToTarget = hitCollider.transform.position - transform.position;
            //前方向からベクトルに向かう角度を求める
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            //求めた角度が設定した視野角内なら
            if (angleToTarget <= fOVAngle / 2)
            {
                //ヒットしたコライダー同士を比べて自身から一番近いコライダーを探す
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    //距離の更新
                    closestDistance = distance;
                    //コライダーからオブジェクト情報を取得
                    closestObject = hitCollider.gameObject;
                }
            }
        }

        //狙いのオブジェクトに設定
        m_targetObject = closestObject;

        return closestObject;

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -m_fieldOfViewAngle / 2, 0) * transform.forward * m_detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, m_fieldOfViewAngle / 2, 0) * transform.forward * m_detectionRange;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
