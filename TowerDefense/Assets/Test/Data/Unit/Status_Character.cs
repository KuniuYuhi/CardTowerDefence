using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatusData_Unit")]
public class Status_Character : ScriptableObject
{
    
    [SerializeField, Header("ヒットポイント")]
    int hp;
    int maxHp;          //最大HP

    [SerializeField, Header("通常攻撃力")]
    int normalAttack;
    int defaultNormalAttack;        //基礎通常攻撃力

    [SerializeField, Header("移動速度")]
    float speed;

    [SerializeField, Header("通常攻撃のクールタイム")]
    float normalAtkIntarval;

    [SerializeField, Header("スキル攻撃のクールタイム")]
    float skillAtkIntarval;

    [SerializeField,Header("索敵範囲")]
    float searchRange;

    [SerializeField, Header("攻撃可能範囲内")]
    float attackRange;

    [SerializeField, Header("停止距離")]
    float stopDistance;

    /// <summary>
    /// ステータスを初期化
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
    /// ダメージを受ける
    /// </summary>
    /// <param name="hitDamage">受けるダメージ量</param>
    public void ApplyDamage(int hitDamage)
    {
       int currentHp = this.hp - hitDamage;

        //HPが0以下にならないようにする
        this.hp = Mathf.Max(currentHp, 0);
    }


    /// <summary>
    /// HPを回復する
    /// </summary>
    /// <param name="amount">回復する量</param>
    public void RecoverHp(int amount)
    {
        int currentHp = this.hp + amount;

        //HPが最大HPをこえないようにする
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        this.hp = currentHp;
    }



}
