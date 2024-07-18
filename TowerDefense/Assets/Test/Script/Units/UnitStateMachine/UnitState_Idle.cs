using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : IUnitState
{

    UnitBase m_unit;

    //状態の設定
    public En_UnitState m_unitState => En_UnitState.enUnitState_Idle;


    public UnitState_Idle(UnitBase unit) => m_unit = unit;


    //状態開始時最初に呼ばれる
    public void Entry()
    {
        m_unit.GetCharacterController().SetmWalkAnimationBlendSpeed(0.0f);
    }

    //毎フレーム呼ばれる更新処理
    public void Update()
    {
        m_unit.CommonStateTransition();

    }

    /// <summary>
    /// 一定のフレームで呼ばれる更新処理（移動処理などをここで行う）
    /// </summary>
    public void FixedUpdate()
    {
        ////ターゲットオブジェクトがいないなら強制的に歩き状態に遷移。（敵の拠点に向かう）
        //if (m_unit.GetCharacterController().GetTargetObject() == null)
        //{
        //    m_unit.MoveTo();
        //}
        ////ターゲットがいて自分から離れていく場合
        //else
        //{
        //    //攻撃可能距離から離れたら追いかける
        //    m_unit.MoveTo();

        //}
        m_unit.MoveTo();
        m_unit.Rotation();
    }

    //状態終了時に呼ばれる
    public void Exit()
    {

    }

}
