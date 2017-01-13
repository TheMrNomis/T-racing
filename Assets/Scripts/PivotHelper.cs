using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotHelper : MonoBehaviour {

    public float gizmoSize = .75f;
    public Color gizmoColor = Color.yellow;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
