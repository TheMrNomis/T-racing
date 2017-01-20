using UnityEngine;
using System.Collections;

public class VehicleStats : MonoBehaviour
{
    public float weight;
    public float speed;
    public float acceleration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setWeight(float newWeight)
    {
        weight = newWeight;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void setAcceleration(float newAcceleration)
    {
        acceleration = newAcceleration;
    }
}
