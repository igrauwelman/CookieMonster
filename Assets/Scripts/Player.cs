using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    private float _canShoot = -1f;
    private bool _isContainerOn = false;
    private bool _instantiateUmbrella = false;
    private bool _isUmbrellaOn = false;

    [Header("External Components")] 
    [SerializeField] private GameObject _cookiePrefab;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameObject _trashcanPrefab;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _containerPowerUpPrefab;
    [SerializeField] private GameObject _umbrellaPowerUpPrefab;
    [SerializeField] private float _powerUpTimeout = 5f;

    [Header("Player Settings")]
    [SerializeField] private int _lives = 4;
    [SerializeField] private float _shootingRate = 0.3f;

    void Start()
    {
        transform.position = new Vector3(0f, 0.5f, 0f);
        _isContainerOn = false;
        _instantiateUmbrella = false;
        _isUmbrellaOn = false;
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

        if (_isContainerOn)
        {
            Instantiate(_containerPowerUpPrefab, new Vector3(10.53f, -2f, 0f), Quaternion.identity);
            _isContainerOn = false;
        }

        if (_instantiateUmbrella)
        {
            Instantiate(_umbrellaPowerUpPrefab, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity, this.gameObject.transform);
            _instantiateUmbrella = false;
        }
    }

    public void Damage()
    {
        // if umbrella is activated it gets now deactivated but player does not lose lives
        if (_isUmbrellaOn)
        {
            _isUmbrellaOn = false;
        }
        // else player gets damaged and loses 1 life
        else
        {
            _lives -= 1;
        }

        // if no lives are left spawning is turned off and remaining objects are destroyed
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

    public void ActivatePowerUp(GameObject powerUp)
    {
        if (powerUp.name.Contains("Container"))
        {
            _isContainerOn = true;
        }

        if (powerUp.name.Contains("Umbrella"))
        {
            _instantiateUmbrella = true;
            _isUmbrellaOn = true;
        }

        StartCoroutine(DeactivatePowerUp());
    }

    IEnumerator DeactivatePowerUp()
    {
        yield return new WaitForSeconds(_powerUpTimeout);
        _isUmbrellaOn = false;
    }
}

