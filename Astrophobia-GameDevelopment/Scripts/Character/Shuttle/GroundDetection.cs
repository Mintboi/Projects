using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
       if(other.gameObject.tag == "Planet")
        {
            Debug.Log("Colliding");
        }
    }
}
