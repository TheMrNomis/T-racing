using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour
{

    //private Animator animator;

	// Use this for initialization
	void Start ()
    {
        //animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Esc input !");
            gameObject.GetComponent<Animator>().SetTrigger("Open");
            gameObject.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}
