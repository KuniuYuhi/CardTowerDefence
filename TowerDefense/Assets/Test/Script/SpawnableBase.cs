using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBase : MonoBehaviour
{
    [SerializeField, Header("�����̏������鋒�_")]
    GameObject m_myBaseObject;

    /// <summary>
    /// ���g�̋��_�I�u�W�F�N�g��ݒ�
    /// </summary>
    /// <returns></returns>
    public void SetBaseObject(GameObject baseObject)
    {
        m_myBaseObject = baseObject;
    }
    /// <summary>
    /// ���g�̋��_�I�u�W�F�N�g���擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetBaseObject()
    {
        return m_myBaseObject;
    }
}
