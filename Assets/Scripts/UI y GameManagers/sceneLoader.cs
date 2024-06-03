using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader: MonoBehaviour
{

    public Animator transition;
    public float stop; 
    
    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();
        }

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Esconder");

        yield return new WaitForSeconds(stop);

        if (levelIndex >= 3)
        {
            levelIndex = 0;
        }
        SceneManager.LoadScene(levelIndex);
            
    }
    
    public void LoadNextLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
}