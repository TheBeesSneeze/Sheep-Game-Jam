using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NakedSheep : ShearBehaviour
{
    public MeshRenderer mesh;
    public Material skin;
    public override void ShearSheep(InputAction.CallbackContext obj)
    {
        Debug.Log("shearing naked");

        if (GetComponent<DialogueNPCBehaviour>().talking) return;

        if (HasBeenSheered)
        {
            AudioSource.PlayClipAtPoint(noShearSound, transform.position, 100);
            return;
        }

        HasBeenSheered = true;

        if (shearSound != null)
            AudioSource.PlayClipAtPoint(shearSound, transform.position, 100);

        UIManager ui = GameObject.FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.UpdateShearUI();
        }

        transform.parent.GetComponent<Animator>().SetBool("Lose Skin",true);
    }
}
