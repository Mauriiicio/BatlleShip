using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    [SerializeField] private GameObject fireAnimationPrefab;
    private void Update()
    {
        Destroy(gameObject, 3);
    }
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(-direction * speed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            GameObject newFireAnimation = Instantiate(fireAnimationPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
