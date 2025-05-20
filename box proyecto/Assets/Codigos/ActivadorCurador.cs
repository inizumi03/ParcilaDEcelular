using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorCurador : MonoBehaviour
{
    public GameObject objetoCurador;       // Objeto curador que se activará
    public Transform puntoDeAparicion;     // Posición donde aparecerá (usa un Empty con Transform)
    public float tiempoEntreActivaciones = 10f;

    private float temporizador;

    private void Start()
    {
        temporizador = tiempoEntreActivaciones;
    }

    private void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            ActivarCurador();
            temporizador = tiempoEntreActivaciones;
        }
    }

    private void ActivarCurador()
    {
        if (objetoCurador != null && puntoDeAparicion != null)
        {
            objetoCurador.transform.position = puntoDeAparicion.position;
            objetoCurador.SetActive(true);
        }
    }
}
