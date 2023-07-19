using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchUI : MonoBehaviour
{
    [SerializeField]
    private GameObject watchUi;
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

        Vector3 leftHandForward = leftHandRotation * Vector3.forward;

        //Debug.Log(leftHandRotation.eulerAngles.z);
        Debug.Log(leftHandForward);
        // 손목을 돌렸을 때 UI 활성화
        if (leftHandRotation.eulerAngles.z > 240f && leftHandRotation.eulerAngles.z < 275f && leftHandForward.x > 0.65f && leftHandForward.x < 1f && !uiActive)
        {
            watchUi.SetActive(true);
            uiActive = true;
        }

        // 손목을 원래 위치로 되돌렸을 때 UI 비활성화
        if ((leftHandRotation.eulerAngles.z <= 240f || leftHandRotation.eulerAngles.z >= 275f) || (leftHandForward.x <= 0.65f || leftHandForward.x >= 1f) && uiActive)
        {
            watchUi.SetActive(false);
            uiActive = false;
        }
    }
}
