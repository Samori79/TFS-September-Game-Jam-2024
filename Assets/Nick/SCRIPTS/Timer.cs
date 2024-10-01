using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10f; // Set the starting time in seconds
    public bool isTimerRunning = false;
    public Text timeText;

    void Start()
    {
        // Optionally start the timer automatically when the game starts
        isTimerRunning = true;
    }

    void Update()
    {
        //if (timeRemaining <= 0)
        //{
        //    SceneManager.LoadScene("GameOver");
        //}
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrease time based on real-time seconds
                UpdateTimerDisplay(timeRemaining); // Update the UI
            }
            else
            {
                Debug.Log("Time's up!");
                timeRemaining = 0;
                SceneManager.LoadScene("GameOver");
                isTimerRunning = false; // Stop the timer when it reaches zero
            }
        }
    }

    // Optional method to update a UI text field with the timer value
    void UpdateTimerDisplay(float currentTime)
    {
        currentTime = Mathf.Clamp(currentTime, 0, Mathf.Infinity); // Ensure time is not negative

        float minutes = Mathf.FloorToInt(currentTime / 60); // Get minutes
        float seconds = Mathf.FloorToInt(currentTime % 60); // Get seconds

        // Display the timer in a mm:ss format
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
