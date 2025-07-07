using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSlime : Enemigo
{
    public bool estaMuerto = false;

    protected override void Start()
    {
        base.Start();
        vida = 100;
        velocidad = 2f;
        detectionRange = 5f;
        daño = 5;
        tiempoEntreAtaques = 1.5f;
    }

    public override void Atacar()
    {
        if (!PuedeAtacar()) return;
        RegistrarAtaque();

        if (animator != null)
            animator.SetTrigger("ataca");

        if (jugador != null)
        {
            PlayerController playerScript = jugador.GetComponent<PlayerController>();
            if (playerScript != null && !playerScript.estaMuerto)
            {
                playerScript.RecibirDanio(daño, transform.position);
            }
        }

        Debug.Log("💥 Slime ataca con daño " + daño);
    }

    protected override void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        FlipSprite(direccion);
    }

    protected override void Morir()
    {
        if (estaMuerto) return;

        estaMuerto = true;

        Debug.Log("🟢 Slime murió");
        Debug.Log("🟩 Morir() slime");

        if (animator != null)
            animator.SetBool("estaMuerto", true);

        Invoke("DesactivarSlime", 1f); // espera a que termine la animación
    }

    private void DesactivarSlime()
    {
        Destroy(gameObject);
    }
}
