using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallOverSheared : ShearBehaviour
{
    public override void ShearSheep(InputAction.CallbackContext obj)
    {
        if(HasBeenSheered )
        {
            return;
        }

        base.ShearSheep(obj);

        transform.parent.GetComponent<SheepLookAtYouScriptThatGetsPutOnTheSheep>().turn = false;
        //transform.parent.GetComponent<SheepLookAtYouScriptThatGetsPutOnTheSheep>().enabled = false;

        Vector3 rotate = transform.parent.eulerAngles;
        rotate.x = -90;
        transform.parent.eulerAngles = rotate;

        Debug.Log(transform.parent.eulerAngles);
    }


}
