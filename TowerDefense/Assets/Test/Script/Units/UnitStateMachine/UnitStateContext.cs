using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateContext
{
    IUnitState m_currentUnitState;      //現在の状態
    IUnitState m_previousUnitState;     //直前の状態

    Dictionary<En_UnitState, IUnitState> m_unitTable;   //駒の全状態を格納した辞書

    public IUnitState GetCurrentUnitState()
    {
        return m_currentUnitState;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="firstUnitState"></param>
    public void Init(UnitBase unit,En_UnitState firstUnitState)
    {
        if(m_unitTable!=null)
        {
            return;
        }

        Dictionary<En_UnitState, IUnitState> table = new()
        {
            { En_UnitState.enUnitState_Idle, new UnitState_Idle(unit) },
            { En_UnitState.enUnitState_Walk, new UnitState_Walk(unit) },
            { En_UnitState.enUnitState_normalAttack, new UnitState_NomalAttack(unit) },
            { En_UnitState.enUnitState_Die, new UnitState_Die(unit) },
        };
        //テーブルを設定
        m_unitTable = table;
        //現在のステートを設定
        m_currentUnitState = m_unitTable[firstUnitState];

        ChangeState(firstUnitState);
    }


    public void ChangeState(En_UnitState changeState)
    {
        if(m_unitTable == null || m_currentUnitState==null)
        {
            Debug.LogError("ステートを切り替えられません");
            return;
        }

        var newState = m_unitTable[changeState];

        //前のステートの抜け出す時の処理をする
        m_previousUnitState = m_currentUnitState;
        m_previousUnitState.Exit();
        //新しいステートの最初の処理をする
        m_currentUnitState = newState;
        m_currentUnitState.Entry();
    }


    public void Update() => m_currentUnitState?.Update();

    public void FixedUpdate() => m_currentUnitState?.FixedUpdate();



}
