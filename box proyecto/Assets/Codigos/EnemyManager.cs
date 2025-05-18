using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Configuración")]
    public List<float> activationDelays; // Cada enemigo puede tener un delay distinto
    public float defaultDelay = 3f; // Si no hay delay específico

    [Header("Lista de enemigos (GameObjects desactivados en escena)")]
    public List<GameObject> enemigosObjects;

    private List<enemigo> enemigos = new List<enemigo>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < enemigosObjects.Count; i++)
        {
            GameObject go = enemigosObjects[i];
            enemigo ec = go.GetComponent<enemigo>();
            if (ec != null)
            {
                enemigos.Add(ec);
                float delay = (i < activationDelays.Count) ? activationDelays[i] : defaultDelay;
                StartCoroutine(ActivarEnemigoConDelay(ec, delay));
            }
            else
            {
                Debug.LogWarning($"El GameObject {go.name} no tiene script enemigo");
            }
        }
    }

    IEnumerator ActivarEnemigoConDelay(enemigo enemigo, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemigo.ActivateEnemy();
    }

    public void StartEnemyCooldown(enemigo enemigo)
    {
        int index = enemigos.IndexOf(enemigo);
        float delay = (index >= 0 && index < activationDelays.Count) ? activationDelays[index] : defaultDelay;
        StartCoroutine(ReactivateEnemyAfterDelay(enemigo, delay));
    }

    IEnumerator ReactivateEnemyAfterDelay(enemigo enemigo, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemigo.ActivateEnemy();
    }

}
