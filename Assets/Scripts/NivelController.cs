using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NivelController : MonoBehaviour
{
    // Panel principal de pausa
    public GameObject panelPausa;

    // Panel de confirmación para salir
    public GameObject panelConfirmacion;

    // Texto donde se muestra el progreso al confirmar salida
    public TextMeshProUGUI textoProgreso;

    // Variables de progreso del nivel (ajusta según tu juego)
    public static int monedasRecogidas = 0;
    public static int enemigosEliminados = 0;

    private bool pausado = false;

    void Start()
    {
        // Oculta ambos paneles al iniciar
        panelPausa.SetActive(false);
        panelConfirmacion.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Alterna pausa con Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado) Continuar();
            else Pausar();
        }
    }

    // Congela el juego y muestra el panel de pausa
    public void Pausar()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
        pausado = true;
    }

    // Reanuda el juego y oculta todo
    public void Continuar()
    {
        panelPausa.SetActive(false);
        panelConfirmacion.SetActive(false);
        Time.timeScale = 1f;
        pausado = false;
    }

    // Muestra la confirmación con el progreso actual
    public void ConfirmarSalida()
    {
        panelPausa.SetActive(false);
        panelConfirmacion.SetActive(true);

        // Muestra el progreso acumulado al jugador
        textoProgreso.text =
            "¿Seguro que quieres salir?\n\n" ;
    }

    // Confirma y carga el menú principal
    public void MenuPrincipal()
    {
        monedasRecogidas = 0;
        enemigosEliminados = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Cancela y regresa al panel de pausa
    public void CancelarSalida()
    {
        panelConfirmacion.SetActive(false);
        panelPausa.SetActive(true);
    }
}