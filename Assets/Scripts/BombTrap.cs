using UnityEngine;
using System.Collections;

public class BombTrap : MonoBehaviour
{
    private Animator animator;
    private Collider2D col;
    private bool activada = false;

    public float tiempoAntesDeDañar = 0.35f;
    public float tiempoAntesDeDestruir = 0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (activada) return;
        if (!collision.CompareTag("Player")) return;

        StartCoroutine(Explotar());
    }

    IEnumerator Explotar()
    {
        activada = true;

        if (col != null)
            col.enabled = false;

        if (animator != null)
            animator.SetTrigger("Explode");

        yield return new WaitForSeconds(tiempoAntesDeDañar);

        if (VidaManager.instancia != null)
            VidaManager.instancia.PerderVida();

        yield return new WaitForSeconds(tiempoAntesDeDestruir);

        Destroy(gameObject);
    }
}