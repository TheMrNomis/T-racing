using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

    public Trajectory path;
    public Transform wheelL;
    public Transform wheelR;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public float targetLength = 10.0f;

    public float maxSpeed = 50.0f;

    private Vector3 target;
    private int targetId;

    private Vector3 currentMotion;

	// Use this for initialization
	void Start () {
        targetId = path.registerTarget();
        currentMotion = Vector3.zero;
	}
    
    // Update is called once per frame
    void Update () {
        path.advanceTarget(targetId, speed * Time.deltaTime);
        target = path.getTargetPosition(targetId);

        float wheelAngle = Vector3.Angle(Vector3.forward, target);
        if(wheelL != null && wheelR != null)
        {
            wheelL.LookAt(target);
            wheelR.LookAt(target);
        }
        //transform.LookAt(target, Vector3.up);

        //transform.Translate(new Vector3(0, 0, 0.9f * speed * Time.deltaTime));
	}

    void OnDrawGizmos () {
        if (target != null)
        {
            Gizmos.DrawSphere(target, .1f);
            Gizmos.DrawRay(transform.position, target - transform.position);
        }
   } 
}
