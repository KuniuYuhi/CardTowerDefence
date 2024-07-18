using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBase : MonoBehaviour
{
    [SerializeField, Header("自分の所属する拠点")]
    GameObject m_myBaseObject;

    /// <summary>
    /// 自身の拠点オブジェクトを設定
    /// </summary>
    /// <returns></returns>
    public void SetBaseObject(GameObject baseObject)
    {
        m_myBaseObject = baseObject;
    }
    /// <summary>
    /// 自身の拠点オブジェクトを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetBaseObject()
    {
        return m_myBaseObject;
    }
}
