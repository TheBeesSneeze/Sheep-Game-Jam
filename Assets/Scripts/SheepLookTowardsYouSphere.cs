using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SheepLookTowardsYouSphere : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SheepLookAtYouScriptThatGetsPutOnTheSheep sheep = other.GetComponent<SheepLookAtYouScriptThatGetsPutOnTheSheep>();

        if(sheep != null)
        {
            sheep.StartTurning();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SheepLookAtYouScriptThatGetsPutOnTheSheep sheep = other.GetComponent<SheepLookAtYouScriptThatGetsPutOnTheSheep>();

        if (sheep != null)
        {
            sheep.StopTurning();
        }
    }
}
