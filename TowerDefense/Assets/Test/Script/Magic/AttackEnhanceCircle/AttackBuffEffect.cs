using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffEffect : MonoBehaviour
{
    float m_boostDuration = 0.0f;

    int m_buffAmount = 0;

    UnitBase m_unit;
   
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="buffAmount">付加攻撃力</param>
    /// <param name="boostDuration">効果持続時間</param>
    public void Init(UnitBase unit, int buffAmount,float boostDuration)
    {
        m_unit = unit;
        m_buffAmount = buffAmount;
        m_boostDuration = boostDuration;
    }

    // Start is called before the first frame update
    void Start()
    {
        //バフをかけるユニットの付加攻撃力に設定した攻撃力を代入
        m_unit.SetAddAttackPower(m_buffAmount);

        //効果持続時間を計って削除するコルーチンを実行
        StartCoroutine(CleanupOnDestroy());

    }

    IEnumerator CleanupOnDestroy()
    {
        //効果持続時間が終わるまでは待つ
        yield return new WaitForSeconds(m_boostDuration);

        //時間が終わったら、付加攻撃力をリセットして削除
        m_unit.SetAddAttackPower(0);


        Destroy(this);
    }

}
