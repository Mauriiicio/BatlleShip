using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cannon;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackDelay = 5f;
    [SerializeField] private GameObject fireAnimationPrefab;
    private NavMeshAgent agent;
    void Start()
    {
        StartCoroutine(AttackLoop());
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject.transform;
            LookAtEnemy();
        }
        else
            agent.updatePosition = false;

    }

    public void LookAtEnemy()
    {
        // Calcula a rotação que aponta para o jogador
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + 90f);

        // Aplica a rotação no eixo 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
        if (Vector2.Distance(transform.position, player.transform.position) > 5)
            agent.SetDestination(player.transform.position);
        else
            agent.SetDestination(transform.position);
    }

    void ShootingAtTime()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 5)
        {
            GameObject cannonBallInstance = Instantiate(cannonBall, cannon.position, transform.rotation);
            GameObject newFireAnimation = Instantiate(fireAnimationPrefab, cannon.position, Quaternion.identity);
            cannonBallInstance.GetComponent<CannonBall>().Move(transform.up);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
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
    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            if (player != null)
                ShootingAtTime();
        }
    }
}