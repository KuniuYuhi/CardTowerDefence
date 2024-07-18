using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃用オブジェクトのアニメーションイベント
/// </summary>
public class AtkObjAnimEventScript : MonoBehaviour
{
    
    [SerializeField,Header("有効化無効化する対象(一つ)")]
    Collider m_collider;


    [SerializeField, Header("有効化無効化する対象(複数)")]
    Collider[] m_colliders;

    // Start is called before the first frame update
    void Start()
    {
        if(m_collider) m_collider.enabled = false;

        foreach(var collider in m_colliders)
        {
            collider.enabled = false;
        }

    }

    /// <summary>
    /// コライダーを有効化
    /// </summary>
    public void EnableCollider()
    {
        m_collider.enabled = true;
    }
    /// <summary>
    /// コライダーを無効化
    /// </summary>
    public void DisableCollider()
    {
        m_collider.enabled = false;
    }


    /// <summary>
    /// リストのコライダーを有効化
    /// </summary>
    public void EnableColliders()
    {
        foreach(var collider in m_colliders)
        {
            collider.enabled = true;
        }
    }
    /// <summary>
    /// リストコライダーを無効化
    /// </summary>
    public void DisableColliders()
    {
        foreach (var collider in m_colliders)
        {
            collider.enabled = false;
        }
    }

    /// <summary>
    /// 指定された番号のコライダーの配列の要素を有効化
    /// </summary>
    public void EnableColliderElement(int element)
    {
        m_colliders[element].enabled = true;
    }
    /// <summary>
    /// 指定された番号のコライダーの配列の要素を無効化
    /// </summary>
    public void DisableColliderElement(int element)
    {
        m_colliders[element].enabled = false;
    }

}
