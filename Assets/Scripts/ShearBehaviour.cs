using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//so unfinished sorry guys

public class ShearBehaviour : MonoBehaviour
{
    public AudioSource SheepAudio;
    public AudioClip NoShearSound;
    public AudioClip ShearSound;
    public int ShearAmount;
    public List<Collider> Wool = new List<Collider>();

    protected virtual void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            //PlayerController.Instance.Sheer.started += ShearSheep;
        }

    }

    protected virtual void OnTriggerExit(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            //PlayerController.Instance.Sheer.started -= ShearSheep;
        }
    }

    public void ShearSheep(InputAction.CallbackContext obj)
    {
        foreach (Collider ball in Wool)
        {
            //turn all colliders on
        }
        //uhhhhh find a better place for this
            AudioSource.PlayClipAtPoint(ShearSound, transform.position, 100);
         
       // if (/*already sheared*/)
        {
            AudioSource.PlayClipAtPoint(NoShearSound, transform.position, 100);
            Debug.Log("sorry, no more wool");
            return;
        }

        ShearAmount = ShearAmount - 1;
        if (ShearAmount < 1)
        {
            //end the game or somethin
        }
    }
}
