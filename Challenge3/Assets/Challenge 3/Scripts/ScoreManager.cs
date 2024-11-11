using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    public void Start()
    {
        score = 0;
        Update();
    }

   public void AddScore(int points)
    {
        score += points;
        Update();
    }

    public void ResetScore()
    {
        score = 0;
        Update();
    }

    private void Update()
    {
     
        scoreText.text = "Score: " + score;
        
    }
}
