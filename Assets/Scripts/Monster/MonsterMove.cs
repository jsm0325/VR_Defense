using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private MonsterScriptable monsterData;

    private bool isStop = false; // 이동 중인지 여부를 나타내는 변수
    private NavMeshAgent agent;                 // 길을 찾아서 이동할 에이전트

    public GameObject targetSet;                // 몬스터가 다음으로 이동해야 할 위치를 담는 변수
    private List<Transform> target;             // 몬스터가 다음으로 이동해야 할 위치 정보
    private int tarPosIndex = 0;                // 몬스터가 다음에 이동해야 할 목표 지점의 인덱스

    private Rigidbody rigid;
    private Collider collid;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        target = new List<Transform>();
        rigid = GetComponent<Rigidbody>();
        collid = GetComponent<Collider>();
        if (agent == null)
        {
            Debug.Assert(false, "Error (NavMeshAgent is Null) : 해당 객체에 NavMeshAgent가 존재하지 않습니다.");
            return;
        }

        if (rigid == null)
        {
            Debug.Assert(false, "Error (RigidBody is Null) : 해당 객체에 RigidBody가 존재하지 않습니다.");
            return;
        }

        if (collid == null)
        {
            Debug.Assert(false, "Error (NavMeshAgent is Null) : 해당 객체에 Collider가 존재하지 않습니다.");
            return;
        }
        // 목표지점의 개수
        int childNum = targetSet.transform.childCount;

        // 목표지점을 기져옴
        for (int i = 0; i < childNum; i++)
            target.Add(targetSet.transform.GetChild(i).transform);
    }

    void Start()
    {
        agent.SetDestination(target[0].position);   // 목적지 설정
        agent.speed = monsterData.moveSpeed;        // 몬스터 이동 속도 데이터에서 받아와서 설정
    }

    void Update()
    {
        
    }

    // 현재 몬스터가 목적지 향해 이동하는지 검사하는 메소드
    public void InspectDestination()
    {
        // 미끄럼 방지
        rigid.velocity = Vector3.zero;

        // 현재 몬스터가 이동하지 않는다면 이후 스크립트는 실행하지 않음
        if (!isStop)
            return;

        // 몬스터가 이동중이지 않을 때 다음 위치로 이동하게 설정
        agent.isStopped = false;
        agent.SetDestination(target[tarPosIndex].position);
        isStop = true; // 목적지로 이동 중으로 바꿔 agent.SetDestination(target.position); 이 함수 한번만 동작하게 함
    }

    // 몬스터를 멈추게 하는 메소드
    public void Stop()
    {
        isStop = false;
        agent.isStopped = true;             // agent 이동 멈추기
        rigid.velocity = Vector3.zero;
        collid.isTrigger = true;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MoveTarget")
        {
            string name = collider.gameObject.name;

            if (name == "Goal")
            {
                gameObject.SetActive(false);
                return;
            }

            tarPosIndex = Convert.ToInt32(name.Substring(name.Length - 1, 1));
            agent.SetDestination(target[tarPosIndex].position);
        }
    }
}