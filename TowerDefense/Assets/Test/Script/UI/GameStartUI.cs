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
        //各UIをアクティブ化する
        m_gameStartPanel.SetActive(true);
        //コルーチン
        StartCoroutine(PanelViewTimer());
    }

    IEnumerator PanelViewTimer()
    {
        yield return new WaitForSeconds(m_viewTimer);

        //ゲームマネージャーにシーンを切り替えてもいい合図を送る
        GameManager.Instance.SetChangeSceneFlag(true);

        //パネルを非アクティブ化する
        m_gameStartPanel.SetActive(false);
    }

}
