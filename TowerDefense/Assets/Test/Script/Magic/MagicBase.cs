using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : SpawnableBase
{
    [SerializeField, Header("�X�e�[�^�X")]
    Status_Magic m_originalStatus;

    Status_Magic m_runtimeStatus;



    public Status_Magic GetRuntimeStatus()
    {
        return m_runtimeStatus;
    }


    protected virtual void Awake()
    {
        //�I���W�i���̃f�[�^����Q�[�����Ɏg���f�[�^�ɃR�s�[
        m_runtimeStatus = Instantiate(m_originalStatus);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    
}
