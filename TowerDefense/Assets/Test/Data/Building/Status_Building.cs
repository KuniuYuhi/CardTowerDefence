using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StatusData_Building")]
public class Status_Building : ScriptableObject
{
    [SerializeField, Header("�����̃^�C�v")]
    EnBuildingType buildingType;

    [SerializeField,Header("�ϋv�l")]
    int endurance;

    [SerializeField, Header("�e����^����l�i�U���͂�񕜗͂Ȃǁj")]
    int effectPower;

    [SerializeField, Header("���ʂ�^����͈�(���a)")]
    float Radius;

    [SerializeField, Header("�C���^�[�o��")]
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

        //HP��0�ȉ��ɂȂ�Ȃ��悤�ɂ���
        endurance = Mathf.Max(hp, 0);
    }

}

public enum EnBuildingType
{
    enBuildingType_Canon,                       //��C�^�C�v
    enBuildingType_AreaEffect_Damage,           //�͈͓��Ƀ_���[�W��^����^�C�v
    enBuildingType_AreaEffect_Heal,             //�͈͓����񕜂���^�C�v
    enBuildingType_AreaEffect_DamageOverTime,   //�͈͓��Ɏ����_���[�W��^����^�C�v
}


