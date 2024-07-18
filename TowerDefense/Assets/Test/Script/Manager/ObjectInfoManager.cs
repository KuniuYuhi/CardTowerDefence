using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoManager : MonoBehaviour
{
    [SerializeField,Header("味方の拠点")]
    Base m_alliedBase;

    [SerializeField,Header("敵の拠点")]
    Base m_enemyBase;

    GameManager m_gameManager;      //ゲームマネージャー


    public Base GetAlliedBase()
    {
        return m_alliedBase;
    }

    public Base GetEnemyBase()
    {
        return m_enemyBase;
    }



    // Start is called before the first frame update
    void Start()
    {
        //ゲームマネージャーの情報を取得
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    void determineOutcome()
    {
        //味方の拠点が壊れたら
        if (m_alliedBase.IsDestroyed())
        {
            //ゲームオーバー

            return;
        }

        if (m_enemyBase.IsDestroyed())
        {
            //ゲームクリア

            return;
        }
    }

}
