using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReintentarManager : MonoBehaviour
{
    // Esta función se puede asignar al botón "Reintentar" desde el Canvas
    public void ReiniciarEscena()
    {
        Time.timeScale = 1f; // Despausar el juego por si estaba pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar escena actual
    }
}
