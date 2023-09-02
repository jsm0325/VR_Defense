using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public GameObject itemDisplay;                          // 아이템을 전시할 목록
    public string content;                                  // 이 스크립트를 넣은 객체의 설명

    private List<KeyValuePair<string, GameObject>> items;   // 전시할 목록을 리스트로 저장
    private static string nowOn = "";                       // 현재 세부 사항에 표시되는 아이템 이름

    public void Awake()
    {
        items = new List<KeyValuePair<string, GameObject>>();

        Debug.Assert(itemDisplay != null, "Error (GameObject is Null) : 전시할 위치가 존재하지 않습니다.");

        // 아이템할 목록을 가져와 이름과 오브젝트가 저장되는 리스트에 저장
        for (int i = 0; i < itemDisplay.transform.childCount; i++)
        {
            GameObject inObject = itemDisplay.transform.GetChild(i).gameObject;

            items.Add(new KeyValuePair<string, GameObject>(inObject.name, inObject));
        }

        // 첫번째 아이템을 세부사항에 표시
        nowOn = items[0].Key;
    }

    public void Update()
    {
        
    }

    public void ItemClick()
    {
        // 같은 것을 클릭할 경우 중지
        if (nowOn == gameObject.name)
            return;

        // 아이템 목록에서 현재 켜져있는 아이템을 끔
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == nowOn)
            {
                items[i].Value.gameObject.SetActive(false);
                break;
            }
        }
        
        // 아이템 목록에서 클릭한 아이템을 킴
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == gameObject.name)
            {
                items[i].Value.gameObject.SetActive(true);
                nowOn = items[i].Key;                       // 선택한 아이템를 현재 세부사항에 표시
                break;
            }
        }
    }
}