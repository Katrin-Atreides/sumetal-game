using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keyboard movement
/// </summary>
public class KeyboardMove : MonoBehaviour
{
    /// <summary>Movement speed</summary>
    public float moveSpeed = 5.0f;
    /// <summary>Cube game object reference
    protected MetalCube cube;

    public bool isActive = false;

    void Start()
    {
        cube = GetComponent<MetalCube>();
    }
    
    void Update()
    {
        if (isActive)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) > 0.001f || Mathf.Abs(vertical) > 0.001f)
            {
                // Get camera forward vector
                var cameraDir = Camera.main.transform.rotation;

                // Normalize input and ensure non-zero direction
                Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

                // Create input rotation (always forward-based)
                Quaternion inputRotation = Quaternion.LookRotation(inputDir);

                // Combine with camera rotation
                Quaternion finalRotation = cameraDir * inputRotation;

                cube.Move2D(finalRotation, moveSpeed);
            }

        }
    }
}
