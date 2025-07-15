using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int maxEnemigos = 5;

    private float spawnTimer;
    private List<GameObject> enemigosVivos = new List<GameObject>();

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Limpiar enemigos destruidos
        enemigosVivos.RemoveAll(e => e == null);

        if (spawnTimer >= spawnInterval && enemigosVivos.Count < maxEnemigos)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject nuevoEnemigo = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemigosVivos.Add(nuevoEnemigo);
    }
}

