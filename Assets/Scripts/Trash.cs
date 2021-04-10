using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trash : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _rotationSpeed = 1f;

    void Update()
    {
        // let it move downwards
        transform.Translate(Vector3.down * (Time.deltaTime * _speed));
        // let it rotate
        transform.Rotate(new Vector3(0f, 360f, 0f) * (Time.deltaTime * _rotationSpeed));
        
        // respawn if out of screen
        if (transform.position.y < -1.6f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), y:10f, z: 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if trash collides with trashcan it gets destroyed
        if (other.CompareTag("Trashcan"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            // add 3 points for can1 and garlic
            if (this.name.Contains("Can1") || this.name.Contains("Garlic"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(3);
            }
            // add 2 points for can3
            else if (this.name.Contains("Can3"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(2);
            }
            // add 1 point for can2, cucumber and zucchini
            else if (this.name.Contains("Can2") || this.name.Contains("Cucumber") || this.name.Contains("Zucchini"))
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(1);
            }
        }
        // if trash collides with player they get damaged and trash gets destroyed
        // (monster "ate" it)
        else if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
