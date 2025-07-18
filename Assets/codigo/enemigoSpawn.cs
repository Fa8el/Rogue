using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoSpawn : EnemigoPadre    
{
     [Header("Persecución")]
    public float rangoVision = 7f;

    protected override void Update()
    {
        base.Update();

        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque)
        {
            rb.velocity = Vector2.zero;
            Atacar();
        }
        else if (distancia <= rangoVision)
        {
            PerseguirJugador();
        }
        else
        {
            // Detener si no ve al jugador
            rb.velocity = Vector2.zero;
            if (anim != null)
                anim.Play("spawn mov");
        }
    }

    void PerseguirJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = direccion * velocidad;

        // Flip
        if (direccion.x != 0 && sr != null)
            sr.flipX = direccion.x < 0;

        if (anim != null)
            anim.Play("spawnentry"); // o animación de movimiento
    }

}