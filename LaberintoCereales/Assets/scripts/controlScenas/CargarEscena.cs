using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargarEscena : MonoBehaviour
{
    public Button yourButton; // Arrastra tu botón aquí desde el Inspector
    public string sceneName; // El nombre de la escena a cargar

    void Start()
    {
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(ChangeScene);
        }
    }

    void ChangeScene()
    {
        // Verifica si el nombre de la escena no está vacío
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no está definido.");
        }
    }
}
