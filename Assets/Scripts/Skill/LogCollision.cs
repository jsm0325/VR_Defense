using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCollision : MonoBehaviour
{
    public int damage = 50;
    public float knockback = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();

        if (monster != null)
        {
            // 몬스터에 데미지를 입힘
            monster.TakeDamage(damage, gameObject.transform.position, knockback);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);           // 한 번만 충돌하도록 설정
        }
    }
}
