using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField, Header("ゲームスタートパネル")]
    GameObject m_gameStartPanel;        //ゲームスタートパネル

    [SerializeField,Header("UIを表示させる時間")]
    float m_viewTimer = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        //各UIを非アクティブ化する
        m_gameStartPanel.SetActive(false);

        
    }

   /// <summary>
   /// ゲームスタートパネルを表示する
   /// </summary>
    public void ActiveGameStartPanel()
    {

        DeckOpen();

        //各UIをアクティブ化する
        //m_gameStartPanel.SetActive(true);

        //コルーチン
        StartCoroutine(PanelViewTimer());
    }

    IEnumerator PanelViewTimer()
    {
        while (true)
        {
            if (DeckManager.instance.GetSetUpHandFlag())
            {
                break;
            }

            //ある程度待つ
            yield return new WaitForSeconds(1.0f);
        }

        //ある程度待つ
        yield return new WaitForSeconds(1.0f);

        //各UIをアクティブ化する
        m_gameStartPanel.SetActive(true);

        //ある程度待つ
        yield return new WaitForSeconds(m_viewTimer);

        //ゲームマネージャーにgameシーンを切り替えてもいい合図を送る
        GameManager.Instance.SetChangeSceneFlag(true);

        //手札のカードを動かせるようにする
        DeckManager.instance.SetHandCardsMovableFlag(true);

        //パネルを非アクティブ化する
        m_gameStartPanel.SetActive(false);
    }


    void DeckOpen()
    {
        //デッキを画面にセット
        DeckManager.instance.StartDeckAndCardSetting();
    }

}
