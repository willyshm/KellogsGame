using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMaze : MonoBehaviour
{
    // Variable pública para definir el nombre de la escena en el Inspector
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cargar la escena definida en el Inspector
            SceneManager.LoadScene(nextScene);
        }
    }
}
