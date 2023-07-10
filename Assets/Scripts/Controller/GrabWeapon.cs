using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabWeapon : MonoBehaviour
{
    private bool isGrabbing = false;
    public Transform GrabPositon;
    private GameObject Weapon;
    void FixedUpdate()
    {
        if(OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))       //버튼을 눌렀다 뗐을 때
        {
            isGrabbing = true;
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))        //버튼을 눌렀을 때
        {
            isGrabbing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Weapon = other.gameObject;

        if(other.CompareTag("Weapon"))
        {
            if(isGrabbing)      //만약 잡는다면
            {
                //잡히는 Weapon의 위치 값을 손에 맞춤
                Weapon.transform.position = GrabPositon.transform.position;
                Weapon.transform.rotation = GrabPositon.transform.rotation;

                //Weapon을 손의 하위 객체로 지정
                Weapon.transform.SetParent(GrabPositon);

                //손에 고정하기 위해 iskinematic 활성
                Weapon.GetComponent<Rigidbody>().isKinematic = true;
            }

            else if(!isGrabbing)        //놓는다면
            {
                //Weapon을 손의 하위 객체에서 해제
                Weapon.transform.SetParent(null);

                //손에 놓기 위해 iskinematic 비활성
                Weapon.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
