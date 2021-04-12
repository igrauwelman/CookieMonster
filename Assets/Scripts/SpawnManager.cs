using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _cookiePrefab;
    [SerializeField] private List<GameObject> _trashPrefabs;
    [SerializeField] private List<GameObject> _collectablePrefabs;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _collectableSpawnRate = 15f;
    private bool _spawningOn = true;
    public bool isCookieOn = false;
    
    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnCollectables());
        isCookieOn = false;
    }
    
    public void ONPlayerDeath()
    {
        _spawningOn = false;
    }

    IEnumerator SpawnSystem()
    {
        
        while (_spawningOn)
        {
            // if cookie power up is not active instantiate (randomly decided) 1 to 5 cookies and 1 trash prefab
            // (cookies not collected get destroyed, trash respawns at the top)
            if (!isCookieOn)
            {
                for (int i = 1; i < (int) Random.Range(2f,5f); i++)
                {
                    // randomize position of y so that the cookies are not in one line
                    Instantiate(_cookiePrefab, new Vector3(Random.Range(-8.3f, 8.3f), y: Random.Range(10f,12f), z: 0f), Quaternion.identity, this.transform);
                }
                Instantiate(_trashPrefabs[(int) Random.Range(0f, _trashPrefabs.Count)], new Vector3(Random.Range(-8.3f, 8.3f), y: 10f, z: 0f), Quaternion.identity, this.transform);
            }
            // if cookie power up is active, instantiate cookie prefabs over the width of the screen repeatedly as long as it is active
            else
            {
                while (isCookieOn)
                {
                    Instantiate(_cookiePrefab, new Vector3(-8f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(-6f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(-4f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(-2f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(-0f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(2f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(4f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(6f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    Instantiate(_cookiePrefab, new Vector3(8f, y: 10f, z: 0f), Quaternion.identity, this.transform);
                    
                    yield return new WaitForSeconds(_delay * 0.2f);
                }
            }
            
            yield return new WaitForSeconds(_delay);
                
        }
            
    }

    IEnumerator SpawnCollectables()
    {
        while (_spawningOn)
        {
            // if cookie power up is active no other collectables should spawn
            if (!isCookieOn)
            {
                Instantiate(_collectablePrefabs[(int) Random.Range(0f, _collectablePrefabs.Count)], new Vector3(Random.Range(-9f, 9f), 10f, 0f), Quaternion.identity, this.transform);
            
                yield return new WaitForSeconds(_collectableSpawnRate);
            }
        }
    }
}
