using UnityEngine;

public class Sandbug : MonoBehaviour
{
    [Header("Detección")]
    public float rangoDeteccion = 4f;
    public Transform jugador;

    [Header("Tiempos")]
    public float tiempoSacudida = 1.5f;   // Sacudida antes de emerger
    public float tiempoExpuesto = 3f;      // Tiempo vulnerable en superficie
    public float tiempoEscondido = 2f;     // Tiempo bajo la arena antes de repetir

    [Header("Knockback al jugador")]
    public float fuerzaKnockback = 5f;

    [Header("Sacudida visual")]
    public float intensidadSacudida = 0.05f;
    public float velocidadSacudida = 20f;

    private Animator animator;
    private Collider2D col;
    private Vector3 posicionOriginal;

    private enum Estado { Escondido, Sacudiendo, Expuesto, Hundiendose }
    private Estado estadoActual = Estado.Escondido;
    private float timerEstado = 0f;
    private bool jugadorEncima = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        posicionOriginal = transform.position;

        // Empieza escondido
        SetSprite(false);
    }

    void Update()
    {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        switch (estadoActual)
        {
            case Estado.Escondido:
                // Si el jugador se acerca, empieza a sacudirse
                if (distancia < rangoDeteccion)
                {
                    estadoActual = Estado.Sacudiendo;
                    timerEstado = tiempoSacudida;
                    if (animator != null) animator.SetTrigger("Sacudir");
                }
                break;

            case Estado.Sacudiendo:
                // Efecto de sacudida visual
                float offsetX = Mathf.Sin(Time.time * velocidadSacudida) * intensidadSacudida;
                transform.position = posicionOriginal + new Vector3(offsetX, 0, 0);

                timerEstado -= Time.deltaTime;
                if (timerEstado <= 0f)
                {
                    // Emerge
                    transform.position = posicionOriginal;
                    estadoActual = Estado.Expuesto;
                    timerEstado = tiempoExpuesto;
                    SetSprite(true);
                    if (animator != null) animator.SetTrigger("Emerger");
                }
                break;

            case Estado.Expuesto:
                timerEstado -= Time.deltaTime;
                if (timerEstado <= 0f)
                {
                    // Se hunde solo si el jugador no lo derrotó
                    Hundirse();
                }
                break;

            case Estado.Hundiendose:
                timerEstado -= Time.deltaTime;
                if (timerEstado <= 0f)
                {
                    estadoActual = Estado.Escondido;
                    posicionOriginal = transform.position;
                    SetSprite(false);
                    if (animator != null) animator.SetTrigger("Esconder");
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (estadoActual != Estado.Expuesto) return;

        Player player = collision.GetComponent<Player>();
        Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();

        // ¿El jugador cayó desde arriba?
        bool saltóEncima = rbJugador != null && rbJugador.linearVelocity.y < -0.1f
                           && collision.transform.position.y > transform.position.y + 0.2f;

        if (saltóEncima)
        {
            // Sandbug muere
            if (animator != null) animator.SetTrigger("Morir");
            col.enabled = false;
            Destroy(gameObject, 0.5f);

            // Pequeño rebote al jugador
            rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 6f);
        }
        else
        {
            // Knockback al jugador
            if (rbJugador != null)
            {
                Vector2 direccion = (rbJugador.position - (Vector2)transform.position).normalized;
                rbJugador.linearVelocity = Vector2.zero;
                rbJugador.AddForce(direccion * fuerzaKnockback, ForceMode2D.Impulse);
            }
        }
    }

    void Hundirse()
    {
        estadoActual = Estado.Hundiendose;
        timerEstado = tiempoEscondido;
        SetSprite(false);
        if (animator != null) animator.SetTrigger("Hundirse");
    }

    void SetSprite(bool visible)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = visible;
        if (col != null) col.enabled = visible;
    }

    // Dibuja el rango de detección en el Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
