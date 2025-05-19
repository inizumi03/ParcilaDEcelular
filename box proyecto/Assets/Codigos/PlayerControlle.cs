using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlle : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 5f;
    private float currentHealth;

    [Header("UI")]
    public Image healthBar;
    public GameObject gameOverCanvas; // <-- Asignar desde el Inspector

    [Header("Animación")]
    private Animator animator;

    [Header("Swipe")]
    private Vector2 touchStart;
    private Vector2 touchEnd;
    public float minSwipeDistance = 50f;

    [Header("Inmunidad")]
    public float immunityDuration = 3f;
    private bool isImmune = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false); // Oculta el canvas al inicio
    }

    private void Update()
    {
        if (!isImmune)
        {
            HandleSwipe();
        }
    }

    private void HandleSwipe()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger("SwipeLeft");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("SwipeRight");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Kick");
        }
#endif

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    touchEnd = touch.position;
                    Vector2 swipe = touchEnd - touchStart;

                    if (swipe.magnitude >= minSwipeDistance)
                    {
                        float x = swipe.x;
                        float y = swipe.y;

                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            if (x < 0)
                                animator.SetTrigger("SwipeLeft");
                            else
                                animator.SetTrigger("SwipeRight");
                        }
                        else
                        {
                            if (y > 0)
                                animator.SetTrigger("Kick");
                            // Puedes agregar más casos como swipe abajo si lo deseas
                        }
                    }
                    break;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (isImmune) return; // Ignorar daño si está inmune

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        animator.SetTrigger("Hit");
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(StartImmunity());
        }
    }

    private IEnumerator StartImmunity()
    {
        isImmune = true;

        yield return new WaitForSeconds(immunityDuration);

        isImmune = false;
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Die()
    {
        Debug.Log("Jugador murió");
        animator.SetTrigger("Death");

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Time.timeScale = 0f; // Pausa el juego
    }
}
