using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for metal cube properties
/// </summary>
public class MetalCube : MonoBehaviour
{
    /// <summary>Speed of the cube</summary>
    public float speed = 1.0f;

    /// <summary>Moves cube in specified direction on 2D plane</summary>
    public void Move2D(Quaternion direction) 
    {
        // get force vector in direction of quaternion
        Debug.Log(Vector3.forward);
        Vector3 force = direction * Vector3.forward;
        Debug.Log(force);
        // get only XZ components (2D plane)
        force = new Vector3(force.x, 0f, force.z);
        // normalize vector to 1, then multiply by speed
        if (force.sqrMagnitude > 0.0001f) force.Normalize();
        force = force * speed;

        Debug.Log(force.ToString());

        // add force to rigidbody component
        var body = GetComponent<Rigidbody>();
        body.AddForce(force);
   }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
