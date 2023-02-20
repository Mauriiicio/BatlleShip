using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject explosionAnimationPrefab;
    public AudioSource explosionSound;
    GameManager manager;
    private string healthFillObjectName = "BarGreenFill";
    [SerializeField] private Image healthFill;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            if (img.gameObject.name == healthFillObjectName)
            {
                healthFill = img;
                break;
            }
        }
        healthFill.fillAmount = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthFill.fillAmount = currentHealth / 100f;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (gameObject.name != "Player")
            manager.points += 10;
        Destroy(gameObject);
        GameObject newFireAnimation = Instantiate(explosionAnimationPrefab, transform.position, Quaternion.identity);
    }

}
