using UnityEngine;

public class BarrilRespawn : MonoBehaviour
{
    public GameObject prefabBarril;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private GameObject instanciaActual;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        instanciaActual = gameObject;
    }

    public void MarcarDestruido()
    {
        instanciaActual = null;
    }

    public void ReiniciarBarril()
    {
        if (instanciaActual == null)
        {
            instanciaActual = Instantiate(prefabBarril, posicionInicial, rotacionInicial);
            BarrilRespawn nuevo = instanciaActual.GetComponent<BarrilRespawn>();

            if (nuevo != null)
            {
                nuevo.prefabBarril = prefabBarril;
            }
        }
    }
}