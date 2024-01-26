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
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour
{
    [Header("Dialogue")]
    public NPCScript DefaultScript;
    public NPCScript SheeredScript;

    public NPCScript CurrentScript { 
        get { return CurrentScript; } 
        set { LoadScript(value);  } 
    }

    private string characterName;
    private AudioClip CharacterDialogueSound;

    private List<string> TextList = new List<string>();

    [Header("Settings")]
    public bool LoopText;
    public static float ScrollSpeed = 0.05f;
    private float bobAnimationTime = 0.5f;

    private bool SkipText;
    public AudioSource dialogueSoundSource;

    private int textIndex = 0;
    private bool typing;

    public virtual void Start()
    {
        PopulateCanvasVariables();

        if (dialogueSoundSource == null)
        {
            dialogueSoundSource = DialogueCanvas.Instance.GetComponent<AudioSource>();
        }

        LoadScript(DialogueScripts);

        if(ButtonPrompt != null)
            ButtonPrompt.SetActive(false);
    }


    /// <summary>
    /// when on speaking box, can start text
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            if(ButtonPrompt != null)
                ButtonPrompt.SetActive(true);

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
            if (ButtonPrompt != null)
                ButtonPrompt.SetActive(false);

            PlayerController.Instance.Select.started -= ActivateSpeech;
        }
    }

    
}
