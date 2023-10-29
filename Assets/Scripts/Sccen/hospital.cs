using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class hospital : MonoBehaviour
{
    private void Start()
    {
        Invoke("moveScenne", 10);
    }

   private void moveScenne()
    {
        SceneManager.LoadScene("Player_Room");
    }
}
