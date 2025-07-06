using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSegui : MonoBehaviour
{
     public Transform jugador;

    void LateUpdate()
    {
        if (jugador != null)
        {
            Vector3 nuevaPos = jugador.position;
            nuevaPos.z = -10f; // aseguramos que la cámara esté detrás
            transform.position = nuevaPos;
            Debug.Log("Cámara en: " + transform.position + " | Jugador en: " + jugador.position);
        }
    }
}
