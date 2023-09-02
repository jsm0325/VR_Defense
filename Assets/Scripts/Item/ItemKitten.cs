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
    private HoverItem2 hoverItem;

    private void Awake()
    {
        kittenCollider = GetComponent<SphereCollider>();
        hoverItem = GetComponent<HoverItem2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hoverItem.itemRotation == false)
        {
            StartCoroutine(DestroyKitten());
            kittenCollider.radius = range;
            if (other.CompareTag("Monster"))
            {
                if (transform.root.name.Equals("OVRPlayerController") == false)
                {
                    //itemFunction.StartCoroutine(itemFunction.SeeKitten(other, this.gameObject, duration));
                    StartCoroutine(SeeKitten(other));
                }
            }
        }
    }

    private IEnumerator DestroyKitten()
    {
        float time = 0.0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = new Vector3(1000, 100, 1000);

        time = 0.0f;
        while (time < 20.0f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
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
        gameObject.transform.position = new Vector3(1000, 100, 1000);

        moveTime = 0.0f;
        while(moveTime < 20.0f)
        {
            moveTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}