using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _cookiePrefab;
    [SerializeField] private List<GameObject> _trashPrefabs;
    [SerializeField] private List<GameObject> _collectablePrefabs;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private float _collectableSpawnRate = 15f;
    private bool _spawningOn = true;
    
    void Start()
    {
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnCollectables());
    }
    
    public void ONPlayerDeath()
    {
        _spawningOn = false;
    }

    IEnumerator SpawnSystem()
    {
        while (_spawningOn)
        {
            Instantiate(_cookiePrefab, new Vector3(Random.Range(-8.3f, 8.3f), y: 10f, z: 0f),
                Quaternion.identity, this.transform);

            Instantiate(_trashPrefabs[(int) Random.Range(0f, _trashPrefabs.Count)], new Vector3(Random.Range(-8.3f, 8.3f), y: 10f, z: 0f), Quaternion.identity, this.transform);

            yield return new WaitForSeconds(_delay);
        }
    }

    IEnumerator SpawnCollectables()
    {
        while (_spawningOn)
        {
            Instantiate(_collectablePrefabs[(int) Random.Range(0f, _collectablePrefabs.Count)],
                new Vector3(Random.Range(-9f, 9f), 6.5f, 0f), Quaternion.identity, this.transform);
            
            yield return new WaitForSeconds(_collectableSpawnRate);
        }
    }
}
