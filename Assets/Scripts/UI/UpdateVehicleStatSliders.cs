using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateVehicleStatSliders : MonoBehaviour {

    //This is a reference of a script which contains all the stats of the vehicle
    public VehicleStats vehicleStats;

    //This is all the sliders to modify with the stats
    public Slider weightSlider;
    public Slider speedSlider;
    public Slider accelerationSlider;

    // Use this for initialization
    void Start ()
    {
        weightSlider.value = vehicleStats.weight;
        speedSlider.value = vehicleStats.speed;
        accelerationSlider.value = vehicleStats.acceleration;
    } 
}
