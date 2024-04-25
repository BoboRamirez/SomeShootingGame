using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;
    public GameObject enemyPrefab, enemyBodyPrefab;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private Queue<GameObject> enemyBodyPool = new Queue<GameObject>();
    private int killCount, enemyCount;
    public int KillCount
    { get { return killCount; } }
    public int EnemyCount
    {        get {  return enemyCount; }    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        InitializeEnemies(100);
        killCount = 0;
        enemyCount = 0;
    }

    void InitializeEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            GameObject enemyBody = Instantiate(enemyBodyPrefab);
            enemy.SetActive(false);
            enemyBody.SetActive(false);
            enemyPool.Enqueue(enemy);
            enemyBodyPool.Enqueue(enemyBody);
        }
    }

    public GameObject GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            enemyCount++;
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            PlayerMove.Instance.Die();
            return null;
        }
    }
    public GameObject GetEnemyBody()
    {
        GameManager.Instance.killCount++;
        if (enemyBodyPool.Count > 0)
        {
            GameObject body = enemyBodyPool.Dequeue();
            body.SetActive(true);
            return body;
        }
        else
        {
            GameObject body = Instantiate(enemyBodyPrefab);
            body.SetActive(true);
            return body;
        }
    }
    public void ReturnEnemy(GameObject enemy)
    {
        killCount++;
        enemyCount--;
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
    public void ReturnBody(GameObject body)
    {
        body.SetActive(false);
        enemyPool.Enqueue(body);
    }
}
