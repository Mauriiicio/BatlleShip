using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : MonoBehaviour
{
    public float damage;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    
    [SerializeField] private GameObject player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject;
            LookAtEnemy();
        }else
            agent.updatePosition = false;
    }
    
    public void LookAtEnemy()
    {
        // Calcula a rotação que aponta para o jogador
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + 90f);

        // Aplica a rotação no eixo 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
        agent.SetDestination(player.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
            GameObject newFireAnimation = Instantiate(healthSystem.explosionAnimationPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "CannonBall")
        {
            HealthSystem healthSystem = GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(other.GetComponent<CannonBall>().damage);
                Destroy(other.gameObject);
            }
        }
    }
}
