using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startInstruction;
    void Start()
    {
        _startInstruction.gameObject.SetActive(false);
    }

    private void Update()
    {
        new WaitForSeconds(3f);
        
        _startInstruction.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // change to Game scene (index 0 is Start Scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
