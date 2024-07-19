using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntrusionUnitChecker : MonoBehaviour
{
    [SerializeField, Header("チェックしたいレイヤー")]
    LayerMask m_includeLayer;

    [SerializeField, Header("チェックしたくないレイヤー")]
    LayerMask m_excuteLayer;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider collision = GetComponent<BoxCollider>();

        collision.includeLayers = m_includeLayer;
        collision.excludeLayers = m_excuteLayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //チェックエリアに入ったら親オブジェクトのゲームオブジェクトを設定

        CharacterControll charaCon = other.GetComponent<CharacterControll>();

        if (charaCon == null) return;

        charaCon.SetTargetObject(transform.parent.gameObject);

    }


    private void OnTriggerExit(Collider other)
    {
        //チェックエリアを抜けたら、追いかけるオブジェクトをリセット

        CharacterControll charaCon = other.GetComponent<CharacterControll>();

        if (charaCon == null) return;

        charaCon.SetTargetObject(null);
    }

}
