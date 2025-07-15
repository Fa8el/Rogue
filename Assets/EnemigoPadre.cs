using UnityEngine;

public class EnemigoPadre : MonoBehaviour
{
    [Header("Estadísticas")]
    public int vida = 100;
    public int vidaMaxima = 100;
    public float velocidad = 2f;
    public int daño = 10;

    protected Transform jugador;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    private ParticleSystem niebla;
    private ParticleSystem.EmissionModule emission;

    [Header("Ataque")]
    public float rangoAtaque = 1.2f;
    public float cooldownAtaque = 1.5f;
    protected float tiempoUltimoAtaque;

    protected Animator anim;


    protected virtual void Start()
    {
        jugador = GameObject.FindWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
            Debug.LogWarning($"⚠️ {gameObject.name} no tiene SpriteRenderer en hijos.");

        vidaMaxima = vida;

        niebla = GetComponentInChildren<ParticleSystem>();
        if (niebla != null)
        {
            emission = niebla.emission;
            niebla.Play();
            ActualizarNiebla();
        }
        anim = GetComponent<Animator>();
        if (anim == null)
        Debug.LogWarning($"⚠️ {gameObject.name} no tiene Animator asignado.");

    }

    protected virtual void Update()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque)
        {
            Atacar();
        }
    }

    protected virtual void Atacar()
    {
        if (Time.time - tiempoUltimoAtaque >= cooldownAtaque)
        {
            PlayerController player = jugador.GetComponent<PlayerController>();
            if (player != null && !player.estaMuerto)
            {
                player.RecibirDanio(daño, transform.position);
                Debug.Log($"💥 {gameObject.name} atacó al jugador con {daño} de daño.");
                if (anim != null)
                anim.SetTrigger("ataque");

                tiempoUltimoAtaque = Time.time;

            }
        }
    }

    public virtual void RecibirDanio(int cantidad)
    {
        vida -= cantidad;
        Debug.Log($"⚔️ {gameObject.name} recibió daño. Vida: {vida}");
        ActualizarNiebla();

        if (vida <= 0)
        {
            Morir();
        }
    }

    protected virtual void Morir()
    {
        Debug.Log($"💀 {gameObject.name} murió.");

        if (niebla != null)
        {
            niebla.Stop();
        }

        //Destroy(gameObject);
        if (anim != null)
    anim.SetTrigger("muerte");

// Desactivar física y collider para que no estorbe
rb.velocity = Vector2.zero;
GetComponent<Collider2D>().enabled = false;

// Destruir después de la animación (1 segundo por ejemplo)
Destroy(gameObject, 1.2f);

    }

    protected virtual void ActualizarNiebla()
    {
        float porcentaje = Mathf.Clamp01((float)vida / vidaMaxima);

        if (sr != null)
        {
            float gris = Mathf.Lerp(0.3f, 1f, porcentaje);
            sr.color = new Color(gris, gris, gris, 1f);
        }

        if (niebla != null)
        {
            var rate = Mathf.Lerp(0f, 20f, 1f - porcentaje);
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(rate);
        }
    }
}


