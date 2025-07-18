using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaraVida : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public TextMeshProUGUI textoVida;
    public PlayerController jugador;

    void Start()
    {
        if (jugador == null)
        {
            jugador = FindObjectOfType<PlayerController>();
        }

        slider.maxValue = jugador.VidaMaxima;
        ActualizarBarra();
    }

    void Update()
    {
        if (jugador != null)
        {
            ActualizarBarra();
        }
    }

    void ActualizarBarra()
    {
        slider.value = jugador.VidaActual;
        textoVida.text = $"{jugador.VidaActual} / {jugador.VidaMaxima}";
    }
}



