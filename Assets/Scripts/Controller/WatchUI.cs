using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchUI : MonoBehaviour
{
    [SerializeField]
    private GameObject watchUi;
    //[SerializeField]
    //private GameObject leftController;
    [SerializeField]
    private Transform cameraTransform;


    private bool uiActive = false;

    // Update is called once per frame
    void Update()
    {
        WatchUiActive();
    }

    private void WatchUiActive()
    {
        // 왼손 회전 감지
        Quaternion leftHandRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

        // 왼손 위치 감지

        //Vector3 leftHandForward = leftHandRotation * Vector3.forward;
        Vector3 cameraForward = cameraTransform.forward;

        watchUi.transform.position = cameraTransform.position + cameraForward * 1f;
        watchUi.transform.rotation = Quaternion.LookRotation(cameraForward);

        //watchUi.transform.position = leftController.transform.position + new Vector3(0, 0.5f, 0.3f);
        //watchUi.transform.rotation = Quaternion.Euler(watchUi.transform.rotation.x + leftController.transform.rotation.x, watchUi.transform.rotation.y + leftController.transform.rotation.y, watchUi.transform.rotation.z + leftController.transform.rotation.z);


        //Debug.Log(leftHandRotation.eulerAngles.z);
        //Debug.Log(leftHandForward);
        // 손목을 돌렸을 때 UI 활성화
        //&& leftHandForward.x > 0.65f && leftHandForward.x < 1f 
        if (leftHandRotation.eulerAngles.z > 200f && leftHandRotation.eulerAngles.z < 300f && !uiActive)
        {
            watchUi.SetActive(true);
            uiActive = true;
        }

        // 손목을 원래 위치로 되돌렸을 때 UI 비활성화
        // || (leftHandForward.x <= 0.65f || leftHandForward.x >= 1f) 
        if ((leftHandRotation.eulerAngles.z <= 200f || leftHandRotation.eulerAngles.z >= 300f) && uiActive)
        {
            watchUi.SetActive(false);
            uiActive = false;
        }
    }
}
