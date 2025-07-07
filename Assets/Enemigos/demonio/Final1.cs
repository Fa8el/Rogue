using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final1 : Enemigo
{
    protected override void Start()
    {
        base.Start();

        vida *= 2;
        velocidad *= 2f * 0.8f; // Se mueve un poco más lento desde el inicio
        detectionRange *= 2f;
        daño *= 2;
    }

    public override void Atacar()
    {
        base.Atacar();
        Debug.Log("🔥 Final1 lanza un super ataque de " + daño);
    }

   
    protected override void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        FlipSprite(direccion);
    }
}
