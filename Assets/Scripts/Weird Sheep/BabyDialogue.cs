using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BabyDialogue : DialogueNPCBehaviour
{
    public override void Exit_text(InputAction.CallbackContext obj)
    {
        bool sheered = GetComponent<ShearBehaviour>().HasBeenSheered;
        if(sheered)
        {
            Debug.Log("player wins");
            SceneManager.LoadScene("WinScreen");
            return;
        }
        
        base.Exit_text(obj);
    }

    public override void LeaveText()
    {
        bool sheered = GetComponent<ShearBehaviour>().HasBeenSheered;
        if (!sheered) return;

        Debug.Log("player wins");
        SceneManager.LoadScene("WinScreen");
    }

    public override void OnDialogueEnd()
    {
        bool sheered = GetComponent<ShearBehaviour>().HasBeenSheered;
        if (!sheered) return;

        Debug.Log("player wins");
        SceneManager.LoadScene("WinScreen");
    }
}
