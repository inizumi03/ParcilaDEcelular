using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public float damage = 1f;
    private Vector3 startPosition;
    private bool isActive = false;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive) return;

        float speed = GameSpeedController.Instance.GetSpeed();
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    public void ActivateEnemy()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void DeactivateEnemy()
    {
        isActive = false;
        transform.position = startPosition;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControlle player = collision.gameObject.GetComponent<PlayerControlle>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }


            DeactivateEnemy();
            EnemyManager.Instance.StartEnemyCooldown(this);
        }
    }
}
