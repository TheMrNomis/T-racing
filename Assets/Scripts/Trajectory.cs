using UnityEngine;
using System.Collections;

public class Trajectory : MonoBehaviour {

    public Transform[] path;
    public bool isClosed = true;

    private struct target {
        public int currentId;
        public float offset;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (path.Length > 0)
            for (int i = 0; i < path.Length; ++i)
            {
                if (!isClosed && i + 1 == path.Length)
                    continue;
                Transform p1 = path[i];
                Transform p2 = (i + 1 < path.Length) ? path[i + 1] : path[0];

                if (p1 != null && p2 != null)
                    Gizmos.DrawRay(p1.position, p2.position - p1.position);
            }
    }

    public Vector3 getTargetPosition(Vector3 point, float offset)
    {
        return getTargetPosition(advanceTarget(getClosestPointOnTheTrack(point), offset));
    }

    private Vector3 getTargetPosition(target currentTarget)
    {
        Vector3 p1 = path[currentTarget.currentId].position;
        Vector3 p2 = path[(currentTarget.currentId + 1 >= path.Length) ? 0 : (currentTarget.currentId + 1)].position;

        Vector3 dir = (p2 - p1).normalized;
        return p1 + currentTarget.offset * dir;
    }

    private target advanceTarget(target currentTarget, float offset)
    {
        if (!isClosed && currentTarget.currentId + 1 >= path.Length)
        {
            currentTarget.offset = 0.0f;
            currentTarget.currentId = path.Length - 1;
            return currentTarget;
        }
        
        Vector3 p1 = path[currentTarget.currentId].position;
        Vector3 p2 = path[(currentTarget.currentId + 1 >= path.Length) ? 0 : (currentTarget.currentId + 1)].position;

        Vector3 dir = p2 - p1;
        float absolute_offset = currentTarget.offset + offset;
        if (absolute_offset > dir.magnitude)
        {
            currentTarget.currentId++;
            if (isClosed && currentTarget.currentId >= path.Length)
                currentTarget.currentId = 0;
            currentTarget.offset = 0.0f;
            currentTarget = advanceTarget(currentTarget, absolute_offset - dir.magnitude);
        }
        else
        {
            currentTarget.offset += offset;
        }
        return currentTarget;
    }

    private target getClosestPointOnTheTrack(Vector3 point)
    {
        float minDistanceToLine = float.MaxValue;
        target bestTarget = new target { currentId = 0, offset = 0 };

        for(int i = 0; i < path.Length; ++i)
        {
            Ray trackPortion;
            float trackPortionLength;
            if (i + 1 >= path.Length)
            {
                if (isClosed)
                {
                    Vector3 direction = path[0].position - path[i].position;
                    trackPortion = new Ray(path[i].position, direction);
                    trackPortionLength = direction.magnitude;
                }
                else
                    continue;
            }
            else
            {
                Vector3 direction = path[i + 1].position - path[i].position;
                trackPortion = new Ray(path[i].position, direction);
                trackPortionLength = direction.magnitude;
            }

            float distanceToLine = Vector3.Cross(trackPortion.direction, point - trackPortion.origin).magnitude;
            float offset = Vector3.Dot(trackPortion.direction, point - trackPortion.origin);
            //ensuring that we stay in the track portion
            if (offset < 0)
                offset = 0;
            else if (offset > trackPortionLength)
                offset = trackPortionLength;

            //recomputing the distance to the line if the offset has been changed
            distanceToLine = ((trackPortion.origin + offset * trackPortion.direction) - point).magnitude;
            if (distanceToLine < minDistanceToLine)
            {
                minDistanceToLine = distanceToLine;
                bestTarget = new target {currentId = i, offset = offset};
            }
        }

        return bestTarget;
    }
}
