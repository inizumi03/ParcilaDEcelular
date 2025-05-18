using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    public static GameSpeedController Instance;

    [Header("Velocidad Global")]
    public float globalSpeed = 5f;

    [Header("Incremento autom�tico")]
    public float speedIncreaseRate = 0.5f; // Cu�nto aumenta por segundo
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
        // Aumenta progresivamente hasta el m�ximo
        if (globalSpeed < maxSpeed)
        {
            globalSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    // M�todo para obtener la velocidad actual
    public float GetSpeed()
    {
        return globalSpeed;
    }
}
