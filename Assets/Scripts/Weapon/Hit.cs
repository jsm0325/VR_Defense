using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public WeaponScriptable WeaponData;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(WeaponData.AttackDamage, transform.position, WeaponData.KnockBack);
            
        }
    }
}
