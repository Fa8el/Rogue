using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitaVida : Agarrable
{
     [SerializeField] private int cantidadVida = 20;

    public override void OnPick()
{
    base.OnPick();

    PlayerController jugador = FindObjectOfType<PlayerController>();
    if (jugador != null)
    {
        int nuevaVida = Mathf.Min(jugador.VidaActual - cantidadVida, jugador.VidaMaxima);
        jugador.vidaActual = nuevaVida;
         Debug.Log($"¡Agarraste un objeto que TE MATAda en {cantidadVida} puntos!");
        Debug.Log($"Vida actual: {jugador.VidaActual}");
        Debug.Log($"Vida aumentada en {cantidadVida}. Vida actual: {jugador.VidaActual}");
    }
    else
    {
        Debug.LogWarning("No se encontró PlayerController para aumentar vida.");
    }
}
}
