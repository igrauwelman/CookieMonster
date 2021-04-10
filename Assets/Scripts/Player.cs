using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;

    [Header("External Components")] 
    [SerializeField] private GameObject _cookiePrefab;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameObject _trashcanPrefab;
    [SerializeField] private UIManager _uiManager;

    [Header("Player Settings")]
    [SerializeField] private int _lives = 4;
    [SerializeField] private float _shootingRate = 0.3f;
    private float _canShoot = -1f;

    void Start()
    {
        transform.position = new Vector3(0f, 0.5f, 0f);
    }
    
    void Update()
    {
        PlayerMovement();
        Shoot();
    }

    void PlayerMovement()
    {
        // setting up the movement (with input)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //transform.GetChild(0).Rotate(new Vector3(0f, horizontalInput * Time.deltaTime * _speed * 2f, 0f), Space.Self);
        transform.Translate(Vector3.right * (Time.deltaTime * _speed * horizontalInput));
        transform.Translate(Vector3.up * (Time.deltaTime * _speed * verticalInput));

        // setting up the boundaries
        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(transform.position.x, y: 4.5f, z: transform.position.z);
        }
        else if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, y: 0f, z: transform.position.z);
        }

        if (transform.position.x > 9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, transform.position.z);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canShoot)
        {
            _canShoot = Time.time + _shootingRate;
            Instantiate(_trashcanPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives -= 1;

        if (_lives == 0)
        {
            if (_spawnManager != null)
            {
                _spawnManager.ONPlayerDeath();
            }
            else
            {
                Debug.LogError("SpawnManager not assigned!");
            }
            Destroy(this.gameObject);
            foreach (Transform child in _spawnManager.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    public void RelayScore(int score)
    {
        _uiManager.AddScore(score);
    }
}
