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

    [SerializeField] private TextMeshProUGUI _cookieMonsterText;
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        // make sure that amount of lives is consistent by directly getting it from the player script
        _lives = GameObject.FindWithTag("Player").GetComponent<Player>()._lives;
        // set game over text inactive
        _cookieMonsterText.gameObject.SetActive(false);
    }
    
    void Update()
    {
        // set _lives many hearts active and the rest inactive
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

    public void GameOver()
    {
        // set game over text active
        _cookieMonsterText.gameObject.SetActive(true);
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        // enlarge cookie monster and reposition it
        player.transform.localScale = new Vector3(2f,2f,2f);
        player.transform.position = new Vector3(0f, 0.75f, 0f);
        // adjust score text color to blue and enlarge it
        _scoreText.color = new Color(0.18f, 0.56f, 1f);
        _scoreText.fontSize = 33f;
    }
}
