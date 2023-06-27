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
    private int currentWaveIndex; // 웨이브 단계


    public void StartWave()
    {
        monsterSpawner.StartWave(waves[currentWaveIndex]);
    }
}


[System.Serializable]
public struct Wave
{
    public float spawnTime;                    // 웨이브 적 생성 주기
    public int maxMonsterCount;                // 웨이브 적 등장 숫자
    public GameObject[] monsterPrefabs;        // 적 등장 종류
}