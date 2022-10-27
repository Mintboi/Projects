using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rb;
    const float G = 667.4f;
    public float thrust;
    public static List<Attractor> Attractors;

    void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in Attractors)
        {
            if(attractor != this)
            {
                Attract(attractor);
            }
        }
    }
    void OnEnable()
    {
        Rigidbody rg = this.GetComponent<Rigidbody>();
        rg.AddForce(transform.forward * (thrust * rb.mass), ForceMode.Impulse);
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    void OnDisable()
    {
        Attractors.Remove(this);
    }
    void Attract(Attractor objWithMass)
    {
        Rigidbody rbToAttract = objWithMass.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;
        if(distance == 0f)
        {
        return;
        }
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);

    }
}
