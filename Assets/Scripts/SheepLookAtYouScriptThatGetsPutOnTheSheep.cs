/*******************************************************************************
* File Name :         SheepLookAtYouScriptThatGetsPutOnTheSheep.cs
* Author(s) :         Toby Schamberger
* Creation Date :     1/27/2024
*
* Brief Description : theres no room for misintrepration
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepLookAtYouScriptThatGetsPutOnTheSheep : MonoBehaviour
{
    public float RotationModifier = 90;

    private Vector3 startingRoation;
    private bool turn;

    private void Start()
    {
        startingRoation = transform.rotation.eulerAngles;
    }
    public void StartTurning()
    {
        startingRoation = transform.rotation.eulerAngles;
        turn = true;

        StartCoroutine(TurnTowardsPlayer());
    }

    public void StopTurning()
    {
        turn = false;
    }

    private IEnumerator TurnTowardsPlayer()
    {
        while(turn)
        {
            transform.LookAt(PlayerController.Instance.transform);

            Vector3 rotate = transform.rotation.eulerAngles;
            rotate = new Vector3(startingRoation.x, rotate.y+RotationModifier, startingRoation.z);

            transform.eulerAngles = rotate;//Quaternion.Euler(startingRoation.x, transform.rotation.y, startingRoation.z);

            yield return null;
        }
    }
}
