using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarrable : MonoBehaviour
{
    [SerializeField] protected string itemName = "Item";
    [SerializeField] protected int quantity = 1;
    public virtual void OnPick()
    {
        Debug.Log($"Picked up {quantity} {itemName}(s)");
        Destroy(gameObject);
    }

    // Detectar cuando el jugador pasa por encima (trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Colisionó con {other.name} y tag {other.tag}");
        if (other.CompareTag("Player"))
        {
            OnPick();
        }
    }
}
