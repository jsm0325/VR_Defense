using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemFunction : MonoBehaviour
{
    public IEnumerator SeeKitten(Collider monster, GameObject gameObject, float duration)
    {
        if(gameObject != null)
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
            StopCoroutine(SeeKitten(monster, gameObject, duration));
        }
    }
}
