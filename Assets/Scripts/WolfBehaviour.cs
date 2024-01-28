using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WolfBehavior : ShearBehaviour
{
    [SerializeField]
    private GameObject babySheep;
    [SerializeField]
    private GameObject wolf;

    // Start is called before the first frame update

    public override void ShearSheep(InputAction.CallbackContext obj)
    {
        base.ShearSheep(obj);

        transform.parent.GetComponent<MeshRenderer>().enabled = false;
        wolf.SetActive(true);
    }
}
