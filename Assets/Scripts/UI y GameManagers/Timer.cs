using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    private float timeElapsed = 0f; // Time elapsed in seconds
    [SerializeField] private TextMeshProUGUI timerText; // Use TextMeshProUGUI for TextMeshPro
    private bool timerIsRunning = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // Optionally start the timer automatically
        StartTimer();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            // Converts the time to minutes and seconds format
            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void ResetTimer()
    {
        timerIsRunning = false;
        timeElapsed = 0f;
        UpdateTimerText();
    }

    public void SetTimerText(TextMeshProUGUI newTimerText)
    {
        timerText = newTimerText;
        UpdateTimerText(); // Update text immediately
    }
}