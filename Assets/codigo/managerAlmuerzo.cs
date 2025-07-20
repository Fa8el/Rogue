using UnityEngine;

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
            panelVictoria.SetActive(false);  // Oculta al inicio
    }

    public void SumarAlmuerzo()
    {
        contador++;
        Debug.Log($"🍗 Almuerzos recogidos: {contador}");

        if (contador >= 3)
        {
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true); // Muestra el panel
                Time.timeScale = 0f;           // Pausa TODO el juego
            }
        }
    }
}


