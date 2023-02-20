using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float velocidade = 5.0f;
    [SerializeField] private float rotacao = 150.0f;
    [SerializeField] private float fireRateCannon = 0.5f;
    [SerializeField] private float fireRateCannonSide = 0.5f;
    [SerializeField] private Transform cannonLoose;
    [SerializeField] private Transform[] cannonLooseSide;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private GameObject fireAnimationPrefab;
    [SerializeField] private int spriteChangeHealth = 10;
    [SerializeField] private SpriteRenderer[] spriteRenderer;
    [SerializeField] private float currentHealth;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private float nextFireCannon = 0.0f;
    private float nextFireCannonSide = 0.0f;

    public bool isGameOver = false;
    private void Start()
    {
        currentHealth = GetComponent<HealthSystem>().maxHealth;
    }
    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            MovementInput();
            RotationInput();

        }
    }
    private void Update()
    {
        if (!isGameOver)
            Shooting();
        ChangeRenderer();
        Boundary();
    }
    public void ChangeRenderer()
    {
        if (currentHealth <= 70)
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteRenderer[0].sprite;
        if (currentHealth <= 50)
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteRenderer[1].sprite;
        if (currentHealth <= 10)
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteRenderer[2].sprite;
    }
    public void MovementInput()
    {
        float eixoVertical = Input.GetAxis("Vertical");
        transform.Translate(transform.up * -eixoVertical * velocidade * Time.deltaTime, Space.World);
    }
    public void RotationInput()
    {
        float eixoHorizontal = Input.GetAxis("Horizontal");
        if (eixoHorizontal != 0)
        {
            transform.Rotate(Vector3.forward, -eixoHorizontal * rotacao * Time.deltaTime);
        }
    }
    public void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFireCannon)
        {
            nextFireCannon = Time.time + fireRateCannon;
            GameObject cannonBallInstance = Instantiate(cannonBall, cannonLoose.position, transform.rotation);
            cannonBallInstance.GetComponent<CannonBall>().Move(transform.up);
            GameObject newFireAnimation = Instantiate(fireAnimationPrefab, cannonLoose.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextFireCannonSide)
        {
            nextFireCannonSide = Time.time + fireRateCannonSide;
            GameObject cannonBallSide0 = Instantiate(cannonBall, cannonLooseSide[0].position, transform.rotation);
            GameObject cannonBallSide1 = Instantiate(cannonBall, cannonLooseSide[1].position, transform.rotation);
            GameObject cannonBallSide2 = Instantiate(cannonBall, cannonLooseSide[2].position, transform.rotation);

            cannonBallSide0.GetComponent<CannonBall>().Move(-transform.right);
            cannonBallSide1.GetComponent<CannonBall>().Move(-transform.right);
            cannonBallSide2.GetComponent<CannonBall>().Move(-transform.right);

            GameObject newFireAnimation0 = Instantiate(fireAnimationPrefab, cannonLooseSide[0].position, Quaternion.identity);
            GameObject newFireAnimation1 = Instantiate(fireAnimationPrefab, cannonLooseSide[1].position, Quaternion.identity);
            GameObject newFireAnimation2 = Instantiate(fireAnimationPrefab, cannonLooseSide[2].position, Quaternion.identity);
        }

    }
    public void Boundary()
    {
        Vector3 playerPos = transform.position;
        float clampedX = Mathf.Clamp(playerPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(playerPos.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, playerPos.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CannonBall")
        {
            HealthSystem healthSystem = GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(other.GetComponent<CannonBall>().damage);
                currentHealth = healthSystem.currentHealth;
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.layer == 3)
        {
            HealthSystem healthSystem = GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(other.GetComponent<EnemyChaser>().damage);
                currentHealth = healthSystem.currentHealth;
            }
        }
    }
}
