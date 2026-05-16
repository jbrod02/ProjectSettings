using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  
  public Transform target; // El objetivo a seguir

  private void LateUpdate()
  
    {
      // Actualiza la posición de la cámara para seguir al objetivo
      transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
  
}