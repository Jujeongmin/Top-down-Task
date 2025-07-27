using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : SingletonBehaviour<MonsterSpawnManager>
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private Queue<GameObject> monsterPool = new();
    private List<MonsterInfo> allMonsterInfos = new();

    private const int PoolMonster = 10;

    private void Start()
    {
        var loader = DataManager.Instance.GetLoader<MonsterInfo, string>();
        allMonsterInfos = loader.DataList;

        int countPerMonster = Mathf.Max(1, PoolMonster / allMonsterInfos.Count);

        foreach (var monsterInfo in allMonsterInfos)
        {
            PreLoadMonsters(monsterInfo, countPerMonster);
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            //SpawnRandomMonster(i);
            SpawnMonster("M0001", i);
        }
    }

    private void PreLoadMonsters(MonsterInfo monsterInfo, int count)
    {
        string monsterID = monsterInfo.MonsterID;

        for (int i = 0; i < count; i++)
        {
            GameObject monsterGO = Instantiate(monsterPrefab);
            Monster monster = monsterGO.GetComponent<Monster>();
            monster.Init(monsterInfo, monsterInfo.MonsterID);
            monsterGO.SetActive(false);
            monsterPool.Enqueue(monsterGO);
        }
    }

    public void SpawnMonster(string monsterID, int spawnIndex = 0)
    {
        if (spawnIndex < 0 || spawnIndex >= spawnPoints.Length) return;

        if (monsterPool.Count == 0) return;

        GameObject monsterGO = monsterPool.Dequeue();
        Monster monster = monsterGO.GetComponent<Monster>();
        monster.Init(allMonsterInfos.Find(m => m.MonsterID == monsterID), monsterID);
        monsterGO.transform.position = spawnPoints[spawnIndex].position;
        monster.ResetMonster();
        monsterGO.SetActive(true);
    }

    public void SpawnRandomMonster(int spawnIndex = 0)
    {
        if (allMonsterInfos.Count == 0) return;

        string randomID = allMonsterInfos[Random.Range(0, allMonsterInfos.Count)].MonsterID;
        SpawnMonster(randomID, spawnIndex);
    }

    public void ReturnToPool(GameObject monsterGO)
    {
        monsterGO.SetActive(false);

        if (monsterPool.Count < PoolMonster)
        {
            monsterPool.Enqueue(monsterGO);
        }
        else
        {
            Destroy(monsterGO);
        }

        StartCoroutine(RespawnAfterDelay(2f, Random.Range(0, spawnPoints.Length)));
    }

    private IEnumerator RespawnAfterDelay(float delay, int spawnIndex)
    {
        yield return new WaitForSeconds(delay);
        //SpawnRandomMonster(spawnIndex);
        SpawnMonster("M0001", spawnIndex);
    }
}
