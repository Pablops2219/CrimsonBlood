using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader: MonoBehaviour
{
    [SerializeField]
    private string sceneName; // Nombre de la escena a cargar

    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Funciono");
        // Verifica si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Carga la escena especificada
            SceneManager.LoadScene(sceneName);
        }
    }
}