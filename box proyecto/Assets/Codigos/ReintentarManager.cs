using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReintentarManager : MonoBehaviour
{
    // Esta funci�n se puede asignar al bot�n "Reintentar" desde el Canvas
    public void ReiniciarEscena()
    {
        Time.timeScale = 1f; // Despausar el juego por si estaba pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar escena actual
    }
}
