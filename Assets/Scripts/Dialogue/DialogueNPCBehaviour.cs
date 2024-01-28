/*******************************************************************************
* File Name :         NPCBehavious.cs
* Author(s) :         Toby Schamberger, Sky Beal, Jay Embry
* Creation Date :     9/11/2023
*
* Brief Description : Basic NPC code that lets the player talk to npcs.
* (code stolen from Gorp Game and Nyne Lyves)
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueNPCBehaviour : MonoBehaviour
{
    [Header("Dialogue")]
    public NPCScript DefaultScript;
    public NPCScript SheeredScript;

    [Header("Leave this null")]
    public Animator animator;

    /// <summary>
    /// when on speaking box, can start text
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            if (DialogueCanvas.Instance.ControlPromptCanvas != null)
                DialogueCanvas.Instance.ControlPromptCanvas.SetActive(true);

            InputManager.Instance.Talk.started += ActivateSpeech;
        }
    }

    /// <summary>
    /// when off speaking box, no start text
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerExit(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            if (DialogueCanvas.Instance.ControlPromptCanvas != null)
                DialogueCanvas.Instance.ControlPromptCanvas.SetActive(false);

            InputManager.Instance.Talk.started -= ActivateSpeech;
        }
    }

    public virtual void ActivateSpeech(InputAction.CallbackContext obj)
    {
        TurnPlayerTowardsSheep();
        SetAnimator(true);

        ShearBehaviour shear = GetComponent<ShearBehaviour>();

        if (shear != null && shear.HasBeenSheered)
        {
            DialogueCanvas.Instance.LoadScript(SheeredScript, this);
        }
        else
        {
            DialogueCanvas.Instance.LoadScript(DefaultScript, this);
        }

        PlayerController.Instance.IgnoreAllInputs = true;
        InputManager.Instance.Talk.started -= ActivateSpeech;
        InputManager.Instance.Pause.started -= PlayerController.Instance.Pause_started;
        InputManager.Instance.Pause.started += Exit_text;
        
    }

    public virtual void Exit_text(InputAction.CallbackContext obj)
    {
        LeaveText();
    }

    public void LeaveText()
    {
        DialogueCanvas.Instance.CancelSpeech();
        SetAnimator(false);

        PlayerController.Instance.IgnoreAllInputs = false;
        InputManager.Instance.Pause.started -= Exit_text;
        InputManager.Instance.Pause.started += PlayerController.Instance.Pause_started;
        //InputManager.Instance.SkipText.started -= DialogueCanvas.Instance.Skip_text;
    }

    void TurnPlayerTowardsSheep()
    {
        PlayerController.Instance.transform.LookAt(transform);

        Vector3 rotate = PlayerController.Instance.transform.rotation.eulerAngles;
        rotate = new Vector3(rotate.x, rotate.y , rotate.z);

        PlayerController.Instance.transform.eulerAngles = rotate;//Quaternion.Euler(startingRoation.x, transform.rotation.y, startingRoation.z);
    }

    public virtual void SetAnimator(bool talkingAnimation)
    {
        if(animator == null)
        {
            Animator animator = transform.parent.GetComponent<Animator>();
        }
        
        if(animator != null)
            animator.SetBool("Talking", talkingAnimation);
    }
}
