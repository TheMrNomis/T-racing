using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

    public Trajectory path;
    public Transform wheelL;
    public Transform wheelR;

    public PathFollower[] adversaries;

    public float mass = 1.0f;
    public float maxSpeed = 20.0f;
    public float maxForce = 20.0f;

    public bool drawBoundingSphere = false;
    public float boundingSphereRadius = 0.0f;
    public bool drawTarget = false;
    public bool drawVelocity = false;
    public bool drawPathPosition = false;
    public bool drawAdversaries = false;

    private Vector3 target;

    private Vector3 velocity;
    private Vector3 acceleration;
    private Vector3 steering;

	// Use this for initialization
	void Start () {
        velocity = Vector3.zero;
        target = Vector3.zero;
	}
    
    // Update is called once per frame
    void Update () {
        steering = new Vector3(0, 0, 0);

        computePathSteering();
        computeAcceleration();

        velocity = truncate(velocity + acceleration * Time.deltaTime, maxSpeed);
        transform.LookAt(transform.position + velocity);
        transform.Translate(velocity.magnitude * Vector3.forward * Time.deltaTime);

        updateWheel();
	}

    Vector3 truncate(Vector3 vec, float maxMagnitude)
    {
        if (vec.magnitude > maxMagnitude)
            vec = vec.normalized * maxMagnitude;
        return vec;
    }

    void computePathSteering()
    {
        target = path.getTargetPosition(transform.position + velocity, 2.0f);
        Vector3 desiredVelocity = (target - transform.position).normalized * maxSpeed;
        steering += desiredVelocity - velocity;
    }

    void computeAvoidanceSteering()
    {
        for(int i = 0; i < adversaries.Length; ++i)
            if(adversaries[i] != null)
            {

            }
    }

    void computeAcceleration ()
    {
        Vector3 steerforce = truncate(steering, maxForce);
        acceleration = steerforce / mass;
    }

    void updateWheel()
    {
        if (wheelL != null && wheelR != null)
        {
            wheelL.LookAt(transform.position + acceleration);
            wheelR.LookAt(transform.position + acceleration);
        }
    }

    void OnDrawGizmos () {
        if(drawBoundingSphere)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, boundingSphereRadius);
        }

        if (drawTarget)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(target, 1f);
            Gizmos.DrawRay(transform.position, target - transform.position);
        }

        if (drawVelocity)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, velocity);
            Gizmos.DrawSphere(transform.position + velocity, 1f);
        }

        if (drawPathPosition)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, path.getTargetPosition(transform.position, 0.0f) - transform.position);
        }

        if (drawAdversaries)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < adversaries.Length; ++i)
                if (adversaries[i] != null)
                    Gizmos.DrawRay(transform.position, adversaries[i].transform.position - transform.position);
        }
    } 
}
