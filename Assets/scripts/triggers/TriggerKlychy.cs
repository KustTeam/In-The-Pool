using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKlychy : MonoBehaviour
{
    private Light spotLight;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    private void Update() 
    {
        if (TheWorld.StateStory == 0)
        {
            spotLight.enabled = true;
        }
        else if (TheWorld.StateStory == 4)
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
        if (TheWorld.StateStory == 0 || TheWorld.StateStory == 4)
        {
            TheWorld.StateStory += 1;
        }
    }
}