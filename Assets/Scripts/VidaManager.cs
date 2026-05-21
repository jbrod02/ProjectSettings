using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaManager : MonoBehaviour
{
    public static VidaManager instancia;
    public static int vidasGuardadas = 3;

    [Header("Vidas")]
    public int vidasActuales = 3;
    public Image[] iconosVida;

    [Header("Barriles que reaparecen")]
    public BarrilRespawn[] barriles;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }

        vidasActuales = vidasGuardadas;
        ActualizarIconos();
    }

    void ActualizarIconos()
    {
        for (int i = 0; i < iconosVida.Length; i++)
        {
            iconosVida[i].enabled = i < vidasActuales;
        }
    }

    public void PerderVida()
    {
        vidasActuales--;
        vidasGuardadas = vidasActuales;
        ActualizarIconos();

        if (vidasActuales > 0)
        {
            RespawnEnCheckpoint();
            ReactivarBarriles();
        }
        else
        {
            ReinicioCompletoNivel();
        }
    }

    void RespawnEnCheckpoint()
    {
        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (Player.checkpointPos != Vector3.zero)
            {
                player.transform.position = Player.checkpointPos;

                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
            else
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void ReactivarBarriles()
    {
        if (barriles == null || barriles.Length == 0) return;

        foreach (BarrilRespawn barril in barriles)
        {
            if (barril != null)
            {
                barril.ReiniciarBarril();
            }
        }
    }

    void ReinicioCompletoNivel()
    {
        vidasGuardadas = 3;
        Player.checkpointPos = Vector3.zero;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}