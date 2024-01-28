using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardboardBehaviour : DialogueNPCBehaviour
{
    public override void ActivateSpeech(InputAction.CallbackContext obj)
    {
        Animator cardboard = transform.parent.GetComponent<Animator>();

        cardboard.SetTrigger("Fall Over");
    }
}
