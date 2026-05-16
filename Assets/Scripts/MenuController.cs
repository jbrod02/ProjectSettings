
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Panel del menú principal (contiene botones "Jugar" y "Salir")
    public GameObject panelMenuPrincipal;

    // Panel de selección de niveles (contiene botones de cada nivel)
    public GameObject panelSeleccionNivel;

    void Start()
    {
        // Al iniciar, muestra el menú principal y oculta la selección de niveles
        panelMenuPrincipal.SetActive(true);
        panelSeleccionNivel.SetActive(false);
    }

    // Se llama al presionar el botón "Jugar"
    // Oculta el menú principal y muestra los niveles disponibles
    public void Jugar()
    {
        panelMenuPrincipal.SetActive(false);
        panelSeleccionNivel.SetActive(true);
    }

    // Se llama al presionar el botón "Volver"
    // Regresa al menú principal desde la pantalla de niveles
    public void Volver()
    {
        panelSeleccionNivel.SetActive(false);
        panelMenuPrincipal.SetActive(true);
    }

    // Se llama al presionar el botón "Nivel 1"
    // Carga la escena del primer nivel
    public void CargarNivel1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Se llama al presionar el botón "Nivel 2"
    // Carga la escena del segundo nivel
    public void CargarNivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    // Se llama al presionar el botón "Salir"
    // Cierra el juego (en el Editor detiene el Play Mode)
    public void Salir()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}