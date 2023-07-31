using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private OVRPlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<OVRPlayerController>();
    }

    private void Update()
    {
        if(OVRInput.Get(OVRInput.RawButton.X))
        {
            playerController.Jump();
        }
    }
}
