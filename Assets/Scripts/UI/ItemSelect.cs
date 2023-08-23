using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public GameObject itemDisplay;

    private List<KeyValuePair<string, GameObject>> items;
    private string nowOn = "";

    public void Awake()
    {
        items = new List<KeyValuePair<string, GameObject>>();

        Debug.Assert(itemDisplay != null, "Error (GameObject is Null) : 전시할 위치가 존재하지 않습니다.");

        for (int i = 0; i < itemDisplay.transform.childCount; i++)
        {
            GameObject inObject = itemDisplay.transform.GetChild(i).gameObject;

            items.Add(new KeyValuePair<string, GameObject>(inObject.name, inObject));
        }

        nowOn = items[0].Key;
    }

    public void Update()
    {
        
    }

    public void ItemClick()
    {
        if (nowOn == gameObject.name)
            return;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == gameObject.name)
                items[i].Value.gameObject.SetActive(true);

            if (items[i].Key == nowOn)
                items[i].Value.gameObject.SetActive(false);
        }
    }
}