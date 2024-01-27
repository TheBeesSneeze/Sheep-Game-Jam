using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//so unfinished sorry guys

public class ShearBehaviour : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource sheepAudio;
    [SerializeField] private AudioClip noShearSound;
    [SerializeField] private AudioClip shearSound;

    [Header("Unity")]
    [SerializeField] private List<Collider> wool = new List<Collider>();

    [HideInInspector] public bool HasBeenSheered=false;

    protected virtual void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            PlayerController.Instance.Shear.started += ShearSheep;
        }

    }

    protected virtual void OnTriggerExit(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            PlayerController.Instance.Shear.started -= ShearSheep;
        }
    }

    public void ShearSheep(InputAction.CallbackContext obj)
    {
        foreach (Collider ball in wool)
        {
            //turn all colliders on
        }
        //uhhhhh find a better place for this
            AudioSource.PlayClipAtPoint(shearSound, transform.position, 100);
         
       // if (/*already sheared*/)
        {
            AudioSource.PlayClipAtPoint(noShearSound, transform.position, 100);
            Debug.Log("sorry, no more wool");
            return;
        }
    }


}
