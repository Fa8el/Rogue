using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSlime : Enemigo
{
    protected override void Start()
    {
        base.Start();
        vida = 100;
        velocidad = 2f;
        detectionRange = 5f;
        daño = 5;
        tiempoEntreAtaques = 1.5f; // 🔁 tiempo entre ataques (personalizado para slime)
    }

    public override void Atacar()
    {
        if (!PuedeAtacar()) return;
        RegistrarAtaque();

        if (animator != null)
            animator.SetTrigger("ataca"); // Trigger definido en Animator

        if (jugador != null)
        {
            PlayerController playerScript = jugador.GetComponent<PlayerController>();
            if (playerScript != null && !playerScript.estaMuerto)
            {
                playerScript.RecibirDanio(daño, transform.position);

                Debug.Log("Jugador recibió daño: " + daño);
            }
        }

        Debug.Log("💥 Estoy atacando");
        Debug.Log("Enemigo básico ataca con daño " + daño);
    }

    protected override void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        FlipSprite(direccion);
    }
}