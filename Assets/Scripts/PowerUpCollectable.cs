using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectable : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    void Start()
    {
        if (this.gameObject.name.Contains("Monster"))
        {
            transform.eulerAngles = new Vector3(0f, -90f, -90f);
        }
    }
    void Update()
    {
        // let it move downwards
        transform.Translate(Vector3.down * (_speed * Time.deltaTime), Space.World);
        
        // destroy if out of screen
        if (transform.position.y < -1.6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.ActivatePowerUp(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
