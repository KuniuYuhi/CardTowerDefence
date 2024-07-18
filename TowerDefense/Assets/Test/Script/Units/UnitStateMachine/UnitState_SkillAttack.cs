using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_SkillAttack : IUnitState
{
    UnitBase m_unit;


  
    //状態の設定
    public En_UnitState m_unitState => En_UnitState.enUnitState_SkillAttack;


    public UnitState_SkillAttack(UnitBase unit) => m_unit = unit;


    //状態開始時最初に呼ばれる
    public void Entry()
    {
        
    }

    //毎フレーム呼ばれる更新処理
    public void Update()
    {
        

        if(!m_unit.GetAnimationEventScript().GetIsActive())
        {
            m_unit.CommonStateTransition();
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
        m_unit.GetAttackTimer().ResetSkillAttackTimer();
        //攻撃が終わったのでフラグを下げる
        m_unit.SetIsAttackActive(false);
    }
}
