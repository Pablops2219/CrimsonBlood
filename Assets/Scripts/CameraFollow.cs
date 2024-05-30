using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador

    // Offset opcional para ajustar la posición relativa de la cámara
    public Vector3 offset = new Vector3(0, 0, -10); // Por defecto, la cámara estará detrás del jugador

    // Velocidad de seguimiento de la cámara
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Calcular la posición deseada de la cámara sumando el offset al jugador
        Vector3 desiredPosition = target.position + offset;

        // Interpolación suave hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualizar la posición de la cámara
        transform.position = smoothedPosition;
    }
}