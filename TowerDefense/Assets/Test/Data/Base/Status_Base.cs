using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StatusData_Base")]
public class Status_Base : ScriptableObject
{
    [SerializeField,Header("�ϋv�l")]
    int endurance;

    [SerializeField, Header("�ő�R�X�g")]
    float cost;



    public int GetEndurance()
    {
        return endurance;
    }


    public void ApplyDamage(int hitDamage)
    {
        int hp = endurance - hitDamage;

        //HP��0�ȉ��ɂȂ�Ȃ��悤�ɂ���
        endurance = Mathf.Max(hp, 0);
    }

}
