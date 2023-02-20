using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public int maxEnemies = 1; 
    public float respawnTime; 
    private List<GameObject> enemies = new List<GameObject>();  
    void Start()
    {
        respawnTime = MenuManager.instance.sliderRespawn.value;
        for (int i = 0; i < maxEnemies; i++)
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemies.Add(enemy);
    }

    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                StartCoroutine(RespawnEnemy(respawnTime));
                enemies.Remove(enemy);
                break;
            }
        }
    }

    IEnumerator RespawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        CreateEnemy();
    }
}