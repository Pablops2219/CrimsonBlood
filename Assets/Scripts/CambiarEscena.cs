using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscenaAlColisionar : MonoBehaviour
{
    // El nombre de la escena a la que deseas cambiar
    public string nombreDeLaEscena;

    // Método que se llama cuando el personaje colisiona con otro objeto
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisionó tiene el tag "Bloque"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambia a la escena especificada
            SceneManager.LoadScene(nombreDeLaEscena);
        }
    }
}