using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f;

    [Header("Ejes de movimiento (activá los que quieras)")]
    public bool moveInX = false;
    public bool moveInY = false;
    public bool moveInZ = true;

    [Header("Loop infinito")]
    public bool enableLoop = true;

    [Tooltip("Distancia que recorre desde su punto original antes de reiniciar")]
    public float loopDistance = 40f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Obtener la velocidad desde el controlador global
        float baseSpeed = GameSpeedController.Instance.GetSpeed();
        float movement = baseSpeed * parallaxFactor * Time.deltaTime;

        Vector3 direction = new Vector3(
            moveInX ? -1f : 0f,
            moveInY ? -1f : 0f,
            moveInZ ? -1f : 0f
        );

        transform.Translate(direction * movement);

        if (enableLoop)
        {
            float traveled = Vector3.Distance(transform.position, startPosition);
            if (traveled >= loopDistance)
            {
                transform.position = startPosition;
            }
        }
    }
}
