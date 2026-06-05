using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    [Header("Efecto visual")]
    public GameObject efectoDestello;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip CheckpointClip;

    private bool activado = false;
    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (activado) return;
        if (!collision.CompareTag("Player")) return;

        activado = true;
        Player.checkpointPos = transform.position;

        if (efectoDestello != null)
        {
            Instantiate(efectoDestello, transform.position, Quaternion.identity);
        }

        if (audioSource != null && CheckpointClip != null)
        {
            audioSource.PlayOneShot(CheckpointClip);
        }

        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        StartCoroutine(DesactivarDespues());
    }

    IEnumerator DesactivarDespues()
    {
        yield return new WaitForSeconds(CheckpointClip.length);
        gameObject.SetActive(false);
    }
}