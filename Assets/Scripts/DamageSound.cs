using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private AudioSource damage;
    [SerializeField] private AudioSource gg;

    [SerializeField] private Player Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        damage.Play();
    }

    private void Update()
    {
        if (Player.playerHealth == 0)
        {
            gg.Play();
        }
    }
}
