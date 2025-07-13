using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENIMIGOFALOPA : EnemigoPadre
{
    
    protected override void Start()
    {
        base.Start();
        vida = 100;
        velocidad = 1f;
        daño = 1;
    }
}