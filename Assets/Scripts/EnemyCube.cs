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
    Renderer rend;

    public Material AgMat;
    public float AgMass = 1.5f;
    public Material BiMat;
    public float BiMass = 0.979f;
    public Material CdMat;
    public float CdMass = 0.865f;
    public Material CuMat;
    public float CuMass = 0.892f;
    public Material PbMat;
    public float PbMass = 1.134f;
    public Material SnMat;
    public float SnMass = 0.479f;
    public Material TiMat; 
    public float TiMass = 0.45f;

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

    public string currentMetalName;

    protected Mode currentMode;
    protected MetalCube enemyCube;
    protected Rigidbody body;
    protected GameObject playerCube;

    public bool isActive = false;

    public void ChangeMetal()
    {
        var currentMetal = (MetalType)(Random.Range((int)MetalType.Ag, (int)MetalType.Ti + 1));

        if (currentMetal == MetalType.Ag)
        {
            currentMetalName = "Ag";
            rend.material = AgMat;
            body.mass = AgMass;
        }
        else if (currentMetal == MetalType.Bi)
        {
            currentMetalName = "Bi";
            rend.material = BiMat;
            body.mass = BiMass;
        }
        else if (currentMetal == MetalType.Cd)
        {
            currentMetalName = "Cd";
            rend.material = CdMat;
            body.mass = CdMass;
        }
        else if (currentMetal == MetalType.Cu)
        {
            currentMetalName = "Cu";
            rend.material = CuMat;
            body.mass = CuMass;
        }
        else if (currentMetal == MetalType.Pb)
        {
            currentMetalName = "Pb";
            rend.material = PbMat;
            body.mass = PbMass;
        }
        else if (currentMetal == MetalType.Sn)
        {
            currentMetalName = "Sn";
            rend.material = SnMat;
            body.mass = SnMass;
        }
        else if (currentMetal == MetalType.Ti)
        {
            currentMetalName = "Ti";
            rend.material = TiMat;
            body.mass = TiMass;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMode = (Mode)(Random.Range((int)Mode.Stalk, (int)Mode.Chase + 1));
        enemyCube = GetComponent<MetalCube>();
        body = GetComponent<Rigidbody>();
        playerCube = GameObject.FindWithTag("Player");
        rend = GetComponent<Renderer>();

        ChangeMetal();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) // if false the enemy ai won't work
        {

            // direction vector to player cube
            var playerVector = playerCube.transform.position - transform.position;
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
            if (Random.Range(0, 100) < 10)
                currentMode = Mode.Stalk;

        }
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
