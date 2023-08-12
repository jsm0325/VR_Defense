using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxHealth = 45; // 최대 체력
    public int currentHealth; // 현재 체력
    public int currency; // 재화

    private void Awake()
    {
        if(instance =null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
        currentHealth = maxHealth; // 시작 시 전체 체력으로 초기화
        currency = 0; // 시작 시 재화를 0으로 초기화

        // UiManager가 존재하는지 확인 함
        if (UiManager.instance == null)
        {
            Debug.Assert(false, "Error (UiManager is Null) : UiManager를 찾을 수 없습니다.");
            return;
        }

        UiManager.instance.UpdateHealthText(currentHealth, maxHealth);
        UiManager.instance.UpdateCurrencyText(currency);
    }

    public void DecreaseHealth(int amount) // 체력 감소 메서드
    {
        currentHealth -= amount;
        if (currentHealth <= 0) // 체력 0 되면 게임 오버
        {
            Debug.Log("Game Over");
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
}
