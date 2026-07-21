using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour
{
    private Light spotLight;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    private void Update() 
    {
        if (TheWorld.StateStory == 1)
        {
            spotLight.enabled = true;
        }
        else if (TheWorld.StateStory == 3)
        {
            spotLight.enabled = true;
        }
        else if (TheWorld.StateStory == 5)
        {
            spotLight.enabled = true;
        }
        else if (TheWorld.StateStory == 7)
        {
            spotLight.enabled = true;
        }
        else
        {
            spotLight.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (TheWorld.StateStory == 1 || TheWorld.StateStory == 3 || TheWorld.StateStory == 5 || TheWorld.StateStory == 7)
        {
            TheWorld.StateStory += 1;
        }
    }
}