using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay, spawnCountDown, spawnDelayLow, spawnDelayHigh;
    [SerializeField] private List<GameObject> spawnAreas;
    private EnemySpawnArea esa;
    private GameObject newEnemy;
    // Start is called before the first frame update
    void Start()
    {
        spawnCountDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.Instance.IsDead) return;
        spawnDelay = EnemyPoolManager.Instance.EnemyCount > 50? spawnDelayLow : spawnDelayHigh;
        if (spawnCountDown >= spawnDelay) 
        {
            spawnCountDown -= spawnDelay;
            foreach (GameObject sa in spawnAreas)
            {
                esa = sa.GetComponent<EnemySpawnArea>();
                if (esa != null && !esa.IsSeen && UnityEngine.Random.Range(0, 2) == 0)
                {
                    newEnemy = EnemyPoolManager.Instance.GetEnemy();
                    if (newEnemy != null)
                    {
                        newEnemy.transform.position = sa.transform.position;
                        newEnemy.GetComponent<Enemy>().Rise();
                    }
                }
                else if (esa == null)
                {
                    Debug.LogError("spawn area not fond in: " +  sa.name);
                }
            }
        }
        else
        {
            spawnCountDown += Time.deltaTime;
        }
    }
}
