using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaunaTrigger : MonoBehaviour
{
    private Light spotLight;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    private void Update() 
    {
        if (TheWorld.StateStory == 6)
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
        if (TheWorld.StateStory == 6)
        {
            TheWorld.StateStory += 1;
        }
    }
}