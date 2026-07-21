using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicTrigger : MonoBehaviour
{
    private Light spotLight;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
    }

    private void Update() 
    {
        if (TheWorld.StateStory == 8)
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
        if (TheWorld.StateStory == 8)
        {
            TheWorld.StateStory += 1;
        }
    }
}