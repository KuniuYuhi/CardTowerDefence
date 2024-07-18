using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーションイベントの関数
/// </summary>
public class AnimationEventScript : MonoBehaviour
{
    [SerializeField,Header("アニメーションイベントを使うユニット(特定の処理で使う)")]
    UnitBase m_unit;

    bool m_isActive = false;

    bool m_isDie = false;

    
    /// <summary>
    /// ターゲットにダメージを与える(一体)
    /// </summary>
    public void ApplyTargetObjectDamage()
    {
        DamageProcessor.HitDamage(
            m_unit.GetCharacterController().GetTargetObject(), 
            m_unit.GetCurrentAttackPower()
            );

        ////ダメージコンポーネントを取得
        //IDamageable damageable = 
        //    m_unit.GetCharacterController().
        //    GetTargetObject().GetComponent<IDamageable>();
        ////コンポーネントがなければ処理しない
        //if (damageable == null) return;
        ////相手のダメージ関数を呼び出す
        //damageable.Damage(m_unit.GetRuntimeCharacterStatus().GetAtk());
    }

    /// <summary>
    /// 行動中かのフラグを取得
    /// </summary>
    /// <returns></returns>
    public bool GetIsActive()
    {
        return m_isActive;
    }

    /// <summary>
    /// 行動開始
    /// </summary>
    public void ActiveStart()
    {
        m_isActive = true;
    }

    /// <summary>
    /// 行動終了
    /// </summary>
    public void ActiveEnd()
    {
        m_isActive = false;
    }

    public void DieObject()
    {
        m_isDie = true;
    }

    public bool IsDie()
    {
        return m_isDie;
    }

}
