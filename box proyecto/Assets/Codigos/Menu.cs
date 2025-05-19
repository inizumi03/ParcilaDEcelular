using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject mainCanvas;
    public GameObject secondaryCanvas;

    // Llama esta funci�n desde el bot�n que cambia de escena
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Llama esta funci�n desde el bot�n que abre el segundo canvas
    public void ShowSecondaryCanvas()
    {
        if (mainCanvas != null) mainCanvas.SetActive(false);
        if (secondaryCanvas != null) secondaryCanvas.SetActive(true);
    }

    // Llama esta funci�n desde el bot�n del segundo canvas para volver
    public void BackToMainCanvas()
    {
        if (secondaryCanvas != null) secondaryCanvas.SetActive(false);
        if (mainCanvas != null) mainCanvas.SetActive(true);
    }
}
