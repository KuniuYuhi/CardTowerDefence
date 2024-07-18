using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMinion : MonoBehaviour, IDamageable
{
    public void Damage(int hitDamage)
    {
        Debug.Log(transform.name + "‚Í" + hitDamage + "‚ğó‚¯‚½");

        
    }

    /// <summary>
    /// €–Sˆ—
    /// </summary>
    public void Die()
    {
        

        Debug.Log(transform.name + "‚Í“|‚³‚ê‚½");
    }


}
