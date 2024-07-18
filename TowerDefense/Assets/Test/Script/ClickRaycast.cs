using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRaycast : MonoBehaviour
{
    public Camera mainCamera;  // 使用するカメラ
    public LayerMask layerMask; // 特定のレイヤー


    public CardData m_cardData;


    public GameObject createObj;

    void Update()
    {
        ClickCast();
    }


    //ドラッグ＆ドロップで詠唱



    /// <summary>
    /// クリックで詠唱
    /// </summary>
    void ClickCast()
    {
        if (Input.GetMouseButtonDown(0)) // マウスの左クリックをチェック
        {
            // クリックしたスクリーン座標を取得
            Vector3 screenPosition = Input.mousePosition;

            // スクリーン座標をレイに変換
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);

            // レイキャストの結果を格納するためのRaycastHit
            RaycastHit hit;

            // 特定のレイヤーに対してレイキャストを行う
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // レイヤーに当たった場合の処理
                Debug.Log("Hit " + hit.collider.name + " at " + hit.point);

                Instantiate(createObj, hit.point, Quaternion.identity);
            }
            else
            {
                // レイヤーに当たらなかった場合の処理
                Debug.Log("No hit");
            }
        }
    }

}
