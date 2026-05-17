using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaManager : MonoBehaviour
{
    public static VidaManager instancia;
    public static int vidasGuardadas = 3; // Persiste entre reinicios

    [Header("Vidas")]
    public int vidasActuales = 3;
    public Image[] iconosVida;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }

        // Restaura las vidas guardadas
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

        if (vidasActuales <= 0)
        {
            // Sin vidas → resetea todo
            vidasGuardadas = 3;
            Player.checkpointPos = Vector3.zero;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Tiene vidas → vuelve al checkpoint
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                if (Player.checkpointPos != Vector3.zero)
                {
                    player.transform.position = Player.checkpointPos;
                    player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                }
                else
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}