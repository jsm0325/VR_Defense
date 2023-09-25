using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int maxHealth = 45; // 최대 체력
    public int currentHealth; // 현재 체력
    public int currency; // 재화
    public int weaponLevel = 1;
    public string weaponName;
    public int score;
    public string studentId;
    private int currentMonsterCount = 0;
    private bool spawnFinished = false;
    private int currentWave = 0;

    public SkillState[] logState;           // 통나무 스킬의 스테이지 당 정보
    public SkillState[] paperState;         // 휴지 스킬의 스테이지 당 정보

    public Score rankingObject;
    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Button")
            {
                print("It's working");
            }
        }

    }

    private void Start()
    {
        currentHealth = maxHealth;  // 시작 시 전체 체력으로 초기화
        currency = 1000;               // 시작 시 재화를 0으로 초기화

        // UiManager가 존재하는지 확인 함
        if (UiManager.uiManager == null)
        {
            Debug.Assert(false, "Error (UiManager is Null) : UiManager를 찾을 수 없습니다.");
            return;
        }

        UiManager.uiManager.UpdateHealthText(currentHealth, maxHealth);
        UiManager.uiManager.UpdateCurrencyText(currency);
    }

    public void DecreaseHealth(int amount) // 체력 감소 메서드
    {
        currentHealth -= amount;
        if (currentHealth <= 0) // 체력 0 되면 게임 오버
        {
            Debug.Log("Game Over");
            rankingObject.AddHighScoreEntry(score, studentId);
        }
        
    }


    public void AddCurrency(int amount)   // 재화 추가
    {
        currency += amount;
    }


    public bool SpendCurrency(int amount)   // 재화 차감
    {
        if (currency >= amount) // 돈 있는 경우
        {
            currency -= amount;
            return true;
        }
        else // 돈 부족한 경우
        {
            return false;
        }
    }

    public void AddMonster()
    {
        currentMonsterCount++;
    }
    public void DieMonster()
    {
        currentMonsterCount--;
        if (spawnFinished == true && currentMonsterCount == 0)
        {
            StageClear();
        }
    }
    public void FinishSpawn()
    {
        spawnFinished = true;
    }

    public void StageClear()
    {
        // 스테이지 클리어 했을 때 작동할 것들 넣기 스킬 강화, 이동 포탈 생성, 그 외 이펙트 등
        Debug.Log("StageClear");
        currentWave++;
        spawnFinished = false;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
