using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float minSpin = 1f;
    public float maxSpin = 5f;
    public float minThrust = 0.1f;
    public float maxThrust = 5.0f;
    public float spinSpeed;
    public float thrust;
    void Start()
    {
        spinSpeed = Random.Range(minSpin, maxSpin);
        thrust = Random.Range(minThrust, maxThrust);

        Rigidbody rg = this.GetComponent<Rigidbody>();
        rg.AddForce(transform.forward * thrust, ForceMode.Impulse);

    }

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
