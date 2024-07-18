using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StatusData_Building")]
public class Status_Building : ScriptableObject
{
    [SerializeField, Header("建物のタイプ")]
    EnBuildingType buildingType;

    [SerializeField,Header("耐久値")]
    int endurance;

    [SerializeField, Header("影響を与える値（攻撃力や回復力など）")]
    int effectPower;

    [SerializeField, Header("効果を与える範囲(半径)")]
    float Radius;

    [SerializeField, Header("インターバル")]
    float intarval;

   


    public EnBuildingType GetBuildingType()
    {
        return buildingType;
    }

    public int GetEndurance()
    {
        return endurance;
    }

    public int GetEffectPower()
    {
        return effectPower;
    }

    public float GetRadius()
    {
        return Radius;
    }

    public float GetIntarval()
    {
        return intarval;
    }

    public void ApplyDamage(int hitDamage)
    {
        int hp = endurance - hitDamage;

        //HPが0以下にならないようにする
        endurance = Mathf.Max(hp, 0);
    }

}

public enum EnBuildingType
{
    enBuildingType_Canon,                       //大砲タイプ
    enBuildingType_AreaEffect_Damage,           //範囲内にダメージを与えるタイプ
    enBuildingType_AreaEffect_Heal,             //範囲内を回復するタイプ
    enBuildingType_AreaEffect_DamageOverTime,   //範囲内に持続ダメージを与えるタイプ
}


