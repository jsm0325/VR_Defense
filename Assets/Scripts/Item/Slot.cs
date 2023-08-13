using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Transform summonPosition;
    //public bool holding = false;            // 아이템이 손에 있는지 체크

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

   public void SummonItem()
    {
        //if(!holding)
        //{
        //    holding = true;
        GameObject summonitemObject = item.itemPrefab;
        summonitemObject.transform.GetComponent<HoverItem2>().itemRotation = false;
        Instantiate(summonitemObject, summonPosition.transform.position, summonPosition.transform.rotation).transform.SetParent(summonPosition);
        summonitemObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        //}
    }

}
