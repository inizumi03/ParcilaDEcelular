using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlle : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 5f;
    private float currentHealth;

    [Header("Stamina (Aguante)")]
    public float maxStamina = 10f;
    private float currentStamina;

    [Header("UI")]
    public Image healthBar;
    public Image staminaBar;
    public GameObject gameOverCanvas;

    [Header("Animación")]
    private Animator animator;

    [Header("Swipe")]
    private Vector2 touchStart;
    private Vector2 touchEnd;
    public float minSwipeDistance = 50f;

    [Header("Inmunidad")]
    public float immunityDuration = 3f;
    private bool isImmune = false;

    [Header("Sonido")]
    public AudioSource hitSound;
    public float hitSoundCooldown = 1f;
    private bool canPlayHitSound = true;

    [Header("Curar con botón")]
    public GameObject curarButton;
    public EnemyDetector enemyDetector;

    private RectTransform canvasRectTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        UpdateHealthBar();
        UpdateStaminaBar();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        if (curarButton != null)
            curarButton.SetActive(false);

        canvasRectTransform = curarButton.transform.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!isImmune)
        {
            HandleSwipe();
        }

        // Revisar constantemente si se debe mostrar el botón de curar
        RevisarBotonCurar();
    }

    private void HandleSwipe()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftArrow)) IntentarGolpear("SwipeLeft");
        if (Input.GetKeyDown(KeyCode.RightArrow)) IntentarGolpear("SwipeRight");
        if (Input.GetKeyDown(KeyCode.UpArrow)) IntentarGolpear("Kick");
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
                            if (x < 0) IntentarGolpear("SwipeLeft");
                            else IntentarGolpear("SwipeRight");
                        }
                        else
                        {
                            if (y > 0) IntentarGolpear("Kick");
                        }
                    }
                    break;
            }
        }
    }

    private void IntentarGolpear(string animTrigger)
    {
        if (currentStamina >= 1)
        {
            animator.SetTrigger(animTrigger);
            ConsumirStamina(1);
        }
    }

    private void ConsumirStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaBar();
    }

    private void RegenerarStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaBar();

        if (currentStamina > 0 && curarButton.activeSelf)
        {
            curarButton.SetActive(false);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isImmune) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        animator.SetTrigger("Hit");
        UpdateHealthBar();

        if (currentHealth > 0f && canPlayHitSound && hitSound != null)
        {
            hitSound.Play();
            StartCoroutine(HitSoundCooldown());
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(StartImmunity());
        }
    }

    private IEnumerator HitSoundCooldown()
    {
        canPlayHitSound = false;
        yield return new WaitForSeconds(hitSoundCooldown);
        canPlayHitSound = true;
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

    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
            staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    public void ActivateTemporaryImmunity(float duration)
    {
        StopCoroutine("StartImmunity");
        StartCoroutine(TemporaryImmunity(duration));
    }

    private IEnumerator TemporaryImmunity(float duration)
    {
        isImmune = true;
        yield return new WaitForSeconds(duration);
        isImmune = false;
    }

    private void Die()
    {
        Debug.Log("Jugador murió");
        animator.SetTrigger("Death");

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;
    }

    private void RevisarBotonCurar()
    {
        if (curarButton == null || canvasRectTransform == null || enemyDetector == null)
            return;

        if (currentStamina <= 0 && enemyDetector.HasEnemies())
        {
            if (!curarButton.activeSelf)
                MostrarBotonCurar();
        }
        else
        {
            if (curarButton.activeSelf)
                curarButton.SetActive(false);
        }
    }

    private void MostrarBotonCurar()
    {
        curarButton.SetActive(true);

        Vector2 randomPos = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Vector2 canvasSize = canvasRectTransform.sizeDelta;
        Vector2 pos = randomPos * canvasSize;

        curarButton.GetComponent<RectTransform>().anchoredPosition = pos;
    }

    public void CurarYDerrotarEnemigo()
    {
        if (enemyDetector == null || enemyDetector.enemiesInRange.Count == 0)
            return;

        GameObject enemigo = enemyDetector.enemiesInRange[0];
        Destroy(enemigo);

        enemyDetector.enemiesInRange.RemoveAt(0);
        RegenerarStamina(3f);
    }

}
