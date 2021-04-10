using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int _score = 0;

    [SerializeField] private TextMeshProUGUI _scoreText;
    
    void Start()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }
}
