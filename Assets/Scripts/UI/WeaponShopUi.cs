using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopUi : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject price;
    public GameObject weaponInformation;
    private GameObject weapon;
    public Transform itemSelect;
    private int weaponLevel;
    [SerializeField] private int[] upgradeCosts = new int[] { 0, 100, 200 }; // 각 무기 레벨별 업그레이드 비용 배열

    public void Start()
    {
        
        weaponLevel = GameManager.gameManager.weaponLevel + 1;
        price.GetComponent<Text>().text = upgradeCosts[weaponLevel - 1].ToString() + " 원";
    }
    private void OnTriggerEnter(Collider other) // 플레이어 접근시 상점 활성화
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            weapon = itemSelect.transform.Find("WP_Bundle").transform.Find(GameManager.gameManager.weaponName).transform.Find(GameManager.gameManager.weaponName + weaponLevel).gameObject;
            ChangeWeapon(GameManager.gameManager.weaponName);
            weaponInformation.GetComponent<Text>().text = "무기 공격력 = " + weapon.GetComponent<Hit>().WeaponData.AttackDamage + '\n' + "무기 공격속도 = " + weapon.GetComponent<Hit>().WeaponData.AttackSpeed + '\n' + "넉백 = " + weapon.GetComponent<Hit>().WeaponData.KnockBack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
        }
    }

    public void UpgradeWeapon()
    {
        if (GameManager.gameManager.SpendCurrency(upgradeCosts[weaponLevel])) // 게임 매니저 코드 돈 있으면 true 및 돈 소모 없으면 false
        {
            if (GameManager.gameManager.weaponLevel >= 3)
            {
                Debug.Log("풀강입니다");
            }
            else
            {
                GameManager.gameManager.weaponLevel += 1;
                weaponLevel = GameManager.gameManager.weaponLevel+1;
                weapon = itemSelect.transform.Find("WP_Bundle").transform.Find(GameManager.gameManager.weaponName).transform.Find(GameManager.gameManager.weaponName + weaponLevel).gameObject;
                price.GetComponent<Text>().text = upgradeCosts[weaponLevel - 1].ToString() + " 원";
                ChangeWeapon(GameManager.gameManager.weaponName);
                weaponInformation.GetComponent<Text>().text = "무기 공격력 = " + weapon.GetComponent<Hit>().WeaponData.AttackDamage + '\n' + "무기 공격속도 = " + weapon.GetComponent<Hit>().WeaponData.AttackSpeed + '\n' + "넉백 = " + weapon.GetComponent<Hit>().WeaponData.KnockBack;
            }
            Debug.Log("구매성공");
        }
        else
        {
            // 상품 구매에 실패한 경우 메시지나 돈 없음을 알려주는 표시 보여주기
            Debug.Log("구매실패");
        }
    }

    public void ChangeWeapon(string weaponName)
    {
        //1랩일 때 1랩 무기 활성
        if (weaponLevel == 1)
        {
            itemSelect.transform.Find("WP_Bundle").transform.Find(weaponName).transform.Find(weaponName + weaponLevel).gameObject.SetActive(true);
        }
        //1랩 초과일 때 전단계무기 비활성 및 현재 무기활성
        else if (1 < weaponLevel && weaponLevel <= 3)
        {
            itemSelect.transform.Find("WP_Bundle").transform.Find(weaponName).transform.Find(weaponName + weaponLevel).gameObject.SetActive(true);
            itemSelect.transform.Find("WP_Bundle").transform.Find(weaponName).transform.Find(weaponName + (weaponLevel - 1)).gameObject.SetActive(false);
        }
        else if (weaponLevel > 3)
        {
            Debug.Log("err");
        }
    }
}
