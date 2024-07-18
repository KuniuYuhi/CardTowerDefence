using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer
{
    private float m_normalAttackIntarval;
    private float m_normalAttackTimer = 0.0f;

    private float m_skillAttackIntarval;
    private float m_skillAttackTimer = 0.0f;

    bool isInit = false;

    bool isnormalAttackable = false;
    bool isskillAttackable = false;

    /// <summary>
    /// 生成時に各種インターバルを設定
    /// </summary>
    /// <param name="normalAttackIntarval">通常攻撃のインターバル</param>
    /// <param name="skillAttackIntarval">スキル攻撃のインターバル</param>
    public AttackTimer(float normalAttackIntarval,float skillAttackIntarval)
    {
        this.m_normalAttackIntarval = normalAttackIntarval;
        this.m_skillAttackIntarval = skillAttackIntarval;

        this.m_normalAttackTimer = normalAttackIntarval;
        this.m_skillAttackTimer = skillAttackIntarval;

        isInit = true;
    }


    public void SetNormalAttackIntarval(float value)
    {
        m_normalAttackIntarval = value;
    }

    public void SetSetAttackIntarval(float value)
    {
        m_skillAttackIntarval = value;
    }


    public bool IsNormalAttackable()
    {
        return isnormalAttackable;
    }

    public bool IsSkillAttackable()
    {
        return isskillAttackable;
    }

    public float GetNormalAttackTimer()
    {
        return m_normalAttackTimer;
    }

    public float GetSkillAttackTimer()
    {
        return m_skillAttackTimer;
    }



    public void Update()
    {
        //通常攻撃が可能でないなら
        if(!isnormalAttackable)
        {
            UpdateNormalAttackTimer();
        }
        //スキル攻撃が可能でないなら
        if(!isskillAttackable)
        {
            UpdateSkillAttackTimer();
        }

    }


    void UpdateNormalAttackTimer()
    {
        //タイマーがインターバルを超えたら
        if (m_normalAttackTimer >= m_normalAttackIntarval)
        {
            //攻撃可能
            isnormalAttackable = true;
            return;
        }

        //タイマーを加算
        m_normalAttackTimer += Time.deltaTime;
    }

    void UpdateSkillAttackTimer()
    {
        //タイマーがインターバルを超えたら
        if (m_skillAttackTimer >= m_skillAttackIntarval)
        {
            //攻撃可能
            isskillAttackable = true;
            return;
        }

        //タイマーを加算
        m_skillAttackTimer += Time.deltaTime;

    }

    


    public void ResetNormalAttackTimer()
    {
        m_normalAttackTimer = 0.0f;
        isnormalAttackable = false;
    }

    public void ResetSkillAttackTimer()
    {
        m_skillAttackTimer = 0.0f;
        isskillAttackable = false;
    }

}
