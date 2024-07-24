using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatusData_Unit")]
public class Status_Character : ScriptableObject
{
    
    [SerializeField, Header("�q�b�g�|�C���g")]
    int hp;
    int maxHp;          //�ő�HP

    [SerializeField, Header("�ʏ�U����")]
    int normalAttack;
    int defaultNormalAttack;        //��b�ʏ�U����

    [SerializeField, Header("�ړ����x")]
    float speed;

    [SerializeField, Header("�ʏ�U���̃N�[���^�C��")]
    float normalAtkIntarval;

    [SerializeField, Header("�X�L���U���̃N�[���^�C��")]
    float skillAtkIntarval;

    [SerializeField,Header("���G�͈�")]
    float searchRange;

    [SerializeField, Header("�U���\�͈͓�")]
    float attackRange;

    [SerializeField, Header("��~����")]
    float stopDistance;

    /// <summary>
    /// �X�e�[�^�X��������
    /// </summary>
    public void InitStatus()
    {
        maxHp = hp;
        defaultNormalAttack = normalAttack;
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public int GetNormalAttack()
    {
        return normalAttack;
    }

    public int GetDefaultNormalAttack()
    {
        return defaultNormalAttack;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetSkillAtkIntarval()
    {
        return skillAtkIntarval;
    }


    public float GetNormalAtkIntarval()
    {
        return normalAtkIntarval;
    }

    public float GetSearchRange()
    {
        return searchRange;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetStopDistance()
    {
        return stopDistance;
    }

    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="hitDamage">�󂯂�_���[�W��</param>
    public void ApplyDamage(int hitDamage)
    {
       int currentHp = this.hp - hitDamage;

        //HP��0�ȉ��ɂȂ�Ȃ��悤�ɂ���
        this.hp = Mathf.Max(currentHp, 0);
    }


    /// <summary>
    /// HP���񕜂���
    /// </summary>
    /// <param name="amount">�񕜂����</param>
    public void RecoverHp(int amount)
    {
        int currentHp = this.hp + amount;

        //HP���ő�HP�������Ȃ��悤�ɂ���
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        this.hp = currentHp;
    }



}
