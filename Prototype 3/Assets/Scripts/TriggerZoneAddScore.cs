using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneAddScore : MonoBehaviour
{
    private UIMangaer uIMangaer;

    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        uIMangaer = GameObject.FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            uIManager.score++;
        }
        
    }
}
