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

    /// <summary>
    /// when on speaking box, can start text
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            if (DialogueCanvas.Instance.ControlPromptCanvas != null)
                DialogueCanvas.Instance.ControlPromptCanvas.SetActive(true);

            PlayerController.Instance.Select.started += ActivateSpeech;
        }
    }

    /// <summary>
    /// when off speaking box, no start text
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            if (DialogueCanvas.Instance.ControlPromptCanvas != null)
                DialogueCanvas.Instance.ControlPromptCanvas.SetActive(false);

            PlayerController.Instance.Select.started -= ActivateSpeech;
        }
    }

    public void ActivateSpeech(InputAction.CallbackContext obj)
    {
        //PUT SHEER IF CONDITION STUFF STUFF HERE
        DialogueCanvas.Instance.LoadScript(DefaultScript, this);

        PlayerController.Instance.IgnoreAllInputs = true;
        PlayerController.Instance.Select.started -= ActivateSpeech;
        PlayerController.Instance.ExitText.started += Exit_text;
        PlayerController.Instance.SkipText.started += Skip_text;
    }

    public virtual void Exit_text(InputAction.CallbackContext obj)
    {
        LeaveText();
    }

    public void LeaveText()
    {
        DialogueCanvas.Instance.CancelSpeech();

        PlayerController.Instance.IgnoreAllInputs = false;
        PlayerController.Instance.ExitText.started -= Exit_text;
        PlayerController.Instance.SkipText.started -= Skip_text;
    }

    public void Skip_text(InputAction.CallbackContext obj)
    {
        DialogueCanvas.Instance.SkipText = false;

        if (DialogueCanvas.Instance.typing)
            DialogueCanvas.Instance.SkipText = true;
    }
}
