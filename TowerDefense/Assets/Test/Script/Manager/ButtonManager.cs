using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField,Header("ゲーム開始ボタン")]
    Button m_gameStartButton;

    [SerializeField, Header("タイトルに戻るボタン")]
    Button m_goToTitleButton;

   

    // Start is called before the first frame update
    void Start()
    {
        SetButtonListener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetButtonListener()
    {
        //ゲーム開始ボタンの設定
        if(m_gameStartButton!=null)
        {
            m_gameStartButton.onClick.RemoveAllListeners();
            m_gameStartButton.onClick.AddListener(GameManager.Instance.ChangeGameStartSceneState);
        }
        //タイトルに戻るに戻るボタンの設定
        if(m_goToTitleButton!=null)
        {
            m_goToTitleButton.onClick.RemoveAllListeners();
            m_goToTitleButton.onClick.AddListener(GameManager.Instance.ChangeTitleSceneState);
        }

    }
}
