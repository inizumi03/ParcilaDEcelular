using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFixer : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 fixedPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Guardamos la posición inicial del jugador
        fixedPosition = transform.position;

        // Congelamos toda la física para que no se mueva ni rote
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void LateUpdate()
    {
        // Forzamos al jugador a quedarse en la misma posición todo el tiempo
        transform.position = fixedPosition;
    }
}
