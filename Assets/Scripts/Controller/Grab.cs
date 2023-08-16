using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grab : MonoBehaviour
{
    private bool isGrabbing = false;
    public Transform leftGrabPositon;
    public Transform rightGrabPosition;
    public GameObject itemData;
    public int BatLevel = 1;
    public int RacketLevel = 1;
    public int WrenchLevel = 1;
    public ItemSlot itemSlot;
    private GameObject Grabbable;
    private LineRenderer lineRenderer;
    private RaycastHit hit;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))       //버튼을 눌렀을 때
        {
            isGrabbing = true;
        }
        else if(OVRInput.GetUp(OVRInput.RawButton.A))        //A 버튼을 누르면 놓는다
        {
            isGrabbing = false;
        }

        if (!isGrabbing)        //만약 놓는다면 모든 무기를 비활성 한다.
        {
            ChangeWeapon(false, false, false);
        }

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            ItemGrab();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            lineRenderer.enabled = false;
        }

        if(OVRInput.Get(OVRInput.RawButton.B))
        {
            itemData.GetComponent<ItemUse>().Use();         // 아이템 사용
        }
    }

    private void OnTriggerEnter(Collider other)     //Enter이여서 버튼누른 상태로 오브젝트에 가져가야함
    {
        Grabbable = other.gameObject;

        if (other.CompareTag("Grabbable"))
        {
            if (isGrabbing)      //만약 잡는다면
            {
                //잡히는 Grabbable의 위치 값을 손에 맞춤
                Grabbable.transform.position = rightGrabPosition.transform.position;
                Grabbable.transform.rotation = rightGrabPosition.transform.rotation;

                //Grabbable을 손의 하위 객체로 지정
                Grabbable.transform.SetParent(rightGrabPosition);

                //손에 고정하기 위해 iskinematic 활성
                Grabbable.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Grabbable = other.gameObject;

        if(other.CompareTag("Grabbable"))
        {
            if(!isGrabbing)        //놓는다면
            {
                //Grabbable 손의 하위 객체에서 해제
                Grabbable.transform.SetParent(null);

                //손에 놓기 위해 iskinematic 비활성
                Grabbable.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        if(other.CompareTag("ShopBat"))
        {
            if(isGrabbing)
            {
                ChangeWeapon(true, false, false);       //bat만 활성화
            }
        }
        if (other.CompareTag("ShopRacket"))
        {
            if (isGrabbing)
            {
                ChangeWeapon(false, true, false);       //Racket만 활성화
            }
        }
        if (other.CompareTag("ShopWrench"))
        {
            if (isGrabbing)
            {
                ChangeWeapon(false, false, true);       //wrench만 활성화
            }
        }
    }

    private void ChangeWeapon(bool bat, bool racket, bool wrench)
    {
        //1랩일 때 1랩 무기 활성
        if (BatLevel == 1)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Bat" + BatLevel).gameObject.SetActive(bat);
        }
        //1랩 초과일 때 전단계무기 비활성 및 현재 무기활성
        else if (1 < BatLevel && BatLevel <= 3)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Bat" + BatLevel).gameObject.SetActive(bat);
            rightGrabPosition.Find("WP_Bundle").transform.Find("Bat" + (BatLevel - 1)).gameObject.SetActive(false);
        }
        else if(BatLevel > 3)
        {
            Debug.Log("err");
        }

        //1랩일 때 1랩 무기 활성
        if (RacketLevel == 1)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Racket" + RacketLevel).gameObject.SetActive(racket);
        }
        //1랩 초과일 때 전단계무기 비활성 및 현재 무기활성
        else if (1 < RacketLevel && RacketLevel <= 3)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Racket" + RacketLevel).gameObject.SetActive(racket);
            rightGrabPosition.Find("WP_Bundle").transform.Find("Racket" + (RacketLevel - 1)).gameObject.SetActive(false);
        }
        else if (RacketLevel > 3)
        {
            Debug.Log("err");
        }
        //1랩일 때 1랩 무기 활성hzl

        if (WrenchLevel == 1)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Wrench" + WrenchLevel).gameObject.SetActive(wrench);
        }
        //1랩 초과일 때 전단계무기 비활성 및 현재 무기활성
        else if (1 < WrenchLevel && WrenchLevel <= 3)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Wrench" + WrenchLevel).gameObject.SetActive(wrench);
            rightGrabPosition.Find("WP_Bundle").transform.Find("Wrench" + (WrenchLevel - 1)).gameObject.SetActive(false);
        }
        else if (WrenchLevel > 3)
        {
            Debug.Log("err");
        }

    }

    private void ItemGrab()
    {
        Debug.DrawRay(leftGrabPositon.position, leftGrabPositon.forward * 10, Color.red);
        //Debug.DrawRay(GrabPositon.position, GrabPositon.forward * 10, Color.blue);

        // 라인 시작점
        lineRenderer.SetPosition(0, leftGrabPositon.position);
        // 라인 종료점
        lineRenderer.SetPosition(1, leftGrabPositon.position + (leftGrabPositon.forward * 10));
        lineRenderer.enabled = true;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.tag == "Item")
            {
                if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))        // 버튼 뗐을때
                {
                    hit.transform.GetComponent<HoverItem2>().itemRotation = false;                          // 아이템 회전 중지
                    hit.transform.position = leftGrabPositon.transform.position;    // 아이템 팔 위치로
                    hit.transform.SetParent(leftGrabPositon);
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    if (this.name == "RightControllerGrabposition")                                         // this가 오른쪽 컨트롤러가 되면 PickUp함수가 오른쪽, 왼쪽 두 번실행되므로 this 체크
                    {
                        return;
                    }
                    else
                    {
                        PickUp();
                    }
                }
            }
        }
    }

    private void PickUp()
    {
        if(hit.collider.tag == "Item")
        {
            Debug.Log(hit.transform.GetComponent<ItemPickUp>().item.itemName + "획득");
            itemSlot.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
            Destroy(hit.transform.gameObject, 1f);                  // 삭제
        }
    }
}
