using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/StatusData_Base")]
public class Status_Base : ScriptableObject
{
    [SerializeField,Header("耐久値")]
    int endurance;

    [SerializeField, Header("最大コスト")]
    float cost;



    public int GetEndurance()
    {
        return endurance;
    }


    public void ApplyDamage(int hitDamage)
    {
        int hp = endurance - hitDamage;

        //HPが0以下にならないようにする
        endurance = Mathf.Max(hp, 0);
    }

}
