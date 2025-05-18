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

    [Header("Animación")]
    private Animator animator;

    [Header("Swipe")]
    private Vector2 touchStart;
    private Vector2 touchEnd;
    public float minSwipeDistance = 50f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        HandleSwipe();
    }

    private void HandleSwipe()
    {
#if UNITY_EDITOR
        // Simulación con teclado para testing en editor
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger("SwipeLeft");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("SwipeRight");
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
                        if (x < 0)
                        {
                            animator.SetTrigger("SwipeLeft");
                        }
                        else
                        {
                            animator.SetTrigger("SwipeRight");
                        }
                    }
                    break;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        animator.SetTrigger("Hit");
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Jugador murió");
        animator.SetTrigger("Death");
        // Aquí podés poner lógica de game over, reinicio, etc.
    }
}
