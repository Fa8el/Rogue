using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int movimientoHorizontal;
    private int movimientoVertical;
    private Vector2 mov;
    bool giroIzq;
    public bool estaMuerto = false;
    [SerializeField] private float fuerzaRebote = 5f;  // Fuerza del rebote

    private bool enRebote = false;
    private float duracionRebote = 0.3f;
    private float tiempoInicioRebote;

    // --- VIDA ---
   [SerializeField] private int vidaMaxima = 5;
public int VidaMaxima => vidaMaxima;

public int vidaActual;
public int VidaActual => vidaActual;
    // --- MOVIMIENTO ---
    [SerializeField] private float speedInicial = 3f;          // Antes estaba en 5f, más lento
[SerializeField] private float multiplicadorSprint = 1.3f;
    private float speedActual;
    private Rigidbody2D rb;

    // --- ANIMACIÓN ---
    private Animator animator;

    [Header("Ataque")]
    [SerializeField] private float rangoAtaque = 1f;
    [SerializeField] private int danioAtaque = 20;
    [SerializeField] private LayerMask capaEnemigos;
    [SerializeField] private Transform puntoAtaque;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        speedActual = speedInicial;
        vidaActual = vidaMaxima;
    }

    void Update()
    {
        if (estaMuerto) return;

        if (!enRebote)
        {
            Moverse();
            FlipSprite();
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (estaMuerto) return;

        if (enRebote)
        {
            if (Time.time - tiempoInicioRebote > duracionRebote)
            {
                enRebote = false; // Fin del rebote
            }
            else
            {
                return; // No hacer nada mientras dure el rebote
            }
        }

        rb.velocity = mov * speedActual;

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedActual = speedInicial * multiplicadorSprint;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedActual = speedInicial;
        }

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

    public void RecibirDanio(int cantidad, Vector2 posicionAtacante)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        Debug.Log("Vida actual: " + vidaActual);

        // Calcular dirección de rebote: del atacante al jugador
        Vector2 direccionRebote = (transform.position - (Vector3)posicionAtacante).normalized;

        AplicarRebote(direccionRebote);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
{
    if (estaMuerto) return;

    estaMuerto = true;

    Debug.Log("¡Jugador muerto!");

    rb.velocity = Vector2.zero;
    animator.SetBool("estaMuerto", true);

    if (vidaActual <= 0)
    {
        GameOverManager manager = FindObjectOfType<GameOverManager>();
        if (manager != null)
        {
            manager.ActivarGameOver();
        }
        else
        {
            Debug.LogWarning("No se encontró GameOverManager");
        }
    }

    Invoke("DesactivarJugador", 1f);
}

    private void DesactivarJugador()
    {
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
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, capaEnemigos);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<EnemigoPadre>()?.RecibirDanio(danioAtaque);

        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }

    public void AplicarRebote(Vector2 direccionRebote)
    {
        if (estaMuerto) return;

        enRebote = true;
        tiempoInicioRebote = Time.time;

        rb.velocity = Vector2.zero;
        rb.AddForce(direccionRebote.normalized * fuerzaRebote, ForceMode2D.Impulse);
    }
}