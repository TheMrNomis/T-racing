﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInputHor : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

	// Use this for initialization
	void Start ()
    {
        buttonSelected = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetAxisRaw("Horizontal") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}