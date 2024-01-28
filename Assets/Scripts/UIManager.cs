using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public int ShearAmountTotal;
    [SerializeField] public bool IsWolf;

    public static void UpdateShearUI()
    {/*
        if (!IsWolf)
        {
            ShearAmountTotal -= 1;
            Debug.Log("-1");

            if (ShearAmountTotal < 1)
            {
                //lose screen??
            }
        }

        if (IsWolf)
        {
            //do the end game thing
        }
    }
    */
}
