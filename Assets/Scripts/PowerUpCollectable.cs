using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        
=======
        // let it move downwards
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        
        // destroy if out of screen
        if (transform.position.y < -1.6f)
        {
            Destroy(this.gameObject);
        }
>>>>>>> 7e9b3b9 (added PowerUp Behaviour)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
