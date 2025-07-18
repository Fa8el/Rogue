using UnityEngine;
using TMPro;
using System.Collections;

public class IntroMensaje : MonoBehaviour
{
    public GameObject panelDialogo;          // El panel que contiene el texto y fondo
    public TextMeshProUGUI textoUI;
    public float velocidadEscritura = 0.05f;
    public float duracionFinal = 2f;

    private string mensajeCompleto =
        "Campesino, ve a buscar la comida al general!!!\n" +
        "Pero cuidado, criaturas sin nombre acechan estas tierras...";

    void Start()
    {
        textoUI.text = "";
        textoUI.color = new Color32(178, 34, 34, 255); // Firebrick, rojo medieval oscuro
        panelDialogo.SetActive(true);       // Mostrar el panel al inicio
        StartCoroutine(EscribirTexto());
    }

    IEnumerator EscribirTexto()
    {
        foreach (char letra in mensajeCompleto)
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        yield return new WaitForSeconds(duracionFinal);

        panelDialogo.SetActive(false);      // Oculta TODO el panel para liberar la pantalla
    }
}



