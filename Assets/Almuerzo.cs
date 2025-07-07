using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almuerzo : Agarrable
{
    // Start is called before the first frame update
    private static int contadorAlmuerzos = 0;

    public override void OnPick()
    {
        base.OnPick();

        contadorAlmuerzos++;

        if (contadorAlmuerzos >= 3)
        {
            Debug.Log("¡Ya tienes el almuerzo del general!");
            // Si querés, podés resetear el contador para empezar de nuevo
            // contadorAlmuerzos = 0;
        }
    }
}
