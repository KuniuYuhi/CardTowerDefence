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
    /// �������Ɋe��C���^�[�o����ݒ�
    /// </summary>
    /// <param name="normalAttackIntarval">�ʏ�U���̃C���^�[�o��</param>
    /// <param name="skillAttackIntarval">�X�L���U���̃C���^�[�o��</param>
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
        //�ʏ�U�����\�łȂ��Ȃ�
        if(!isnormalAttackable)
        {
            UpdateNormalAttackTimer();
        }
        //�X�L���U�����\�łȂ��Ȃ�
        if(!isskillAttackable)
        {
            UpdateSkillAttackTimer();
        }

    }


    void UpdateNormalAttackTimer()
    {
        //�^�C�}�[���C���^�[�o���𒴂�����
        if (m_normalAttackTimer >= m_normalAttackIntarval)
        {
            //�U���\
            isnormalAttackable = true;
            return;
        }

        //�^�C�}�[�����Z
        m_normalAttackTimer += Time.deltaTime;
    }

    void UpdateSkillAttackTimer()
    {
        //�^�C�}�[���C���^�[�o���𒴂�����
        if (m_skillAttackTimer >= m_skillAttackIntarval)
        {
            //�U���\
            isskillAttackable = true;
            return;
        }

        //�^�C�}�[�����Z
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
