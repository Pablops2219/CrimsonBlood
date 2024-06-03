using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private PlayerController playerController;
    public TextMeshProUGUI   scoreText;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("PlayerController not found in the scene.");
        }

        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.LogError("TextMeshPro component not found on the GameObject.");
        }
    }

    void FixedUpdate()
    {
        string puntuacion = playerController.getPuntuacion().ToString();
        scoreText.SetText(puntuacion);
    }
}