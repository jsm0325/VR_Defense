using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Transform target; // 목표 지점
    private bool isTrapped = false; // 트랩 걸렸는지 여부
    public float trapDuration; // 트랩 지속 시간
    private bool isMovingToDestination = false; // 이동 중인지 여부를 나타내는 변수
    NavMeshAgent agent; // 길을 찾아서 이동할 에이전트

    // 체력 관련 변수
    public MonsterScriptable monsterData;               // 몬스터 데이터 스크립터블 객체
    public int currentHealth { get; private set; }      // 현재 체력 (외부에서 읽기 허용)

    private Slider hpSlider;                            // 체력 슬라이더

    // 외형 카테고리들을 저장할 배열의 배열
    public GameObject[][] appearanceOptions;
    // 외형 카테고리들
    public GameObject[] hairOptions;
    public GameObject[] topOptions;
    public GameObject[] bottomOptions;
    public GameObject[] shoeOptions;
    public Vector3 clothsScale = new Vector3(11.0f, 11.0f, 9.0f);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();           // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        hpSlider = GetComponentInChildren<Slider>();    // 몬스터에서 hp 슬라이더를 찾음

        // 몬스터에 몬스터 슬라이더가 존재하는지 확인
        if (hpSlider == null)
            Debug.Log("Error (Monster Slider) : 몬스터에 체력 바가 존재하지 않습니다.");
    }
    private void Start()
    {
        currentHealth = monsterData.maxHp; // 현재 체력 최대 체력으로 설정
        agent.SetDestination(target.position); // 목적지 설정
        agent.speed = monsterData.moveSpeed; // 몬스터 이동 속도 데이터에서 받아와서 설정
        InitializeAppearanceOptions(); // 외형 카테고리 배열 초기화
        SetRandomAppearance(); // 랜덤하게 바디, 의상, 헤어 등을 선택하여 적용
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
        if (duration < 0)
        {
            Debug.Log("Error (Unacceptable Value) : 스턴 길이는 음수가 될 수 없습니다.");
            return;
        }

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
        currentHealth -= damage;    // 현재 체력에서 데미지 만큼 빼는 코드

        // 체력 0 이하시 작동
        if (currentHealth <= 0)
        {
            hpSlider.value = 0;
            Die(); 
        }

        // 만약 캐릭터가 체력이 남아있을 경우
        else
            hpSlider.value = ((float)currentHealth / monsterData.maxHp) * 100;  // 현재 체력을 슬라이더에 반영
    }

    private void Die()// 몬스터가 죽었을 때 호출
    {
        GameManager.Instance.AddCurrency(monsterData.coin); // 몬스터 coin 값 만큼 재화 증가
        UiManager.instance.UpdateCurrencyText(GameManager.Instance.currency);
        Destroy(gameObject); // 몬스터 게임 오브젝트 삭제
    }

    private void InitializeAppearanceOptions()
    {
        // 외형 카테고리 배열 생성과 초기화
        appearanceOptions = new GameObject[4][];
        appearanceOptions[0] = hairOptions;
        appearanceOptions[1] = topOptions;
        appearanceOptions[2] = bottomOptions;
        appearanceOptions[3] = shoeOptions;
    }

    private void SetRandomAppearance()
    {
        for (int i = 0; i < appearanceOptions.Length; i++)
        {
            // 해당 카테고리의 배열 길이가 0 이상인 경우에만 랜덤한 인덱스 선택
            if (appearanceOptions[i].Length > 0)
            {
                int randomIndex = Random.Range(0, appearanceOptions[i].Length);
                GameObject selectedAppearance = appearanceOptions[i][randomIndex];
                selectedAppearance.transform.localScale = clothsScale;
                Instantiate(selectedAppearance, transform);
            }
        }
    }

}
