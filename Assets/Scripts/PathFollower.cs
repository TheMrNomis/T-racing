using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

    public Trajectory path;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public float targetLength = 10.0f;

    private Vector3 target;
    private int targetId;

	// Use this for initialization
	void Start () {
        targetId = path.registerTarget();
	}
	
	// Update is called once per frame
	void Update () {
        path.advanceTarget(targetId, speed * Time.deltaTime);
        target = path.getTargetPosition(targetId);

        transform.LookAt(target);
	}

    void OnDrawGizmos () {
        if (target != null)
            Gizmos.DrawSphere(target, .1f);
   } 
}
