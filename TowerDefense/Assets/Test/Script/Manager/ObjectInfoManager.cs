using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoManager : MonoBehaviour
{
    [SerializeField,Header("�����̋��_")]
    Base m_alliedBase;

    [SerializeField,Header("�G�̋��_")]
    Base m_enemyBase;

    GameManager m_gameManager;      //�Q�[���}�l�[�W���[


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
        //�Q�[���}�l�[�W���[�̏����擾
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    void determineOutcome()
    {
        //�����̋��_����ꂽ��
        if (m_alliedBase.IsDestroyed())
        {
            //�Q�[���I�[�o�[

            return;
        }

        if (m_enemyBase.IsDestroyed())
        {
            //�Q�[���N���A

            return;
        }
    }

}
