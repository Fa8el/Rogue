using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSlime : Enemigo
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start(); 
        vida = 100;
        velocidad = 2f;

        detectionRange = 5f;
        daño = 5;
    }

    

     public override void Atacar()
    {
        Debug.Log("Enemigo básico ataca con daño " + daño);
    }
}
