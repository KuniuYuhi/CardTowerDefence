using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultUI : MonoBehaviour
{
    [SerializeField, Header("勝利パネル")]
    GameObject m_victoryPanel;        // 勝利パネル（勝利時に表示するUI）
    [SerializeField, Header("敗北パネル")]
    GameObject m_defeatPanel;         // 敗北パネル（敗北時に表示するUI）

    [SerializeField, Header("タイトルに戻るボタン")]
    GameObject m_goToTitleButton;

    // Start is called before the first frame update
    void Start()
    {
        //各UIを非アクティブ化する
        m_victoryPanel.SetActive(false);
        m_defeatPanel.SetActive(false);
        m_goToTitleButton.SetActive(false);
    }

    /// <summary>
    /// 勝敗パネルを有効化する
    /// </summary>
    public void ActiveResultPanel()
    {
        //ゲームマネージャーから勝敗を教えてもらう
        switch (GameManager.Instance.GetOutcome())
        {
            //勝利
            case GameManager.EnOutcome.enOutcome_WIn:
                m_victoryPanel.SetActive(true);
                break;
            //敗北
            case GameManager.EnOutcome.enOutcome_Lose:
                m_defeatPanel.SetActive(true);
                break;

            case GameManager.EnOutcome.enOutcome_None:
                Debug.LogError("勝敗は決まっていない");
                break;
        }

        //タイトルに戻るボタンをアクティブ化
        m_goToTitleButton.SetActive(true);
    }


}
