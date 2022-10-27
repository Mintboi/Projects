using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitTarget; // Assign the sphere/planet you want to orbit
    public float speed; // how fast the ship is moving
    public float distance; // how far the ship will orbit
    public bool updateDistance; // check this if you make changes to distance so the dummy's position will be updated

    private Transform _directionDummy;

    private void Start()
    {
        InitializeDummy();
    }

    private void InitializeDummy()
    {
        // Create a dummy object for the ship to look at, and position the dummy accordingly
        if (_directionDummy == null)
        {
            _directionDummy = new GameObject("_dummy").GetComponent<Transform>();
            _directionDummy.parent = orbitTarget;
        }

        _directionDummy.position = orbitTarget.position;
        _directionDummy.rotation = Quaternion.identity;
        _directionDummy.localScale = Vector3.one;
        _directionDummy.Translate(new Vector3(0, 0, -distance));
    }

    private void Update()
    {
        UpdateDummy();

        // Move the ship by following the dummy object and make it bottom side always face the sphere
        Vector3 _worldUp = transform.position - orbitTarget.position;
        transform.LookAt(_directionDummy, _worldUp * 2);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        if (Time.frameCount % 30 == 0)
            Debug.Log(Vector3.Distance(transform.position, _directionDummy.position));
    }

    private void UpdateDummy()
    {
        // Move the dummy around the sphere
        if (updateDistance)
        {
            updateDistance = false;
            Start();
        }

        _directionDummy.LookAt(orbitTarget, orbitTarget.up);
        _directionDummy.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }
}
