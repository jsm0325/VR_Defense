using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public GameObject leftGrabPositon;
    public void Use()
    {
        if (leftGrabPositon.GetComponentInChildren<ItemPickUp>() != null)                                               // 손에 아이템이 있는지 체크
        {
            Item.ItemType item = leftGrabPositon.transform.GetChild(1).GetComponent<ItemPickUp>().item.itemType;        //  itemType을 할당
            if (item == Item.ItemType.CatPunch)
            {
                Transform itemObject = leftGrabPositon.transform.GetChild(1);                                           // 손의 1번째 자식 오브젝트 할당(위치가 바뀌면 수정)
                itemObject.GetComponent<ItemCatPunch>().StartCatPunchCoroutine();
            }
        }
    }
}
