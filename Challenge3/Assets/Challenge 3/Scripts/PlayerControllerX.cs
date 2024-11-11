/*
 Treasure Keys 
Challenge 3
Control player movement and gravity

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;
    public float floatForce = 10f;
    public float bounceForce = 5f;
    public float gravityModifier = 1.2f;
    public float maxY = 14f;
    public float minY = 0.5f;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip groundedSound;

    private Rigidbody playerRb;

    public ScoreManager scoreManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        playerRb.AddForce(Physics.gravity * (gravityModifier - 1), ForceMode.Acceleration);

        playerRb.drag = 1f;

        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
        }

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < maxY)
            {
                playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            }

            if (transform.position.y > maxY)
            {
                playerRb.position = new Vector3(playerRb.position.x, maxY, playerRb.position.z);
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            }

            if (transform.position.y < minY)
            {
                playerRb.position = new Vector3(playerRb.position.x, minY, playerRb.position.z);
                playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                playerAudio.PlayOneShot(groundedSound, 1.0f);
            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Destroy(collision.gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            scoreManager.AddScore(1); 
            Destroy(other.gameObject); 
        }
    }
}
