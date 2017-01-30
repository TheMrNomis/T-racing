using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotationButton : MonoBehaviour
{
    public GameObject cardPanel;

	// This function is called when we click on the button
	public void OnClick ()
    {
        //Initialisation of the card
        GameObject card = gameObject;

        //We get the selected card
        Toggle[] toggles = cardPanel.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; ++i)
        {
            if (toggles[i].isOn)
            {
                card = toggles[i].gameObject;
                break;
            }
        }

        if(card != gameObject)
        {
            //Now that we have get the card, we rotate the image of it
            Vector3 axis = new Vector3(0.0f, 0.0f, 1.0f);
            //card.transform.Rotate(axis, 60.0f);
            Image [] img = card.GetComponentsInChildren<Image>();
            for(int i = 0; i < img.Length; ++i)
            {
                img[i].transform.Rotate(axis, 60.0f);
            }

            //Then we rotate the text in order to keep it straight
            /*Text txt = card.GetComponentInChildren<Text>();
            if (txt)
                txt.gameObject.transform.Rotate(axis, -60.0f);
            else
                Debug.Log("No text found");*/
        }
    }
}
