using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cura : MonoBehaviour
{
    public float activationDelay = 10f;
    public float moveSpeed = 3f;
    public float immunityDuration = 3f;

    public GameObject inmunityImage; // Asignar imagen del Canvas
    public AudioClip healSound;

    private Transform player;
    private AudioSource audioSource;

    private Vector3 initialPosition;
    private bool isActive = false;

    private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();

        gameObject.SetActive(false); // Empieza desactivado
        StartCoroutine(ActivateAfterDelay());
    }

    private void Update()
    {
        if (isActive && player != null)
        {
            // Movimiento hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay);
        ResetPosition();
        gameObject.SetActive(true);
        isActive = true;
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            PlayerControlle playerScript = other.GetComponent<PlayerControlle>();
            if (playerScript != null)
            {
                playerScript.Heal(1f); // Cura 1 punto de vida
                playerScript.ActivateTemporaryImmunity(immunityDuration);

                if (inmunityImage != null)
                    StartCoroutine(ShowInmunityImage());

                if (audioSource != null && healSound != null)
                    audioSource.PlayOneShot(healSound);
            }

            isActive = false;
            gameObject.SetActive(false); // Se desactiva
            StartCoroutine(ActivateAfterDelay()); // Comienza ciclo otra vez
        }
    }

    private IEnumerator ShowInmunityImage()
    {
        inmunityImage.SetActive(true);
        yield return new WaitForSeconds(immunityDuration);
        inmunityImage.SetActive(false);
    }
}
