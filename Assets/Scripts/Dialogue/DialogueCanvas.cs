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

    [Header("Unity")]
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject dialogueArea;

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

    public virtual void CancelSpeech()
    {
        Debug.Log("Cancelling speech");

        PlayerController.Instance.IgnoreAllInputs = false;
        PlayerController.Instance.Pause.started -= Exit_text;
        PlayerController.Instance.SkipText.started -= Skip_text;

        textIndex = 0;
        if (ButtonPrompt != null)
            ButtonPrompt.SetActive(true);

        if (DialogueCanvas != null)
            DialogueCanvas.SetActive(false);

    }

    /// <summary>
    /// starts the coroutine
    /// </summary>
    public void ActivateSpeech(InputAction.CallbackContext obj)
    {
        ActivateSpeech();
    }

    public virtual void ActivateSpeech()
    {
        Debug.Log("Activating speech");

        PlayerController.Instance.IgnoreAllInputs = true;

        PlayerController.Instance.Pause.started += Exit_text;
        PlayerController.Instance.SkipText.started += Skip_text;

        //if end dialogue
        if (!LoopText && textIndex == TextList.Count)
        {
            CancelSpeech();
            return;
        }

        if (!typing)
        {
            StartCoroutine(StartText());
        }

        if (ButtonPrompt != null)
            ButtonPrompt.SetActive(false);
    }

    public virtual void Exit_text(InputAction.CallbackContext obj)
    {
        CancelSpeech();
    }

    public void Skip_text(InputAction.CallbackContext obj)
    {
        SkipText = false;

        if (typing)
            SkipText = true;
    }

    /// <summary>
    /// coroutine that does the scrolly typewriter text
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator StartText()
    {
        yield return new WaitForSeconds(ScrollSpeed);
        typing = true;
        DialogueCanvas.SetActive(true);

        SkipText = false;

        ChangeDialogueSprites();

        BobAnimations();

        SwitchTalkingSound();

        SwitchNames();

        //typewriter text
        for (int i = 0; i < TextList[textIndex].Length + 1; i++)
        {
            if (SkipText)
            {
                Debug.Log("Skipping Text");
                SkipText = false;
                TextBox.text = TextList[textIndex];
                break;
            }

            TextBox.text = TextList[textIndex].Substring(0, i);

            PlayTalkingSound();

            yield return new WaitForSeconds(ScrollSpeed);
        }

        typing = false;
        textIndex++;

        if (textIndex >= TextList.Count && LoopText)
        {
            textIndex = 0;
        }
    }

    private void PlayTalkingSound()
    {
        if (dialogueSoundSource == null)
            return;

        dialogueSoundSource.Stop();
        dialogueSoundSource.pitch = Random.value;
        dialogueSoundSource.Play();
    }

    private void ChangeDialogueSprites()
    {
        if (textIndex < PlayerTalkingSprites.Count && PlayerTalkingSprites[textIndex] != null)
            PlayerSprite.sprite = PlayerTalkingSprites[textIndex];

        if (textIndex < NPCTalkingSprites.Count && NPCTalkingSprites[textIndex] != null)
            NPCSprite.sprite = NPCTalkingSprites[textIndex];
    }

    private void BobAnimations()
    {
        if (textIndex < WhoIsTalking.Count)
        {
            if (WhoIsTalking[textIndex] == Talking.Player)
                StartCoroutine(AnimateSprite(PlayerSprite.rectTransform));

            if (WhoIsTalking[textIndex] == Talking.NPC)
                StartCoroutine(AnimateSprite(NPCSprite.rectTransform));
        }
    }

    private void SwitchTalkingSound()
    {
        if (dialogueSoundSource == null)
        {
            Debug.LogWarning("No audio source on " + gameObject.name);
            return;
        }

        if (WhoIsTalking[textIndex].Equals(Talking.Player))
            dialogueSoundSource.clip = Character1DialogueSound;

        if (WhoIsTalking[textIndex].Equals(Talking.NPC))
            dialogueSoundSource.clip = Character2DialogueSound;

        if (WhoIsTalking[textIndex].Equals(Talking.Nobody))
            dialogueSoundSource.clip = null;
    }

    /// <summary>
    /// switches out names and flips text area
    /// </summary>
    private void SwitchNames()
    {
        if (Name1 == null || Name2 == null || DialogueArea == null)
        {
            Debug.LogWarning("Name Text boxes or Dialogue Area not defined in " + gameObject.name);

            return;
        }

        if (WhoIsTalking[textIndex].Equals(Talking.Player))
        {
            Name1.gameObject.SetActive(true);
            Name2.gameObject.SetActive(false);

            Name1.text = character1Name;

            DialogueArea.transform.localScale = new Vector3(1, 1, 1);
        }

        if (WhoIsTalking[textIndex].Equals(Talking.NPC))
        {
            Name1.gameObject.SetActive(false);
            Name2.gameObject.SetActive(true);

            Name2.text = character2Name;

            DialogueArea.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// Animates the sprite by bobbing it up and down.
    /// </summary>
    /// <param name="Sprite"></param>
    /// <returns></returns>
    private IEnumerator AnimateSprite(RectTransform Sprite)
    {
        Vector3 startPosition = Sprite.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 50, 0);

        float i = 0;

        while (i < 0.5f)
        {
            i += 0.02f;

            Sprite.position = Vector3.Lerp(startPosition, targetPosition, i);

            yield return new WaitForSeconds(bobAnimationTime / 100);
        }

        while (i <= 1f)
        {
            i += 0.02f;

            Sprite.position = Vector3.Lerp(targetPosition, startPosition, i);

            yield return new WaitForSeconds(bobAnimationTime / 100);
        }
    }

    public void PopulateCanvasVariables()
    {
        TextBox = UniversalVariables.Instance.TextBox;
        DialogueCanvas = UniversalVariables.Instance.DialogueCanvas;
        DialogueArea = UniversalVariables.Instance.DialogueArea;
        PlayerSprite = UniversalVariables.Instance.PlayerSprite;
        NPCSprite = UniversalVariables.Instance.NPCSprite;

        Name1 = UniversalVariables.Instance.Name1;
        Name2 = UniversalVariables.Instance.Name2;

        if (dialogueSoundSource == null)
            dialogueSoundSource = UniversalVariables.Instance.dialogueSoundSource;
    }

    public void LoadScript(NPCScript Script)
    {
        if (Script == null)
            return;

        TextList = Script.TextList;

        WhoIsTalking = Script.WhoIsTalking;

        character1Name = Script.CharacterName;

        CharacterDialogueSound = Script.CharacterDialogueSound;
    }
}
