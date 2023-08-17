using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private bool isTrapped = false; // 트랩 걸렸는지 여부
    public float trapDuration;      // 트랩 지속 시간

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

    // 움직임 관련 변수
    private MonsterMove moveComponent;

    // 넉백 변수
    Vector3 KnockBackPosition;

   
    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();    // 몬스터에서 hp 슬라이더를 찾음
        moveComponent = GetComponent<MonsterMove>();

        // 몬스터에 몬스터 슬라이더가 존재하는지 확인
        if (hpSlider == null)
            Debug.Assert(false, "Error (Monster Slider) : 몬스터에 체력 바가 존재하지 않습니다.");

        // 몬스터에 움직임관련 컴포넌트가 존재하는지 확인
        if (moveComponent == null)
            Debug.Assert(false, "Error (Monster Move) : 몬스터에 움직임에 관한 컴포넌트가 존재하지 않습니다.");
    }
    private void Start()
    {
        currentHealth = monsterData.maxHp; // 현재 체력 최대 체력으로 설정
        InitializeAppearanceOptions(); // 외형 카테고리 배열 초기화
        SetRandomAppearance(); // 랜덤하게 바디, 의상, 헤어 등을 선택하여 적용
        
    }

    void Update()
    {
        if (isTrapped)
            return;

        moveComponent.InspectDestination();
    }

    public void SetTrapped(float duration)
    {
        if (duration < 0)
        {
            Debug.Assert(false, "Error (Unacceptable Value) : 스턴 길이는 음수가 될 수 없습니다.");
            return;
        }

        if (!isTrapped)
        {
            isTrapped = true;
            moveComponent.Stop();
            trapDuration = duration;

            StartCoroutine(ReleaseFromTrap()); // 멈춤 상태를 표현하는 애니메이션 등을 추가하기 
        }
    }

    private IEnumerator ReleaseFromTrap()
    {
        yield return new WaitForSeconds(trapDuration); // 몬스터 정지
        isTrapped = false;
    }

    private IEnumerator KnockBack(Vector3 weaponpos, float knockback)
    {
        //Lerp사용 밀려나는 느낌이 들게 만듬
        float flytime = 0.0f;

        while (flytime < 0.125) //0.2초 동안 넉백
        {
            flytime += (Time.deltaTime);
            KnockBackPosition = transform.position + ((transform.position - weaponpos) * knockback);    //현재 위치 - 무기 위치에 밀려나는 정도를 곱하고 더해 밀려난 위치 얻음
            transform.position = Vector3.Lerp(transform.position, KnockBackPosition, flytime/ 0.125f);     //0.125초를 기준으로 날라감

            yield return null;
        }
        yield return null;
    }

    public void TakeDamage(int damage,Vector3 weaponpos,float knockback) // 데미지 받는 코드
    {
        currentHealth -= damage;    // 현재 체력에서 데미지 만큼 빼는 코드
        StartCoroutine(KnockBack(weaponpos, knockback));    //넉백 코루틴
      
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

    public void Die()// 몬스터가 죽었을 때 호출
    {
        ItemDrop();
        GameManager.gameManager.AddCurrency(monsterData.coin); // 몬스터 coin 값 만큼 재화 증가
        
        UiManager.uiManager.UpdateCurrencyText(GameManager.gameManager.currency);
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

    private void SetRandomAppearance() // 랜덤 의상 생성 코드
    {
        for (int i = 0; i < appearanceOptions.Length; i++)
        {
            // 해당 카테고리의 배열 길이가 0 이상인 경우에만 랜덤한 인덱스 선택
            if (appearanceOptions[i].Length > 0)
            {
                int randomIndex = Random.Range(0, appearanceOptions[i].Length);
                GameObject selectedAppearancePrefab = appearanceOptions[i][randomIndex];
                GameObject selectedAppearance = Instantiate(selectedAppearancePrefab, transform.position, transform.rotation, transform);
                selectedAppearance.transform.localScale = clothsScale;
                Animator appearanceAnim = selectedAppearance.GetComponent<Animator>();
                if (appearanceAnim == null)
                {
                    appearanceAnim = selectedAppearance.AddComponent<Animator>();
                }
                appearanceAnim.runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;
            }
        }
    }

    public void ItemDrop()
    {
        Choose(new float[2] { 10f, 90f });
        
        float Choose(float[] probs)
        {
            
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    switch(i)
                    {
                        case 0:
                            int rand = Random.Range(0, monsterData.dropItem.Length);
                            Instantiate(monsterData.dropItem[rand], transform.position, Quaternion.identity);
                            break;
                        case 1:
                            break;
                    }
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

    }
}