using UnityEngine;

public class TutorialInicioNivel : MonoBehaviour
{
    [Header("Panel del tutorial")]
    public GameObject panelTutorial;

    void Start()
    {
        if (panelTutorial != null)
            panelTutorial.SetActive(true);

        Time.timeScale = 0f;
    }

    public void CerrarTutorial()
    {
        if (panelTutorial != null)
            panelTutorial.SetActive(false);

        Time.timeScale = 1f;
    }
}