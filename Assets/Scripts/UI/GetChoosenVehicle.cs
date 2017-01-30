using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetChoosenVehicle : MonoBehaviour
{
    //This is the sliders of the GameUI vehicle stats panel
    public Slider weightSlider;
    public Slider speedSlider;
    public Slider accelerationSlider;

    //This panel is the one which contains the toggle group
    public GameObject selectionPanel;

    // This function is called when the user clicked on the button
    public void OnClick ()
    {
        //Initialisation of the panel
        GameObject choosenPanel = gameObject;

        //We get the selected panel
        Toggle[] toggles = selectionPanel.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < toggles.Length; ++i)
        {
            if (toggles[i].isOn)
            {
                choosenPanel = toggles[i].gameObject;
                break;
            }
        }

        //We update statistics
        VehicleStats vehicleStats = choosenPanel.GetComponent<VehicleStats>();
        weightSlider.value = vehicleStats.weight;
        speedSlider.value = vehicleStats.speed;
        accelerationSlider.value = vehicleStats.acceleration;

        updatePlayerPrefs(vehicleStats);
    }
	
	// We change the player prefs
	void updatePlayerPrefs (VehicleStats vehicleStats)
    {
        PlayerPrefs.SetFloat("weight", vehicleStats.weight);
        PlayerPrefs.SetFloat("speed", vehicleStats.speed);
        PlayerPrefs.SetFloat("acceleration", vehicleStats.acceleration);
	}
}
