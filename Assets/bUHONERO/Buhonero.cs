using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buhonero : MonoBehaviour
{
    public Transform target;            // El objetivo
    public float speed = 2f;            // Velocidad del NPC
    public float detectionRange = 10f;  // Rango máximo para empezar a acercarse
    public float stopDistance = 1.5f;   // Hasta dónde se acerca antes de irse

    private bool seVa = false;
    private bool yaHablo = false;
    private bool giroIzq = false; // Para controlar flip del sprite

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 direction = Vector3.zero;

        if (!seVa && distance < detectionRange && distance > stopDistance)
        {
            // Se acerca al objetivo
            direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (!seVa && distance <= stopDistance)
        {
            // Cuando llega cerca, habla y activa la retirada
            Debug.Log("Campesino, ve a buscar la comida al general!!! pero cuidado criaturas sin nombre asechan siempre hambrientas estas tierras");
            seVa = true;
        }

        if (seVa)
        {
            // Se aleja en dirección contraria al objetivo
            direction = (transform.position - target.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (direction != Vector3.zero)
        {
            FlipSprite(direction);
        }
    }

    private void FlipSprite(Vector3 direction)
    {
        // Si se mueve hacia la izquierda y no está girado, gira el sprite
        if (direction.x < 0 && !giroIzq)
        {
            giroIzq = true;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
        // Si se mueve hacia la derecha y está girado, vuelve a la normalidad
        else if (direction.x > 0 && giroIzq)
        {
            giroIzq = false;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de detección (rojo) y el punto de parada (verde)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}


