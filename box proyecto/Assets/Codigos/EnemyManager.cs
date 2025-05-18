using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Configuración")]
    public float activationDelay = 3f;

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
        foreach (GameObject go in enemigosObjects)
        {
            enemigo ec = go.GetComponent<enemigo>();
            if (ec != null)
            {
                enemigos.Add(ec);
            }
            else
            {
                Debug.LogWarning($"El GameObject {go.name} no tiene script enemigo");
            }
        }

        StartCoroutine(ActivarEnemigosSecuencial());
    }

    IEnumerator ActivarEnemigosSecuencial()
    {
        foreach (enemigo enemigo in enemigos)
        {
            yield return new WaitForSeconds(activationDelay);
            enemigo.ActivateEnemy();
        }
    }

    public void StartEnemyCooldown(enemigo enemigo)
    {
        StartCoroutine(ReactivateEnemyAfterDelay(enemigo));
    }

    IEnumerator ReactivateEnemyAfterDelay(enemigo enemigo)
    {
        yield return new WaitForSeconds(activationDelay);
        enemigo.ActivateEnemy();
    }

}
