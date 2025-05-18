using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDefeatTrigger : MonoBehaviour
{
    public Text scoreText; // Texto UI que muestra los puntos
    public int pointsPerEnemy = 1; // Cuántos puntos suma por enemigo derrotado

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            // Sumamos puntos
            score += pointsPerEnemy;

            // Actualizamos UI
            if (scoreText != null)
                scoreText.text = "Puntos: " + score.ToString();

            // Desactivamos el enemigo
            other.gameObject.SetActive(false);
        }
    }
}

