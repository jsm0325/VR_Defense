using System;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseEffect : MonoBehaviour
{
    public GameObject coin;                     // 코인 이펙트를 실행할 오브젝트
    public Text displayCost;                    // 구매 비용을 표시할 Text

    public float moveSpeed;                     // 코인의 이동 속도
    public float fadeSpeed;                     // 사라질 때 투명해지는 속도
    public static bool isPlayCoin = false;      // 코인 이펙트 재생 여부
    
    private Renderer renderer;                  // 코인의 랜더러

    private float currentAlpha = 1.0f;          // 현재 코인의 투명도

    public void Start()
    {
        Debug.Assert(coin != null, "Error (GameObject is Null) : 코인 오브젝트가 존재하지 않습니다.");
        renderer = coin.GetComponent<Renderer>();
        coin.SetActive(false);
    }

    public void Update()
    {
        // 코인을 구매중인 경우
        if (isPlayCoin)
            CoinRise();
    }

    public void BuyItem()
    {
        int getCurrency = GameManager.gameManager.currency;
        int getCost = Int32.Parse(displayCost.text.Substring(0, displayCost.text.Length - 1));

        // 재화가 부족할 경우
        if (getCurrency < getCost)
        {


            return;
        }

        // 구입한 가격을 설정
        ShopButton.BuyItem(getCost);
        isPlayCoin = true;
    }

    public void CoinRise()
    {
        // 코인이 올라갈 때 Start
        if (!coin.activeSelf)
        {
            coin.SetActive(true);
            coin.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            // 코인을 다시 보이게 설정
            currentAlpha = 1.0f;
            Color getColor = renderer.material.color;
            getColor.a = currentAlpha;
            renderer.material.color = getColor;
        }

        // 오브젝트를 위로 이동
        coin.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // 투명도를 감소시키기
        currentAlpha -= fadeSpeed * Time.deltaTime;

        // 오브젝트 렌더러의 투명도 설정
        Color currentColor = renderer.material.color;
        currentColor.a = Mathf.Max(currentAlpha, 0);
        renderer.material.color = currentColor;

        // 투명도가 최소값 이하로 떨어지면 오브젝트 파괴
        if (currentAlpha <= 0)
        {
            coin.SetActive(false);
            isPlayCoin = false;
        }
    }
}
