using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : SpawnableBase
{
    [SerializeField, Header("ステータス")]
    Status_Magic m_originalStatus;

    Status_Magic m_runtimeStatus;



    public Status_Magic GetRuntimeStatus()
    {
        return m_runtimeStatus;
    }


    protected virtual void Awake()
    {
        //オリジナルのデータからゲーム中に使うデータにコピー
        m_runtimeStatus = Instantiate(m_originalStatus);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    
}
