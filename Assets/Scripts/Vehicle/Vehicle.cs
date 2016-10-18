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
        m_vRigidBody.velocity = new Vector3(1.0f, 0, 0);
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(KeyCode.LeftArrow))
             m_acceleration += new Vector3(-.1f,0,0);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            m_acceleration += new Vector3(.1f, 0, 0);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            m_acceleration += new Vector3(0, 0, -.1f);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            m_acceleration += new Vector3(0, 0, 0.1f);

        m_vRigidBody.velocity += m_acceleration;
    }

    public Vector3 GetVelocity() { return m_vRigidBody.velocity; }
}
