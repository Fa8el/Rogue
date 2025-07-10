using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoClick : MonoBehaviour
{
     public AudioSource audioSource;

    public void ReproducirSonido()
    {
        audioSource.Play();
    }
}
