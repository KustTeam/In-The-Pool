using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuchokTrigger : MonoBehaviour
{
    private Light spotLight;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    private void Update() 
    {
        if (TheWorld.StateStory == 2)
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
        if (TheWorld.StateStory == 2)
        {
            TheWorld.StateStory += 1;
        }
    }
}