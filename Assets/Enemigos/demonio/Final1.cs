using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Final1 : Enemigo
{
    protected override void Start()
    {
        base.Start(); // Muy importante para que busque al jugador y se registre en EnemyManager

        vida *= 2;
        velocidad *= 2f;
        detectionRange *= 2f;
        daño *= 2;
    }

    public override void Atacar()
    {
        base.Atacar(); // Llama al ataque original

        // Podés agregar efectos especiales acá
        Debug.Log("🔥 Final1 lanza un super ataque de " + daño);
    }

    public override void Mover()
    {
        velocidad *= 0.8f; // Se mueve un poco más lento
        base.Mover();
    }
     protected override void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        FlipSprite(direccion); // 👈 Esto es obligatorio si NO llamás a base.MoverHaciaJugador()
    }
}
*/