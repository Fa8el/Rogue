using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ESTA LÍNEA ES NECESARIA

public class controladorMenu : MonoBehaviour
{
     public AudioSource sonidoBoton;
    public float retardo = 11f;

    // Cambiá esto por el nombre exacto de tu escena principal
    public string nombreEscenaJuego = "SampleScene";

    public void Jugar()
    {
        StartCoroutine(CambiarEscenaDespuesDeSonido(nombreEscenaJuego));
    }

    public void Salir()
    {
        StartCoroutine(SalirDespuesDeSonido());
    }

    IEnumerator CambiarEscenaDespuesDeSonido(string nombreEscena)
    {
        if (sonidoBoton != null)
            sonidoBoton.Play();

        yield return new WaitForSeconds(retardo);
        SceneManager.LoadScene(nombreEscena);
    }

    IEnumerator SalirDespuesDeSonido()
    {
        if (sonidoBoton != null)
            sonidoBoton.Play();

        yield return new WaitForSeconds(retardo);
        Application.Quit();
        Debug.Log("Salir del juego"); // Esto solo se ve en build
    }
}

