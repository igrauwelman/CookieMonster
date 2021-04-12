using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _cookiePrefab;
    [SerializeField] private List<GameObject> _trashPrefabs;
    [SerializeField] private List<GameObject> _collectablePrefabs;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _collectableSpawnRate = 15f;
    private bool _spawningOn = true;
    public bool _isMonsterOn = false;
    
    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnCollectables());
        _isMonsterOn = false;
    }
    
    public void ONPlayerDeath()
    {
        _spawningOn = false;
    }

    IEnumerator SpawnSystem()
    {
        
        while (_spawningOn)
        {
            if (!_isMonsterOn)
            {
                Instantiate(_cookiePrefab, new Vector3(Random.Range(-8.3f, 8.3f), y: 10f, z: 0f),
                    Quaternion.identity, this.transform);
                Instantiate(_trashPrefabs[(int) Random.Range(0f, _trashPrefabs.Count)], new Vector3(Random.Range(-8.3f, 8.3f), y: 10f, z: 0f), Quaternion.identity, this.transform);
            }
            else
            {
                while (_isMonsterOn)
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
        {//Random.Range(0f, _collectablePrefabs.Count)
            if (!_isMonsterOn)
            {
                Instantiate(_collectablePrefabs[(int) 2],
                    new Vector3(Random.Range(-9f, 9f), 10f, 0f), Quaternion.identity, this.transform);
            
                yield return new WaitForSeconds(_collectableSpawnRate);
            }
        }
    }
}
