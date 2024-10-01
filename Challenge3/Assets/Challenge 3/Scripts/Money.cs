using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private UIManager uIManager;

    void Start()
    {
        uIManager = GameObject.FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.AddScore(1); // Add score when collected
            Destroy(gameObject); // Destroy the money object
        }
    }
}
