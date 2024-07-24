using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewDetector : MonoBehaviour
{
    [SerializeField,Header("検出したいレイヤー")]
    LayerMask m_detectionLayer;

    float m_detectionRange;         //索敵範囲

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

        UnitBase unitBase = GetComponent<UnitBase>();

        m_detectionRange = unitBase.GetRuntimeStatus().GetSearchRange();

        
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
            FindHitObjectInFieldOfView(m_detectionRange);
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

    GameObject FindHitObjectInFieldOfView(float range)
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
           
            //ヒットしたコライダー同士を比べて自身から一番近いコライダーを探す
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            //前の一番近いコライダーとの距離より短かったら
            if (distance < closestDistance)
            {
                //距離の更新
                closestDistance = distance;
                //コライダーからオブジェクト情報を取得
                closestObject = hitCollider.gameObject;
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
    }
}
