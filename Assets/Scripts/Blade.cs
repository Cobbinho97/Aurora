using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float rotateSpeed;

    public float speed;


    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);

    }
}
