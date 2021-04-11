using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectable : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        // let it move downwards
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        
        // destroy if out of screen
        if (transform.position.y < -1.6f)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
