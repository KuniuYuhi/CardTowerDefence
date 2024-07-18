using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineBlendListCamera blendListCamera;

    [SerializeField]
    string m_inGameCameraName;

    bool m_changeSceneFlag = false;






    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        aaaa();

    }

    
    void aaaa()
    {
        if (m_changeSceneFlag|| blendListCamera.LiveChild==null) return;

        //現在のカメラが設定した名前と同じなら
        if (blendListCamera.LiveChild.Name == m_inGameCameraName)
        {
            //ブレンド中でないなら
            if (!blendListCamera.IsBlending)
            {
                //カメラがステージのセンターに移ったので
                //ゲームスタートパネルを表示させる
                InGameUiManager.Instance.ActiveGameStartPanel();


                //カメラが完全に切り替わったのでフラグを立てる
                m_changeSceneFlag = true;
                //ゲームスタートのUIを表示させる

            }
        }


        //シーン切り替えフラグを立てる
        //if (m_changeSceneFlag)
        //{
        //    m_changeSceneFlag = false;
        //    GameManager.Instance.SetChangeSceneFlag(true);
        //}
    }


}
