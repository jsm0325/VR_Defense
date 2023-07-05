using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int price; // 상품 가격

    private void BuyItem()
    {
        if (GameManager.Instance.SpendCurrency(price)) // 게임 매니저 코드 돈 있으면 true 및 돈 소모 없으면 false
        {
            // 상품 구매에 성공했을 때 기능 작성하기
            
        }
        else
        {
            // 상품 구매에 실패한 경우 메시지나 돈 없음을 알려주는 표시 보여주기
            
        }
    }
}
