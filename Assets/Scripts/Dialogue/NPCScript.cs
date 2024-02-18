/*******************************************************************************
* File Name :         NPCScript.cs
* Author(s) :         Toby Schamberger
* Creation Date :     9/11/2023
*
* Brief Description : Scriptable object that stores ONE dialogue interraction.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCScript", menuName = "NPC Script")]
public class NPCScript : ScriptableObject
{
    [SerializeField][TextArea] public List<string> TextList;

    public string CharacterName;

    public bool IsNormal;

    public AudioClip CharacterDialogueSound;
    public AudioClip CharacterDialogueSound2;
    public AudioClip CharacterDialogueSound3;
}
