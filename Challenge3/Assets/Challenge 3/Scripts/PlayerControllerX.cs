﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;
    public float floatForce = 5f; // Adjust this value for how much force is applied
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; // Adjust the global gravity
        playerAudio = GetComponent<AudioSource>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse); // Initial upward force
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse); // Apply upward force
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
