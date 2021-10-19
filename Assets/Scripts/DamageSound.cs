using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private AudioSource damage;
    [SerializeField] private AudioSource gg;
    [SerializeField] private AudioSource bgmStart;
    [SerializeField] private AudioSource bgmLoop;
    [SerializeField] private AudioSource bgmStart2;
    [SerializeField] private AudioSource bgmLoop2;    
    [SerializeField] private AudioSource bgmStart3;
    [SerializeField] private AudioSource bgmLoop3;

    [SerializeField] private AudioSource bgm;

    [SerializeField] private Player Player;

    //private bool playedL2 = false;
    //private bool playedL3 = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        damage.Play();
    }

    private void Update()
    {
        if (Player.playerHealth == 0)
        {
            bgm.Stop();
            gg.Play();
        }
        
        

    }

    private void Start()
    {
        bgm.Play();
    }
 
    /*
    IEnumerator BGM()
    {
        yield return new WaitForSeconds(bgmStart.clip.length);
        bgmLoop.Play();
    }
    IEnumerator BGM2()
    {
        bgmLoop.Stop();
        bgmStart2.Play();
        yield return new WaitForSeconds(bgmStart2.clip.length);
        bgmLoop2.Play();
    }
    IEnumerator BGM3()
    {
        bgmLoop2.Stop();
        bgmStart3.Play();
        yield return new WaitForSeconds(bgmStart3.clip.length);
        bgmLoop3.Play();
    }
    */
}
