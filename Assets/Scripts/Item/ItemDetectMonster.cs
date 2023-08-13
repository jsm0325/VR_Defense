using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetectMonster : MonoBehaviour
{
    //trigger Collider를 Item의 자식 개체에 생성해서 적용

    private bool isActive = false;                      //아이템 활성화 여부, 부모 객체 스크립트에서 변수 상태 변경함
    private string parentTag = null;                    //부모 객체의 태그를 저장하는 변수

    private void Awake()
    {
        if (transform.parent == null) {
            Debug.Assert(false, "Error (There is no Parent) : 해당 객체에 parent가 존재하지 않습니다.");
            return;
        }
        parentTag = transform.parent.tag;               //부모 객체의 태그를 읽음
    }

    private void OnTriggerStay(Collider other) 
    {
        if (isActive)                                   //아이템이 활성화된 상태에서
        {
            if (other.gameObject.CompareTag("Monster")) //충돌한 object가 Monster이고
            {  
                if (parentTag == "ItemLullaby")         //자장가 아이템이면
                {
                    MonsterMove monsterMove = other.GetComponent<MonsterMove>();
                    if (monsterMove != null)
                    {
                        monsterMove.Stop();             //Monster의 monsterMove 스크립트를 불러와 Monster의 움직임을 멈추는 Stop 함수를 호출함
                    }
                }
            }
        }
    }

    public void SetIsActive(bool value)                 //isActive변수를 다른 스크립트에서 변경할 수 있게 하는 함수
    {
        isActive = value;
    }
}
