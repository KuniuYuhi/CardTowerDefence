using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultManager : MonoBehaviour
{
    [SerializeField,Header("ゲームの情報を管理するマネージャー")]
    ObjectInfoManager m_objectInfoManager;

    bool m_isBattleOver = false;

    // Update is called once per frame
    void Update()
    {
        //勝敗が決まって、UIを表示したら、この先処理しない
        if (m_isBattleOver) return;

        CheckBattleOver();

    }






    void CheckBattleOver()
    {
        //勝敗は決まったら
        if (CheckGameOutcome())
        {
            //UIの勝敗パネルを表示させる
            InGameUiManager.Instance.ActiveResultPanel();

            //バトルは終わった
            m_isBattleOver = true;
            return;
        }

        //バトルは終わっていない
    }

    bool CheckGameOutcome()
    {
        //味方の拠点が壊されたら
        if (m_objectInfoManager.GetAlliedBase().IsDestroyed())
        {
            //勝敗を敗北に設定する
            GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_Lose);
            return true;
        }
        //敵の拠点が壊れたら
        else if (m_objectInfoManager.GetEnemyBase().IsDestroyed())
        {
            //勝敗を勝利に設定する
            GameManager.Instance.SetOutcome(GameManager.EnOutcome.enOutcome_WIn);
            return true;
        }

        //勝敗は決まっていない
        return false;
    }

    

}
