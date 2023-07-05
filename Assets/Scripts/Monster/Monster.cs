using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    private bool isTrapped = false; // 트랩 걸렸는지 여부
    public float trapDuration; // 트랩 지속 시간
    private bool isMovingToDestination = false; // 이동 중인지 여부를 나타내는 변수
    NavMeshAgent agent; // 길을 찾아서 이동할 에이전트

    public MonsterScriptable monsterData; // 몬스터 데이터 스크립터블 객체
    private int currentHealth; // 현재 체력
    private void Awake()
    {
       
        agent = GetComponent<NavMeshAgent>(); // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
    }
    private void Start()
    {
        currentHealth = monsterData.maxHp; // 현재 체력 최대 체력으로 설정
        agent.SetDestination(target.position); // 목적지 설정
        agent.speed = monsterData.moveSpeed; // 몬스터 이동 속도 데이터에서 받아와서 설정
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
    public void TakeDamage(int damage) // 데미지 받는 코드
    {
        currentHealth -= damage; // 현재 체력에서 데미지 만큼 빼는 코드
        if (currentHealth <= 0) // 체력 0 이하시 작동
        {
            Die(); 
        }
    }

    private void Die()// 몬스터가 죽었을 때 호출
    {
        GameManager.Instance.AddCurrency(monsterData.coin); // 몬스터 coin 값 만큼 재화 증가
        Destroy(gameObject); // 몬스터 게임 오브젝트 삭제
    }
}
