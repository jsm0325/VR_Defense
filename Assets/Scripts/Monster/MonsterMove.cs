using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private MonsterScriptable monsterData;

    private bool isMovingToDestination = false; // 이동 중인지 여부를 나타내는 변수
    private NavMeshAgent agent;                 // 길을 찾아서 이동할 에이전트

    public GameObject targetSet;                // 몬스터가 다음으로 이동해야 할 위치를 담는 변수
    private List<Transform> target;             // 몬스터가 다음으로 이동해야 할 위치 정보
    private int tarPosIndex = 0;                // 몬스터가 다음에 이동해야 할 목표 지점의 인덱스

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        target = new List<Transform>();

        int childNum = targetSet.transform.childCount;

        for (int i = 0; i < childNum; i++)
            target.Add(targetSet.transform.GetChild(i).transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(target[0].position);   // 목적지 설정
        agent.speed = monsterData.moveSpeed;        // 몬스터 이동 속도 데이터에서 받아와서 설정
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 현재 몬스터가 목적지 향해 이동하는지 검사하는 메소드
    public void InspectDestination()
    {
        if (!isMovingToDestination) // 트랩에 걸리지 않은 상태인데 목적지로 이동중이지 않으면 목적지 설정해줌
        {
            agent.isStopped = false;
            agent.SetDestination(target[tarPosIndex].position);
            isMovingToDestination = true; // 목적지로 이동 중으로 바꿔 agent.SetDestination(target.position); 이 함수 한번만 동작하게 함
        }
    }

    // 몬스터를 멈추게 하는 메소드
    public void Stop()
    {
        isMovingToDestination = false;
        agent.isStopped = true;         // agent 이동 멈추기
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Monster")
        {
            tarPosIndex++;
            agent.SetDestination(target[tarPosIndex].position);
        }
    }
}