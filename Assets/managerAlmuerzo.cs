using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class managerAlmuerzo : MonoBehaviour
{
    public static managerAlmuerzo instancia;
    private int contador = 0;
    public GameObject panelVictoria;

    private void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);

        if (panelVictoria != null)
            panelVictoria.SetActive(false);  // Desactiva panel al iniciar
    }

    public void SumarAlmuerzo()
    {
        contador++;
        Debug.Log($"🍗 Almuerzos recogidos: {contador}");

        if (contador >= 3)
        {
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);   // Muestra el panel
                Time.timeScale = 0f;             // Pausa la escena
            }

            // Usa StartCoroutine en la instancia, pero con WaitForSecondsRealtime para que corra aunque el juego esté pausado
            instancia.StartCoroutine(EsperarYCambiarEscena());
        }
    }

    private IEnumerator EsperarYCambiarEscena()
    {
        // Espera 3 segundos en tiempo real, no afectado por timeScale
        yield return new WaitForSecondsRealtime(3f);

        // Antes de cambiar de escena, reanuda el tiempo para evitar problemas
        Time.timeScale = 1f;

        SceneManager.LoadScene("EscenaFinal");
    }
}


