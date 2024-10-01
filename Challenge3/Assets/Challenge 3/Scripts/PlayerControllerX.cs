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

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKey(KeyCode.Space) && transform.position.y < maxY)
            {
                playerRb.AddForce(Vector3.up * floatForce);
            }

            if (!Input.GetKey(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.down * floatForce * Time.deltaTime, ForceMode.Acceleration);
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
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
