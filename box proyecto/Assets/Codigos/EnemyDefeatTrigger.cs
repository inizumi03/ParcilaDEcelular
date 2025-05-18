using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDefeatTrigger : MonoBehaviour
{
    public Text scoreText;
    public int pointsPerEnemy = 1;
    public float reactivationTime = 3f;

    // No usamos el score local, lo maneja ScoreManager
    // private int score = 0;

    // Guarda la posición original de cada enemigo activo o desactivado
    private Dictionary<GameObject, Vector3> enemyOriginalPositions = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        // Busca en toda la escena, incluidos desactivados (solo en root)
        enemigo[] enemigosEnEscena = Resources.FindObjectsOfTypeAll<enemigo>();

        foreach (enemigo e in enemigosEnEscena)
        {
            GameObject go = e.gameObject;
            if (!enemyOriginalPositions.ContainsKey(go))
            {
                enemyOriginalPositions.Add(go, go.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            // Sumamos puntos con ScoreManager
            ScoreManager.Instance.AddPoints(pointsPerEnemy);

            // Actualizamos el texto (opcional, si ScoreManager no lo hace)
            if (scoreText != null)
                scoreText.text = "Puntos: " + ScoreManager.Instance.GetScore().ToString();

            // Detenemos la posible activación inmediata para reiniciar posición
            StartCoroutine(ReactivateEnemy(other.gameObject));
        }
    }

    private IEnumerator ReactivateEnemy(GameObject enemy)
    {
        enemy.SetActive(false);

        yield return new WaitForSeconds(reactivationTime);

        if (enemyOriginalPositions.ContainsKey(enemy))
        {
            enemy.transform.position = enemyOriginalPositions[enemy];
        }

        enemy.SetActive(true);
    }
}

