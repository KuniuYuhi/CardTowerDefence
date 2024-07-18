using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U���p�I�u�W�F�N�g�̃A�j���[�V�����C�x���g
/// </summary>
public class AtkObjAnimEventScript : MonoBehaviour
{
    
    [SerializeField,Header("�L��������������Ώ�(���)")]
    Collider m_collider;


    [SerializeField, Header("�L��������������Ώ�(����)")]
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
    /// �R���C�_�[��L����
    /// </summary>
    public void EnableCollider()
    {
        m_collider.enabled = true;
    }
    /// <summary>
    /// �R���C�_�[�𖳌���
    /// </summary>
    public void DisableCollider()
    {
        m_collider.enabled = false;
    }


    /// <summary>
    /// ���X�g�̃R���C�_�[��L����
    /// </summary>
    public void EnableColliders()
    {
        foreach(var collider in m_colliders)
        {
            collider.enabled = true;
        }
    }
    /// <summary>
    /// ���X�g�R���C�_�[�𖳌���
    /// </summary>
    public void DisableColliders()
    {
        foreach (var collider in m_colliders)
        {
            collider.enabled = false;
        }
    }

    /// <summary>
    /// �w�肳�ꂽ�ԍ��̃R���C�_�[�̔z��̗v�f��L����
    /// </summary>
    public void EnableColliderElement(int element)
    {
        m_colliders[element].enabled = true;
    }
    /// <summary>
    /// �w�肳�ꂽ�ԍ��̃R���C�_�[�̔z��̗v�f�𖳌���
    /// </summary>
    public void DisableColliderElement(int element)
    {
        m_colliders[element].enabled = false;
    }

}
