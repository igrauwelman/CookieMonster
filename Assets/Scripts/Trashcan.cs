using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trashcan : MonoBehaviour
{
   [SerializeField] private float _speed = 5f;

    
    void Update()
    {
        // "normal" trashcan
            if (name.Contains("Trashcan"))
            { 
                // let it move upwards
                transform.Translate(Vector3.up * (Time.deltaTime * _speed));
                // let it rotate a bit 
                transform.Rotate(new Vector3(0f, Random.Range(-45f,45f), 0f) * (Time.deltaTime * _speed * 2f), Space.Self);
            }
            // container powerUp
            else if (name.Contains("Container"))
            {
                transform.Translate(Vector3.up * (Time.deltaTime * _speed * 2f));
            }

            // destroy if out of screen
            if (transform.position.y > 10f)
            {
                Destroy(this.gameObject);
            }
    }
    
}
