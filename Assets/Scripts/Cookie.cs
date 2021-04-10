using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cookie : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _rotationSpeed = 1f;

    void Start()
    {
        transform.eulerAngles = new Vector3(0f, -90f, -90f);
    }
    void Update()
    {
        // let it move downwards
        transform.Translate(Vector3.down * (Time.deltaTime * _speed), Space.World);
        // let it rotate
        transform.Rotate(new Vector3(0f, 360f, 0f) * (Time.deltaTime * _rotationSpeed));

        // if cookie leaves the screen before being collected, it gets destroyed
        // OR: respawn at the top
        if (transform.position.y < -1.6)
        {
           // Destroy(this.gameObject);
            transform.position = new Vector3(Random.Range(-9f, 9f), y: 10f, z: 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if cookie collides with player add 5 to score and destroy cookie
        if (other.CompareTag("Player"))
        {
            // make sure that the collision with player gets only counted once
            // when umbrella is active
            if (!other.name.Contains("Umbrella"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.RelayScore(5);
                    Destroy(this.gameObject);
                }
            }
        }
        // if cookie collides with trashcan subtract 1 from the score and destroy cookie
        else if (other.CompareTag("Trashcan"))
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(-1);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
