using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 駒クラスのステートの基底クラス
/// </summary>
public interface IUnitState
{
   
    En_UnitState m_unitState { get; }

    public En_UnitState GetUnitState()
    {
        return m_unitState;
    }

    //状態開始時最初に呼ばれる
    void Entry();

    //毎フレーム呼ばれる更新処理
    void Update();

    /// <summary>
    /// 一定のフレームで呼ばれる更新処理（移動処理などをここで行う）
    /// </summary>
    void FixedUpdate();

    //状態終了時に呼ばれる
    void Exit();


}

/// <summary>
/// 駒のステート
/// </summary>
public enum En_UnitState
{
    enUnitState_Idle,                   //待機状態
    enUnitState_Walk,                   //歩き状態
    enUnitState_normalAttack,           //通常攻撃状態
    enUnitState_Die,                    //死亡状態
}