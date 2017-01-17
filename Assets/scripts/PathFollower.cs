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
        //computeAvoidanceSteering();
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
                Vector3 adversaryToUs = transform.position - adversaries[i].transform.position;
                if (adversaryToUs.magnitude < 0.8 * (adversaries[i].boundingSphereRadius + boundingSphereRadius))
                {
                    steering += (adversaryToUs.normalized / Mathf.Max(adversaryToUs.magnitude, 0.01f)) * 2.0f;
                }
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

    /**
     * @param u: vector 1
     * @param v: vector 2
     * @param w: vector from u.P0 to v.P0
    **//*
    Vector3 dist3D_Segment_to_Segment(Vector3 u, Vector3 v, Vector3 w)
    {
        float SMALLNUM = 1e-5f;
        float a = Vector3.Dot(u, u);         // always >= 0
        float b = Vector3.Dot(u, v);
        float c = Vector3.Dot(v, v);         // always >= 0
        float d = Vector3.Dot(u, w);
        float e = Vector3.Dot(v, w);
        float D = a * c - b * b;        // always >= 0
        float sc, sN, sD = D;       // sc = sN / sD, default sD = D >= 0
        float tc, tN, tD = D;       // tc = tN / tD, default tD = D >= 0

        // compute the line parameters of the two closest points
        if (D < SMALLNUM)
        { // the lines are almost parallel
            sN = 0.0f;         // force using point P0 on segment S1
            sD = 1.0f;         // to prevent possible division by 0.0 later
            tN = e;
            tD = c;
        }
        else
        {                 // get the closest points on the infinite lines
            sN = (b * e - c * d);
            tN = (a * e - b * d);
            if (sN < 0.0)
            {        // sc < 0 => the s=0 edge is visible
                sN = 0.0f;
                tN = e;
                tD = c;
            }
            else if (sN > sD)
            {  // sc > 1  => the s=1 edge is visible
                sN = sD;
                tN = e + b;
                tD = c;
            }
        }

        if (tN < 0.0)
        {            // tc < 0 => the t=0 edge is visible
            tN = 0.0f;
            // recompute sc for this edge
            if (-d < 0.0)
                sN = 0.0f;
            else if (-d > a)
                sN = sD;
            else
            {
                sN = -d;
                sD = a;
            }
        }
        else if (tN > tD)
        {      // tc > 1  => the t=1 edge is visible
            tN = tD;
            // recompute sc for this edge
            if ((-d + b) < 0.0)
                sN = 0;
            else if ((-d + b) > a)
                sN = sD;
            else
            {
                sN = (-d + b);
                sD = a;
            }
        }
        // finally do the division to get sc and tc
        sc = (float) (Mathf.Abs(sN) < SMALLNUM ? 0.0 : sN / sD);
        tc = (float) (Mathf.Abs(tN) < SMALLNUM ? 0.0 : tN / tD);

        // get the difference of the two closest points
        Vector3 dP = w + (sc * u) - (tc * v);  // =  S1(sc) - S2(tc)

        return dP;   // return the closest distance
    }*/
}
