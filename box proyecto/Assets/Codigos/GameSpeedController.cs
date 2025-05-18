using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    public static GameSpeedController Instance;

    [Header("Velocidad Global")]
    public float globalSpeed = 5f;

    [Header("Incremento automático")]
    public float speedIncreaseRate = 0.5f; // Cuánto aumenta por segundo
    public float maxSpeed = 20f;

    void Awake()
    {
        // Singleton para acceder desde cualquier script
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Aumenta progresivamente hasta el máximo
        if (globalSpeed < maxSpeed)
        {
            globalSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    // Método para obtener la velocidad actual
    public float GetSpeed()
    {
        return globalSpeed;
    }
}
