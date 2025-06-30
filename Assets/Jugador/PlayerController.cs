using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int movimientoHorizontal;
    private int movimientoVertical;
    private Vector2 mov;
    bool giroIzq;

    // --- VIDA ---
    [SerializeField] private int vidaMaxima = 5;
    private int vidaActual;

    // --- MOVIMIENTO ---
    [SerializeField] private float speedInicial = 5f;
    [SerializeField] private float multiplicadorSprint = 1.5f;
    private float speedActual;
    private Rigidbody2D rb;

    // --- ANIMACIÓN ---
    private Animator animator;

    [Header("Ataque")]
[SerializeField] private float rangoAtaque = 1f;
[SerializeField] private int danioAtaque = 1;
[SerializeField] private LayerMask capaEnemigos;
[SerializeField] private Transform puntoAtaque;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Guardamos la velocidad base
        speedActual = speedInicial;
        vidaActual = vidaMaxima;
    }

    void Update()
    {
        Moverse();
        FlipSprite();
        Attack();
    }

    void FixedUpdate()
    {
        // Movimiento del personaje con física
        rb.velocity = mov * speedActual * Time.fixedDeltaTime;

        // Actualizamos animación según movimiento
        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            animator.SetFloat("xVelocity", 1);
        }
        else
        {
            animator.SetFloat("xVelocity", 0);
        }
    }

    void Moverse()
    {
        // Sprint con LeftShift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedActual = speedInicial * multiplicadorSprint;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedActual = speedInicial;
        }

        // Movimiento horizontal
        if (Input.GetKey(KeyCode.D))
        {
            movimientoHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movimientoHorizontal = -1;
        }
        else
        {
            movimientoHorizontal = 0;
        }

        // Movimiento vertical
        if (Input.GetKey(KeyCode.W))
        {
            movimientoVertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movimientoVertical = -1;
        }
        else
        {
            movimientoVertical = 0;
        }

        // Vector de movimiento normalizado
        mov = new Vector2(movimientoHorizontal, movimientoVertical).normalized;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
            RealizarAtaque();
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        Debug.Log("Vida actual: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("¡Jugador muerto!");
        gameObject.SetActive(false);
    }

    private void FlipSprite()
    {
        if (rb.velocity.x < 0 && !giroIzq)
        {
            giroIzq = true;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
        else if (rb.velocity.x > 0 && giroIzq)
        {
            giroIzq = false;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void RealizarAtaque()
{
    // Detecta todos los colliders en el área de ataque
    Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, capaEnemigos);

    foreach (Collider2D enemigo in enemigosGolpeados)
    {
        // Suponemos que tiene un script con método RecibirDanio
        enemigo.GetComponent<Enemigo>()?.RecibirDanio(danioAtaque);
    }
}

void OnDrawGizmosSelected()
{
    if (puntoAtaque == null) return;
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
}

}



