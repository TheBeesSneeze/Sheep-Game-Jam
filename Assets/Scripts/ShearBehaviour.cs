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

    [HideInInspector] public bool HasBeenSheered = false;

    protected virtual void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            InputManager.Instance.Shear.started += ShearSheep;
            Debug.Log("entered sheep zone");
        }

    }

    protected virtual void OnTriggerExit(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            InputManager.Instance.Shear.started -= ShearSheep;
            Debug.Log("exited sheep zone");
        }
    }

    public void ShearSheep(InputAction.CallbackContext obj)
    {
        if(HasBeenSheered)
        {
            AudioSource.PlayClipAtPoint(noShearSound, transform.position, 100);
            Debug.Log("sorry, no more wool");
            return;
        }

        HasBeenSheered = true;
        AudioSource.PlayClipAtPoint(shearSound, transform.position, 100);

        foreach (SphereCollider ball in wool)
        {
            ball.enabled = true;
            Debug.Log("shearing...");
        }   
    }


}
