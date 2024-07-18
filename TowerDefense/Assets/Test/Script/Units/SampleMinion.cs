using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMinion : MonoBehaviour, IDamageable
{
    public void Damage(int hitDamage)
    {
        Debug.Log(transform.name + "は" + hitDamage + "を受けた");

        
    }

    /// <summary>
    /// 死亡時処理
    /// </summary>
    public void Die()
    {
        

        Debug.Log(transform.name + "は倒された");
    }


}
