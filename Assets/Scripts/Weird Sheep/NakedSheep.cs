using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NakedSheep : ShearBehaviour
{
    public Shader shader;
    public override void ShearSheep(InputAction.CallbackContext obj)
    {
        Debug.Log("shearing naked");

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

        //Material material = transform.parent.GetComponent<Material>();
        //shader.Albedo
    }
}
