using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullero : EnemigoPadre
{
    [Header("Patrullaje")]
    public Transform[] puntosPatrulla;
    private int indiceActual = 0;
    private bool patrullando = true;

    [Header("Persecución")]
    public float rangoVision = 5f;
    private bool persiguiendo = false;

    protected override void Start()
    {
        base.Start();

        if (puntosPatrulla.Length == 0)
        {
            Debug.LogWarning($"{gameObject.name} no tiene puntos de patrulla asignados.");
            patrullando = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (jugador == null) return;

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= rangoAtaque)
        {
            rb.velocity = Vector2.zero;
            Atacar();
            return;
        }

        if (distanciaAlJugador <= rangoVision)
        {
            PersigueJugador();
            persiguiendo = true;
        }
        else
        {
            if (persiguiendo)
            {
                persiguiendo = false;
                indiceActual = 0;
            }

            if (patrullando)
                Patrullar();
        }
    }

    void Patrullar()
    {
        if (puntosPatrulla.Length == 0) return;

        Transform objetivo = puntosPatrulla[indiceActual];
        Vector2 direccion = (objetivo.position - transform.position).normalized;
        rb.velocity = direccion * velocidad;

        if (direccion.x != 0 && sr != null)
        {
            sr.flipX = direccion.x < 0;
        }

        if (Vector2.Distance(transform.position, objetivo.position) < 0.2f)
        {
            indiceActual = (indiceActual + 1) % puntosPatrulla.Length;
        }

        if (anim != null)
            anim.Play("patrullero");
    }

    void PersigueJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = direccion * velocidad;

        if (direccion.x != 0 && sr != null)
        {
            sr.flipX = direccion.x < 0;
        }

        if (anim != null)
            anim.Play("patrullero");
    }
}

