using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalBodyDetector : MonoBehaviour
{
    private GameObject Planet;
    public int PlanetRadius;
    public bool planetVisited;

    void Start()
    {
    Planet = GameObject.FindWithTag("Gravitational Body");
    } 
    void Update()
        {
            DetectGravity();
        }
    private void DetectGravity()
    {

        var Shuttle = this.transform;

        if (Vector3.Distance(Shuttle.position, Planet.transform.position) < PlanetRadius)
        {
            PlanetChecker();
        }



         void PlanetChecker()
        {
            if (planetVisited != true)
            {
                Vector3 GravityUp = (transform.position - Planet.transform.position).normalized;
                Vector3 bodyUp = transform.up;

                Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, GravityUp) * transform.rotation;
                Shuttle.rotation = Quaternion.Slerp(Shuttle.rotation, targetRotation, 50 * Time.deltaTime);
            }
            else
            {
                Debug.Log("Visited");
            }

        }
    }
}
