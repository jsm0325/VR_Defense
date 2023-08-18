using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemKitten : MonoBehaviour
{
    [SerializeField]
    private float duration = 5.0f;
    [SerializeField]
    private float range = 0f;
    private SphereCollider kittenCollider;

    private void Awake()
    {
        kittenCollider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            if (transform.root.name.Equals("OVRPlayerController") == false)
            {
                Debug.Log("½ÇÇà");
                StartCoroutine(SeeKitten(other));
            }
        }
    }

    private IEnumerator SeeKitten(Collider monster)
    {
        float moveTime = 0.0f;
        MonsterMove monsterMove = monster.GetComponent<MonsterMove>();
        int posTempIndex;
        posTempIndex = monster.GetComponent<MonsterMove>().tarPosIndex;

        monsterMove.target.Add(gameObject.transform);
        monsterMove.tarPosIndex = monsterMove.target.Count - 1;

        NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
        agent.SetDestination(monsterMove.target[monsterMove.tarPosIndex].position);
        while (moveTime < duration)
        {
            moveTime += Time.deltaTime;
            yield return null;
        }

        monsterMove.tarPosIndex = posTempIndex;
        agent.SetDestination(monsterMove.target[monsterMove.tarPosIndex].position);
        Destroy(gameObject);
    }

    //private IEnumerator DestroyObject()
    //{
    //    float time = 0.0f;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    Destroy(gameObject);
    //}
}
