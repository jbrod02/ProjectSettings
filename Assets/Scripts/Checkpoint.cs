using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Efecto visual")]
    public GameObject efectoDestello;

    private bool activado = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (activado) return;
        if (!collision.CompareTag("Player")) return;

        activado = true;

        // Guarda la posición del checkpoint
        Player.checkpointPos = transform.position;

        // Efecto visual
        if (efectoDestello != null)
        {
            Instantiate(efectoDestello, transform.position, Quaternion.identity);
        }

        // Desactiva la moneda
        gameObject.SetActive(false);
    }
}