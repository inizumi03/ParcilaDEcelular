using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<GameObject> enemiesInRange = new List<GameObject>();

    public delegate void EnemiesChangedHandler();
    public event EnemiesChangedHandler OnEnemiesChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo") && !enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Add(other.gameObject);
            OnEnemiesChanged?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemigo") && enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Remove(other.gameObject);
            OnEnemiesChanged?.Invoke();
        }
    }

    public bool HasEnemies()
    {
        return enemiesInRange.Count > 0;
    }
}
