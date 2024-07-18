using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeObjectDetector : MonoBehaviour
{
    [SerializeField, Header("検出したいレイヤー")]
    LayerMask m_detectionLayer;

    float m_detectionRange;         //索敵範囲

    [SerializeField,Header("探すオブジェクト")]
    BuildingBase m_buildingObject;  //探すオブジェクト

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


    // Start is called before the first frame update
    void Start()
    {
        m_targetObject = null;

        m_detectionRange = m_buildingObject.GetRuntimeStatus().GetRadius();

    }

    // Update is called once per frame
    void Update()
    {
        //狙いのオブジェクトがなくならないうちはオブジェクトを探さない
        //if (m_targetObject != null)
        //{
        //    return;
        //}

        //インターバルに達したらオブジェクトを探す
        if (IsFindIntarval())
        {
            FindHitObjectInRange(m_detectionRange);
        }
    }

    bool IsFindIntarval()
    {
        if (m_timer >= detectionInterval)
        {
            m_timer = 0.0f;
            return true;
        }

        m_timer += Time.deltaTime;
        return false;
    }

    /// <summary>
    /// 範囲内で一番近いオブジェクトを探す（1体）
    /// </summary>
    /// <param name="range">探す範囲</param>
    /// <returns></returns>
    GameObject FindHitObjectInRange(float range)
    {
        //範囲内でヒットした特定のレイヤーを持つコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position, range, m_detectionLayer
            );

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        //当たったコライダーの数分繰り返す
        foreach(var hitCollider in hitColliders)
        {
            //コライダーから自身の座標までの長さを求める
            float distance = Vector3.Distance(
                hitCollider.transform.position, transform.position
                );

            //距離が範囲内であれば
            if(distance<= range)
            {
                //計算した距離が一番近い距離より短いなら
                if(distance < closestDistance)
                {
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
    }

}
