using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    Stalk,
    Chase
}

public class EnemyCube : MonoBehaviour
{
    // Enemy AI
    //
    // modes:
    // - stalk - try to keep distance from player without falling down
    // - chase - chase player around
    //
    // modes change at random
    // add random jitters as if cube is controlled by a person

    public float stalkDistance = 10.0f;
    public float moveSpeed = 8.0f;
    public float edgeDistanceCheckCoef = 1.0f;

    public Material AgMat;
    public float AgMass;
    public float AgSpeed;
    public Material BiMat;
    public float BiMass;
    public float BiSpeed;
    public Material CdMat;
    public float CdMass;
    public float CdSpeed;
    public Material CuMat;
    public float CuMass;
    public float CuSpeed;
    public Material PbMat;
    public float PbMass;
    public float PbSpeed;
    public Material SnMat;
    public float SnMass;
    public float SnSpeed;
    public Material TiMat; 
    public float TiMass;
    public float TiSpeed;

    public MetalType currentMetal;
    public string currentMetalName;

    protected Mode currentMode;
    protected MetalCube enemyCube;
    protected Rigidbody body;
    protected GameObject playerCube;

    enum MetalType
    {
        Ag,
        Bi,
        Cd,
        Cu,
        Pb,
        Sn,
        Ti
    }

    public void ChangeMetal()
    {
        var currentMetal = (MetalType)(Random.Range((int)MetalType.Ag, (int)MetalType.Ti + 1);

        if (currentMetal == MetalType.Ag)
        {
            currentMetalName = "Ag";
            renderer.material = AgMat;
            body.mass = AgMass;
            moveSpeed = AgSpeed;
        }
        else if (currentMetal == MetalType.Bi)
        {
            currentMetalName = "Bi";
            renderer.material = BiMat;
            body.mass = BiMass;
            moveSpeed = BiSpeed;
        }
        else if (currentMetal == MetalType.Cd)
        {
            currentMetalName = "Cd";
            renderer.material = CdMat;
            body.mass = CdMass;
            moveSpeed = CdSpeed;
        }
        else if (currentMetal == MetalType.Cu)
        {
            currentMetalName = "Cu";
            renderer.material = CuMat;
            body.mass = CuMass;
            moveSpeed = CuSpeed;
        }
        else if (currentMetal == MetalType.Pb)
        {
            currentMetalName = "Pb";
            renderer.material = PbMat;
            body.mass = PbMass;
            moveSpeed = PbSpeed;
        }
        else if (currentMetal == MetalType.Sn)
        {
            currentMetalName = "Sn";
            renderer.material = SnMat;
            body.mass = SnMass;
            moveSpeed = SnSpeed;
        }
        else if (currentMetal == MetalType.Ti)
        {
            currentMetalName = "Ti";
            renderer.material = TiMat;
            body.mass = TiMass;
            moveSpeed = TiSpeed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMetal();

        currentMode = (Mode)(Random.Range((int)Mode.Stalk, (int)Mode.Chase + 1));
        enemyCube = GetComponent<MetalCube>();
        body = GetComponent<Rigidbody>();
        playerCube = GameObject.FindGameObjectsWithTag("Player")[0]; 
    }

    // Update is called once per frame
    void Update()
    {
        // direction vector to player cube
        var playerVector = playerCube.transform.position - enemyCube.transform.position;
        var distance = playerVector.magnitude;
        var direction = playerVector.normalized;

        var velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z);
        
        // edge check
        // send ray down from calculated position - if no collision - retreat
        if (!CastRayDownward(body.transform.position + velocity * edgeDistanceCheckCoef))
        {
            // if player won't be hit - move away from the edge
            if (!WillHitPlayer(transform.position, body.velocity.normalized)) 
            {
                enemyCube.Move2D(Quaternion.LookRotation(velocity.normalized * -1), moveSpeed);
            }
        }
        // if no edge detected - continue as normal
        else if (currentMode == Mode.Stalk)
        {
            if (distance <= stalkDistance)
            {
                // move away
                direction = direction * -1;
                enemyCube.Move2D(Quaternion.LookRotation(direction), moveSpeed);
            }
            else 
            {
                // switch mode to chase
                currentMode = Mode.Chase;
            }
        }
        else if (currentMode == Mode.Chase)
        {
            // move as close as possible
            enemyCube.Move2D(Quaternion.LookRotation(direction), moveSpeed);
        }

        // 20% chance to change mode to stalk
        if (Random.Range(0, 100) < 20)
            currentMode = Mode.Stalk;
    }

    bool CastRayDownward(Vector3 origin)
    {
        // Cast the ray downward from the predefined position
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, 10.0f, ~0))
        {
           // Visualize the ray (red if hit)
            //Debug.DrawRay(origin, Vector3.down * hit.distance, Color.red);
            return true;
        }
        else
        {
            // Visualize the ray (green if no hit)
            //Debug.DrawRay(origin, Vector3.down * 10.0f, Color.green);
            return false;
        }
    }

    bool WillHitPlayer(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        // Cast a sphere along the ray
        if (Physics.SphereCast(origin, 0.5f, direction, out hit, 30.0f, ~0))
        {
            // Debug.Log($"Hit object: {hit.collider.gameObject.name} with sphere cast");
            // Debug.DrawLine(origin, hit.point, Color.red);
            // 
            // // Draw the sphere at impact point
            // Debug.DrawRay(hit.point, Vector3.up * 0.5f, Color.yellow);
            // Debug.DrawRay(hit.point, Vector3.right * 0.5f, Color.yellow);
            // Debug.DrawRay(hit.point, Vector3.forward * 0.5f, Color.yellow);

            // Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Player")
                return true;
            else
                return false;
        }
        else
        {
            //Debug.DrawRay(origin, direction * 30.0f, Color.green);
            return false;
        }
    }
}
