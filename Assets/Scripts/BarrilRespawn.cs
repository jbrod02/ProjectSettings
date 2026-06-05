using UnityEngine;

public class BarrilRespawn : MonoBehaviour
{
    public GameObject prefabBarril;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private bool destruido = false;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        SpawnBarril();
    }

    void SpawnBarril()
    {
        if (prefabBarril == null)
        {
            Debug.LogError("PrefabBarril no asignado en " + gameObject.name);
            return;
        }

        GameObject nuevoBarril = Instantiate(prefabBarril, posicionInicial, rotacionInicial);

        BarrilInstancia instancia = nuevoBarril.GetComponent<BarrilInstancia>();
        if (instancia != null)
        {
            instancia.spawner = this;
        }
        else
        {
            Debug.LogError("El prefab del barril no tiene BarrilInstancia");
        }
    }

    public void MarcarDestruido()
    {
        destruido = true;
        Debug.Log(gameObject.name + " marcado como destruido");
    }

    public void ReiniciarBarril()
    {
        Debug.Log(gameObject.name + " ReiniciarBarril, destruido = " + destruido);

        if (destruido)
        {
            destruido = false;
            SpawnBarril();
        }
    }
}