using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for metal cube properties
/// </summary>
public class MetalCube : MonoBehaviour
{
    /// <summary>Rigidbody reference</summary>
    protected Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    /// <summary>Moves cube in specified direction on 2D plane</summary>
    public void Move2D(Quaternion direction, float speed) 
    {
        // get force vector in direction of quaternion
        Vector3 force = direction * Vector3.forward;
        // get only XZ components (2D plane)
        force = new Vector3(force.x, 0f, force.z);
        // normalize vector to 1, then multiply by speed
        if (force.sqrMagnitude > 0.0001f) force.Normalize();
        force = force * speed;

        // add force to rigidbody component
        body.AddForce(force);
   }
}
