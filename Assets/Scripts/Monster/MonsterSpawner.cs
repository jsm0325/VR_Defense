using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject spawner;  // 몬스터 스폰 위치
    public Transform target;    // 목표 지점
    private int spawnersLength; // 스폰 장소 개수
    private Wave currentWave;   // 현재 웨이브 단계 정보

    private void Start()
    {
        spawnersLength = spawner.transform.childCount;
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave; // 웨이브 정보 저장
        StartCoroutine("SpawnMonster"); // 웨이브 시작
    }

    private IEnumerator SpawnMonster()
    {
        int spawnMonsterCount = 0; // 현재 웨이브 생성 몬스터 숫자

        while( spawnMonsterCount < currentWave.maxMonsterCount)
        {
            int monsterIndex = Random.Range(0, currentWave.monsterPrefabs.Length);
            int randomPosition = Random.Range(0, spawnersLength);
            // 몬스터 프리팹을 스폰 위치에 생성
            GameObject spawnedMonster = Instantiate(currentWave.monsterPrefabs[monsterIndex], spawner.transform.GetChild(randomPosition).transform.position, spawner.transform.GetChild(randomPosition).transform.rotation);
            spawnMonsterCount++;
            // 스폰된 몬스터에게 목표 지점 정보를 전달할 수 있도록 설정
            MonsterMovement monsterMovement = spawnedMonster.GetComponent<MonsterMovement>();
            if (monsterMovement != null)
            {
                monsterMovement.target = target;
            }
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }
}

