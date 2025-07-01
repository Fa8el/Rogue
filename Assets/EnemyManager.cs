using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public GameObject jefePrefab;
    public Transform[] spawnPoints;

    public int enemigosEnOleada = 10;        // Total de enemigos normales en la oleada
    public float duracionOleada = 30f;       // Tiempo que dura la oleada en segundos
    public float tiempoEntreSpawn = 2f;      // Tiempo entre cada aparición de enemigo
    public float tiempoEntreOleadas = 15f;   // Tiempo de espera después de la oleada

    private int enemigosGenerados = 0;
    private bool oleadaActiva = false;
    private bool jefeSpawned = false;

    private int enemigosVivos = 0;

    void Start()
    {
        // No arrancamos oleada automática
        Debug.Log("Esperando que elimines enemigos iniciales...");
    }

    void Update()
    {
        // Solo iniciamos oleada si no hay enemigos vivos y no hay oleada activa
        if (enemigosVivos <= 0 && !oleadaActiva)
        {
            StartCoroutine(CicloOleadas());
        }
    }

    IEnumerator CicloOleadas()
    {
        oleadaActiva = true;
        jefeSpawned = false;
        enemigosGenerados = 0;

        Debug.Log("🟢 Iniciando oleada");

        float tiempoInicio = Time.time;

        // Mientras dure la oleada y no se hayan generado todos los enemigos
        while (Time.time < tiempoInicio + duracionOleada && enemigosGenerados < enemigosEnOleada)
        {
            SpawnEnemigo();
            enemigosGenerados++;
            yield return new WaitForSeconds(tiempoEntreSpawn);
        }

        // Esperamos que el jugador elimine todos los enemigos antes de continuar
        Debug.Log("Esperando que elimines todos los enemigos de la oleada...");

        // Esperar a que mueran todos los enemigos de la oleada (incluido jefe si ya spawneó)
        while (enemigosVivos > 0)
        {
            yield return null;
        }

        // Spawn jefe después de eliminar todos enemigos normales
        if (!jefeSpawned)
        {
            SpawnJefe();
            jefeSpawned = true;
        }

        // Esperar que el jugador elimine al jefe
        Debug.Log("Esperando que elimines al jefe...");

        while (enemigosVivos > 0)
        {
            yield return null;
        }

        Debug.Log("🔴 Oleada terminada. Esperando para la siguiente...");
        yield return new WaitForSeconds(tiempoEntreOleadas);

        oleadaActiva = false;
    }

    void SpawnEnemigo()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject nuevo = Instantiate(enemigoPrefab, spawn.position, Quaternion.identity);

        Enemigo enemigo = nuevo.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.esDeOleada = true;
        }

        RegistrarEnemigo();

        Debug.Log("👾 Enemigo spawnado");
    }

    void SpawnJefe()
    {
        if (jefePrefab == null)
        {
            Debug.LogWarning("⚠️ jefePrefab no asignado. No se puede spawnear jefe.");
            return;
        }

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject jefe = Instantiate(jefePrefab, spawn.position, Quaternion.identity);

        Enemigo enemigo = jefe.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.esDeOleada = true;
            enemigo.vida = 500;
            enemigo.daño = 50;
            enemigo.velocidad = 1f;
        }

        RegistrarEnemigo();

        Debug.Log("👑 Jefe spawnado");
    }

    public void RegistrarEnemigo()
    {
        enemigosVivos++;
        Debug.Log("Enemigos vivos: " + enemigosVivos);
    }

    public void EliminarEnemigo()
    {
        enemigosVivos--;
        Debug.Log("Enemigos vivos: " + enemigosVivos);
    }
}



