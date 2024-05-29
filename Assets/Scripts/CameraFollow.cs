using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    

    void LateUpdate()
    {
        
        // Asegurarse de que la cámara no rote
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}