using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSlime : EnemigoPadre
{
    public bool estaMuerto = false;

    protected override void Start()
    {
        base.Start();
        vida = 100;
        vidaMaxima = 100;
        velocidad = 2f;
        rangoAtaque = 1.2f;
        cooldownAtaque = 1.5f;
        daño = 5;
    }

    protected override void Update()
    {
        if (estaMuerto) return;
        base.Update();
    }

    protected override void Atacar()
    {
        if (Time.time - tiempoUltimoAtaque < cooldownAtaque) return;
        tiempoUltimoAtaque = Time.time;

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

    protected override void MoverseHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
    }

    protected override void Morir()
    {
        if (estaMuerto) return;

        estaMuerto = true;

        Debug.Log("🟢 Slime murió");

        Destroy(gameObject);
    }

    public override void RecibirDanio(int cantidad)
    {
        if (estaMuerto) return;

        Debug.Log($"{gameObject.name} recibió {cantidad} de daño.");
        base.RecibirDanio(cantidad);
    }
}


