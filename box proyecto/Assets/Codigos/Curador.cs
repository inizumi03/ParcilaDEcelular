using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curador : MonoBehaviour
{
    public float velocidad = 3f;
    public float curacion = 1f;
    private Transform objetivoJugador;

    private void OnEnable()
    {
        // Buscar al jugador por Tag
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
            objetivoJugador = jugador.transform;
    }

    private void Update()
    {
        if (objetivoJugador != null)
        {
            // Mover hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, objetivoJugador.position, velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Buscar el componente del jugador y curar
            PlayerControlle jugador = other.GetComponent<PlayerControlle>();
            if (jugador != null)
            {
                jugador.Heal(curacion);
            }

            gameObject.SetActive(false); // Desactivar después de curar
        }
    }
}
