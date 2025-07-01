using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 100;
    public float velocidad = 2f;
    public float detectionRange = 5f;
    public int daño = 10;

    protected Transform jugador;
    private EnemyManager enemyManager;
    public bool esDeOleada = false;  // <-- Variable para marcar si es enemigo de oleada

    protected virtual void Start()
    {
        jugador = GameObject.FindWithTag("Player")?.transform;

        if (jugador == null)
        {
            Debug.LogError("❌ No se encontró al jugador.");
        }
        else
        {
            Debug.Log("✅ Jugador encontrado: " + jugador.name);
        }

        // Buscar y registrar en el EnemyManager
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager?.RegistrarEnemigo();
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Si es de oleada, persigue siempre; si no, solo dentro del rango detectionRange
        if (esDeOleada || distancia < detectionRange)
        {
            MoverHaciaJugador();

            if (distancia < 1.2f)
            {
                Atacar();
            }
        }
    }

    protected virtual void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
    }

    public virtual void Mover()
    {
        Debug.Log("El enemigo se mueve a velocidad " + velocidad);
    }

    public virtual void Atacar()
    {
        Debug.Log("El enemigo ataca con daño " + daño);
    }

    public void RecibirDanio(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("⚔️ Enemigo recibió daño. Vida actual: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("💀 ¡Enemigo destruido!");
        enemyManager?.EliminarEnemigo();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}




