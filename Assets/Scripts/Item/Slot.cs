using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;

    [SerializeField]
    private TextMeshProUGUI textCount;

    // 아이템 획득
    public void AddItem(Item item, int count = 1)
    {
        this.item = item;
        itemCount = count;
        textCount.text = itemCount.ToString();
    }

    // 아이템 개수 조정
    public void SetSlotCount(int count)
    {
        itemCount += count;
        textCount.text = itemCount.ToString();
    }

}
