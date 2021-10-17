using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    [SerializeField] private Transform pointToGo;
    public float speed;
    [SerializeField] private Player Player;
    
    void Start()
    {
        
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, pointToGo.position, step);
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.playerHealth -= 1;

        Destroy(gameObject);
    }

    void Die()
    {
        Player.score += 100;
        Player.money += 10;
        Destroy(gameObject);
    }
}
