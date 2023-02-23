using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasshopper : MonoBehaviour
{
    
    public GameObject target;
    public float timeBuffer = 1f;
    public float launchForce = 10f;
    Rigidbody rb;
    
    Vector3 startingPosition;
    Vector3 targetStartingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        targetStartingPosition = target.transform.position;
        Time.timeScale = timeBuffer; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //f to fire
        if (Input.GetKeyDown(KeyCode.F))
        {
            FiringSolution firing = new FiringSolution();
            Debug.Log("In the Space function");
            Nullable<Vector3> aimVector = firing.calculateFiringSolution(transform.position, target.transform.position, launchForce, Physics.gravity);
            if (aimVector.HasValue)
                rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
        }

        //r to reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = true;
            transform.position = startingPosition;
            rb.isKinematic = false;

            target.GetComponent<Rigidbody>().isKinematic = true;
            target.transform.position = targetStartingPosition;
            target.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}