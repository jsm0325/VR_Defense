using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AI;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target;
    private bool isTrapped = false; // 트랩 걸렸는지 여부
    public float trapDuration;
    private bool isMovingToDestination = false; // 이동 중인지 여부를 나타내는 변수
    // 길을 찾아서 이동할 에이전트
    NavMeshAgent agent;

    private void Awake()
    {
        // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(target.position);
    }

    void Update()
    {
        if (!isTrapped) 
        {
            if (!isMovingToDestination) // 트랩에 걸리지 않은 상태인데 목적지로 이동중이지 않으면 목적지 설정해줌
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                isMovingToDestination = true; // 목적지로 이동 중으로 바꿔 agent.SetDestination(target.position); 이 함수 한번만 동작하게 함
            }
        }
    }

    public void SetTrapped(float duration)
    {
        if (!isTrapped)
        {
            isTrapped = true;
            isMovingToDestination = false;
            agent.isStopped = true; // agent 이동 멈추기
            trapDuration = duration;
            StartCoroutine(ReleaseFromTrap()); // 멈춤 상태를 표현하는 애니메이션 등을 추가하기 
        }
    }

    private IEnumerator ReleaseFromTrap()
    {
        yield return new WaitForSeconds(trapDuration); // 몬스터 정지
        isTrapped = false;
    }
}