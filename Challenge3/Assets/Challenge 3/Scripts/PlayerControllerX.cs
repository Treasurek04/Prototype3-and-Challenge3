using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;
    public float floatForce = 5f;
    private float gravityModifier = 1.2f;
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    void Start()
    {
        // Get Rigidbody and AudioSource components
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        // Set gravity
        Physics.gravity *= gravityModifier;

        // Give initial upward force to the balloon
        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
    }

    void Update()
    {
        // Check for spacebar input to make the balloon float
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            playerRb.AddForce(Vector3.down * gravityModifier * Time.deltaTime);
        }

        // Prevent the balloon from floating off screen
        if (transform.position.y > 14 && playerRb.velocity.y > 0)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Handle collision with Bomb
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // Handle collision with Money
        if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }

    }
}
