using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _heartPrefabs;
    private int _lives;
    
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score = 0;
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        _lives = GameObject.FindWithTag("Player").GetComponent<Player>()._lives;
    }

    void Update()
    {
        
        for (int i = 0; i < _heartPrefabs.Count; i++)
        {
            if (i < _lives)
            {
                _heartPrefabs[i].SetActive(true);
            }
            else
            {
                _heartPrefabs[i].SetActive(false);
            }
        }
    }
    
    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateLife(int life)
    {
        _lives -= life;
    }
}
