using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask Enemys;
    public float coolDown = 2f;
    private float currentTime = 0f;
    public float radius = 3f;
    public int damage = 1;
    
    public AudioSource fire;
    public AudioSource die;
    private bool canShoot = true;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(boxCollider.bounds.center, radius, Vector2.up, 1f, Enemys);
        if (hit.collider != null && canShoot)
        {
            hit.collider.gameObject.GetComponent<Enemy>().health -= damage;
            if (hit.collider.gameObject.GetComponent<Enemy>().health <= 0)
                die.Play();
            fire.Play();
            canShoot = false;
        }
        if (!canShoot)
            ResetShoot();

    }

    void ResetShoot()
    {
        if (currentTime < coolDown)
            currentTime = currentTime + 1 * Time.deltaTime;
        else
        {
            currentTime = 0f;
            canShoot = true;
        }
    }
}
