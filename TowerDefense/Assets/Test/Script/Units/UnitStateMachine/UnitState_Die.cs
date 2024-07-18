using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Die : IUnitState
{
    UnitBase m_unit;

    //状態の設定
    public En_UnitState m_unitState => En_UnitState.enUnitState_Die;


    public UnitState_Die(UnitBase unit) => m_unit = unit;


    //状態開始時最初に呼ばれる
    public void Entry()
    {

    }

    //毎フレーム呼ばれる更新処理
    public void Update()
    {
        //死亡アニメーションが終わったら
        if (m_unit.GetAnimationEventScript().IsDie())
        {
            //削除処理
            m_unit.ProcessDie();
        }
    }

    /// <summary>
    /// 一定のフレームで呼ばれる更新処理（移動処理などをここで行う）
    /// </summary>
    public void FixedUpdate()
    {

    }

    //状態終了時に呼ばれる
    public void Exit()
    {

    }
}
