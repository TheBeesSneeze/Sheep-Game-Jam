using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallOverSheared : ShearBehaviour
{
    public override void ShearSheep(InputAction.CallbackContext obj)
    {
        base.ShearSheep(obj);

        Vector3 rotate = transform.eulerAngles;
        rotate.x = -90;
        transform.eulerAngles = rotate;
    }
}
