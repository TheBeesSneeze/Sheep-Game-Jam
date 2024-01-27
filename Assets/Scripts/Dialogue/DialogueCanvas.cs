/*******************************************************************************
* File Name :         DialogueCanvas.cs
* Author(s) :         Toby Schamberger
* Creation Date :     1/26/2024
*
* Brief Description : 
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueCanvas : MonoBehaviour
{
    public static DialogueCanvas Instance;

    [Header("Control prompts")]
    [SerializeField] public GameObject ControlPromptCanvas;

    [Header("Dialogue Canvas")]
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private AudioSource dialogueSoundSource;

    [Header("Settings")]
    public static float ScrollSpeed = 0.05f;

    [HideInInspector] public bool SkipText;

    private int textIndex = 0;
    [HideInInspector]public bool typing;

    private NPCScript currentScript;
    private DialogueNPCBehaviour currentSheep;

    private AudioClip CharacterDialogueSound;

    private List<string> TextList = new List<string>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        if(ControlPromptCanvas!= null)
        {
            ControlPromptCanvas.SetActive(false);
        }
        
        if(dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
            return;
        }

        Debug.LogWarning("no canvas dummy");
    }

    public void LoadScript(NPCScript script, DialogueNPCBehaviour sheep)
    {
        if (script == null)
        {
            return;
        }

        currentSheep = sheep;
        currentScript = script;

        TextList = script.TextList;

        characterName.text = script.CharacterName;

        if(script.CharacterDialogueSound != null)
            CharacterDialogueSound = script.CharacterDialogueSound;

        ActivateSpeech();
    }

    public virtual void ActivateSpeech()
    {
        Debug.Log("Activating speech");

        PlayerController.Instance.IgnoreAllInputs = true;
        

        if (!typing)
        {
            StartCoroutine(StartText());
        }

        if (ControlPromptCanvas != null)
            ControlPromptCanvas.SetActive(false);
    }

    public virtual void CancelSpeech()
    {
        Debug.Log("Cancelling speech");

        PlayerController.Instance.IgnoreAllInputs = false;

        textIndex = 0;
        if (ControlPromptCanvas != null)
            ControlPromptCanvas.SetActive(true);

        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }

    /// <summary>
    /// coroutine that does the scrolly typewriter text
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator StartText()
    {
        yield return new WaitForSeconds(ScrollSpeed);

        Debug.Log("start text, "+ textIndex);

        InputManager.Instance.SkipText.started += Skip_text;

        if (textIndex >= TextList.Count)
        {
            CancelSpeech();
            yield break;
        }

        typing = true;
        dialogueCanvas.SetActive(true);

        SkipText = false;

        characterName.text = currentScript.CharacterName;

        //typewriter text
        for (int i = 0; i < TextList[textIndex].Length + 1; i++)
        {
            if (SkipText)
            {
                Debug.Log("Skipping Text");
                SkipText = false;
                textBox.text = TextList[textIndex];
                break;
            }
           
            textBox.text = TextList[textIndex].Substring(0, i);

            PlayTalkingSound();

            yield return new WaitForSeconds(ScrollSpeed);
        }

        Debug.Log("done typing");
        typing = false;
        

        InputManager.Instance.SkipText.started -= Skip_text;
        InputManager.Instance.SkipText.started += Next_Text;
    }

    private void PlayTalkingSound()
    {
        if (dialogueSoundSource == null)
            return;

        dialogueSoundSource.Stop();
        dialogueSoundSource.pitch = Random.value;
        dialogueSoundSource.Play();
    }

    public void Skip_text(InputAction.CallbackContext obj)
    {
        Debug.Log("skipping text");
        SkipText = true;

        /*
        if (typing)
        {
            Debug.Log("skip text true");
            SkipText = true;
            return;
        }
        */
    }

    public void Next_Text(InputAction.CallbackContext obj)
    {
        Debug.Log("next text");
        textIndex++;
        StartCoroutine(StartText());
        InputManager.Instance.SkipText.started -= Next_Text;
    }
}
