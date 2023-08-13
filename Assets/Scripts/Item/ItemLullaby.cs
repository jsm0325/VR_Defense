using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLullaby : MonoBehaviour
{
    
    public float validTime = 10.0f;                               //아이템 유효시간
 
    private bool isObtained = true;                               //아이템이 사용자에게 획득된 적이 있는지 여부
                                                                   //아이템 획득을 관리하는 코드에서 true로 변경해줘야 함
    private bool isInstall = false;                                //아이템 설치 여부
    private bool isWaitingToDestroy = false;                        //아이템이 삭제 대기 중인지의 여부
    
    private Rigidbody rigid;

    ItemDetectMonster itemDetect = null;
    Vector3 pos;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.Assert(false, "Error (RigidBody is Null) : 해당 객체에 RigidBody가 존재하지 않습니다.");
            return;
        }

        Transform child = transform.GetChild(0);                    //첫번째 자식 개체를 불러옴
        if (child == null) {
            Debug.Assert(false, "Error (There is no child) : 해당 객체에 child가 존재하지 않습니다.");
            return;
        }

        itemDetect = child.GetComponent<ItemDetectMonster>();
        if (itemDetect == null)
        {
            Debug.Assert(false, "Error (There is no Script) : 해당 객체에 해당 스크립트가 존재하지 않습니다.");
            return;
        }
    }

    private void Start() {
        pos = transform.position;
    }

    private void Update() 
    {
        if (isInstall) 
        {
            transform.position = pos;
            if (!isWaitingToDestroy)
            {
                itemDetect.SetIsActive(true);
                Invoke("DestroyObject", validTime); //삭제 대기 중으로 상태 변화
                isWaitingToDestroy = true;
                rigid.isKinematic = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Floor"))
        {   
            if (isObtained && !isInstall)
            {   
                pos = transform.position;
                isInstall = true;
            }
        }
    }

    private void DestroyObject() 
    {
        Destroy(gameObject);
    }

    public void SetIsObtain(bool value)
    {
        isObtained = value;
    }
}
