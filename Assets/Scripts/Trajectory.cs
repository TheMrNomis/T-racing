using UnityEngine;
using System.Collections;

public class Trajectory : MonoBehaviour {

    public Transform[] path;
    public bool isClosed = true;

    private struct target {
        public int currentId;
        public float offset;
    }

    private target[] targets = new target[] { };

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
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

    public int registerTarget()
    {
        int id = targets.Length;
        target tmp_target = new target { };
        tmp_target.currentId = 0;
        tmp_target.offset = 0.0f;

        target[] tmp_targets = (target[]) targets.Clone();
        targets = new target[tmp_targets.Length + 1];
        for (int i = 0; i < targets.Length; ++i)
        {
            if (i < tmp_targets.Length)
                targets[i] = tmp_targets[i];
            else
                targets[i] = tmp_target;
        }

        return id;
    }

    public void advanceTarget(int targetId, float offset)
    {
        if (targetId >= 0 && targetId < targets.Length)
        {
            target current_target = targets[targetId];
            if(!isClosed && current_target.currentId + 1 >= path.Length)
            {
                current_target.offset = 0.0f;
                targets[targetId] = current_target;
                return;
            } 
            Vector3 p1 = path[current_target.currentId].position;
            Vector3 p2 = path[(current_target.currentId + 1 >= path.Length) ? 0 : current_target.currentId + 1].position;

            Vector3 dir = p2 - p1;
            float absolute_offset = current_target.offset + offset;
            if(absolute_offset > dir.magnitude)
            {
                current_target.currentId++;
                if (isClosed && current_target.currentId >= path.Length)
                    current_target.currentId = 0;
                current_target.offset = 0.0f;
                targets[targetId] = current_target;
                advanceTarget(targetId, absolute_offset - dir.magnitude);
            }
            else
            {
                current_target.offset += offset;
                targets[targetId] = current_target;
            }
        }
    }

    public Vector3 getTargetPosition(int targetId)
    {
        if(targetId >= 0 && targetId < targets.Length)
        {
            target current_target = targets[targetId];
            Vector3 p1 = path[current_target.currentId].position;
            Vector3 p2 = path[(current_target.currentId + 1 >= path.Length) ? 0 : current_target.currentId + 1].position;

            Vector3 dir = (p2 - p1).normalized;
            return p1 + current_target.offset * dir;
        }
        else
        {
            return path[0].position;
        }
    }
}
