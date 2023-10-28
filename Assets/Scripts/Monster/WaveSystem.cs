using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private MonsterSpawner monsterSpawner;
    [SerializeField]

    private bool isStarted = false;
    public void StartWave()
    {
        if (isStarted == false)
        {
            int currentWaveIndex = GameManager.gameManager.GetCurrentWave() - 1; // 웨이브 단계
            monsterSpawner.StartWave(waves[currentWaveIndex]);
            isStarted = true;
        }
        
    }
    // 다음 스테이지 도착하면 작동하도록 해야함
    public void ClearWave()
    {
        isStarted = false;
    }
}



[System.Serializable]
public struct Wave
{
    public float spawnTime;                    // 웨이브 적 생성 주기
    public int maxMonsterCount;                // 웨이브 적 등장 숫자
    public GameObject[] monsterPrefabs;        // 적 등장 종류
}