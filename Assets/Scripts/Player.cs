using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 4f;

    private Rigidbody2D rb2D;
    private float move;
    private bool isGrounded;

    [Header("Suelo")]
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Animaciones normales")]
    private Animator animator;

    [Header("Monedas")]
    private int coins = 0;
    public TMP_Text text;

    [Header("Panel de victoria")]
    public GameObject panelGanaste;

    [Header("Escenas")]
    public string nombreEscenaMenuPrincipal = "MainMenu";

    private bool juegoTerminado = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Time.timeScale = 1f;

        if (panelGanaste != null)
        {
            panelGanaste.SetActive(false);
        }

        if (text != null)
        {
            text.text = coins.ToString();
        }
    }

    void Update()
    {
        if (juegoTerminado)
            return;

        move = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(move));
            animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
            animator.SetBool("IsGrounded", isGrounded);
        }
    }

    void FixedUpdate()
    {
        if (juegoTerminado)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (juegoTerminado)
            return;

        if (collision.CompareTag("Coin"))
        {
            coins++;
            if (text != null)
            {
                text.text = coins.ToString();
            }
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Spikes"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.CompareTag("Barrel"))
        {
            Vector2 knockbackDirection = (rb2D.position - (Vector2)collision.transform.position).normalized;
            rb2D.linearVelocity = Vector2.zero;
            rb2D.AddForce(knockbackDirection * 3f, ForceMode2D.Impulse);

            BoxCollider2D[] colliders = collision.gameObject.GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = false;
            }

            Animator barrelAnimator = collision.GetComponent<Animator>();
            if (barrelAnimator != null)
            {
                barrelAnimator.enabled = true;
            }

            Destroy(collision.gameObject, 0.5f);
        }

        if (collision.CompareTag("Ruby"))
        {
            juegoTerminado = true;
            rb2D.linearVelocity = Vector2.zero;

            if (panelGanaste != null)
            {
                panelGanaste.SetActive(true);
            }

            Destroy(collision.gameObject);

            Canvas.ForceUpdateCanvases();
            Time.timeScale = 0f;
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenuPrincipal);
    }
}