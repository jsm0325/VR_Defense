using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.gameManager.ChangeWeaponSwapEnabled();
            GameManager.gameManager.PlusCurrentWave();
            SceneManager.LoadScene(sceneName);
        }
    }
}
