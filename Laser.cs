﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _laserspeed = 15.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserMovement();
    }
    void LaserMovement()
    {
        transform.Translate(Vector3.up * _laserspeed * Time.deltaTime);
        if (transform.position.y > 6)
        {
            
            Destroy(this.gameObject);
        }
    }
    
}
