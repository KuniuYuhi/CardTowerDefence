using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMinion : MonoBehaviour, IDamageable
{
    public void Damage(int hitDamage)
    {
        Debug.Log(transform.name + "��" + hitDamage + "���󂯂�");

        
    }

    /// <summary>
    /// ���S������
    /// </summary>
    public void Die()
    {
        

        Debug.Log(transform.name + "�͓|���ꂽ");
    }


}
