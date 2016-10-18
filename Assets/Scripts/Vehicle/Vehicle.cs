using UnityEngine;
using System.Collections;


public enum EState
{
      Idle
    , Acceleration
    , Break
}

public class Vehicle : MonoBehaviour {

    /// <summary>
    /// The RigidBody, the component that manages weight & 
    /// the velocity of the Vehicle
    /// </summary>
    public Rigidbody m_vRigidBody;
    public Vector3 m_acceleration;


	// Use this for initialization
	void Start () {
        m_vRigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        m_vRigidBody.velocity += m_acceleration;
        Debug.Log("Velocity : " + m_vRigidBody.velocity.ToString());

    }

    public Vector3 GetVelocity() { return m_vRigidBody.velocity; }
}
