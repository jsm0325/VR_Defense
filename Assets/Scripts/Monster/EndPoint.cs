using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            // 몬스터인 경우 해당 객체를 파괴
            Destroy(other.gameObject);
        }
    }
}
