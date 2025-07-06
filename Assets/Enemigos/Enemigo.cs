using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 100;
    public float velocidad = 2f;
    public float detectionRange = 5f;
    public int daño = 10;
    private bool giroIzq = false;

    protected Transform jugador;
    protected Animator animator;
    private EnemyManager enemyManager;
    public bool esDeOleada = false;

    // Cooldown de ataque
    protected float tiempoEntreAtaques = 1.5f;
    private float ultimoAtaque = -999f;

    protected virtual void Start()
    {
        jugador = GameObject.FindWithTag("Player")?.transform;
        animator = GetComponent<Animator>();

        if (jugador == null)
        {
            Debug.LogError("❌ No se encontró al jugador.");
        }
        else
        {
            Debug.Log("✅ Jugador encontrado: " + jugador.name);
        }

        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager?.RegistrarEnemigo();
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (esDeOleada || distancia < detectionRange)
        {
            // 🚫 NO se mueve si está lo suficientemente cerca (no empuja)
            if (distancia > 1.2f)
            {
                MoverHaciaJugador();
            }
            else
            {
                Atacar();
            }
        }
    }

    protected virtual void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // ✅ Movimiento suave que respeta la distancia sin empujar
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

        FlipSprite(direccion);
    }

    public virtual void Atacar()
    {
        if (!PuedeAtacar()) return;
        RegistrarAtaque();

        Debug.Log($"💥 [{gameObject.name}] Atacando con daño {daño}");

        if (jugador != null)
        {
            PlayerController playerScript = jugador.GetComponent<PlayerController>();
            if (playerScript != null && !playerScript.estaMuerto)
            {
                playerScript.RecibirDanio(daño, transform.position);
                Debug.Log($"Jugador recibió daño: {daño} (vida: {playerScript.VidaActual})");
            }
            else
            {
                Debug.LogWarning("No se encontró PlayerController en el jugador o está muerto");
            }
        }
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

    protected void FlipSprite(Vector3 direction)
    {
        if (direction.x < 0 && !giroIzq)
        {
            giroIzq = true;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
        else if (direction.x > 0 && giroIzq)
        {
            giroIzq = false;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    // Cooldown helpers
    protected bool PuedeAtacar()
    {
        float tiempoDesdeUltimo = Time.time - ultimoAtaque;
        Debug.Log($"⏱️ [{gameObject.name}] Tiempo desde último ataque: {tiempoDesdeUltimo:F2} / Necesario: {tiempoEntreAtaques}");
        return tiempoDesdeUltimo >= tiempoEntreAtaques;
    }

    protected void RegistrarAtaque()
    {
        ultimoAtaque = Time.time;
    }
}
