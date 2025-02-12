using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShearBehaviour : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] protected AudioSource sheepAudio;
    [SerializeField] protected AudioClip noShearSound;
    [SerializeField] protected AudioClip shearSound;

    [Header("Unity")]
    [SerializeField] private List<Rigidbody> wool = new List<Rigidbody>();

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

    public virtual void ShearSheep(InputAction.CallbackContext obj)
    {
        Debug.Log("shearing");

        if (GetComponent<DialogueNPCBehaviour>().talking) return;

        if(HasBeenSheered)
        {
            if(noShearSound != null)
                AudioSource.PlayClipAtPoint(noShearSound, transform.position, 100);

            return;
        }

        HasBeenSheered = true;

        if(shearSound != null)
            AudioSource.PlayClipAtPoint(shearSound, transform.position, 100);

        foreach (Rigidbody ball in wool)
        {
            Rigidbody myRB = ball.GetComponent<Rigidbody>();

            float x = Random.Range(-20, 20);
            float y = Random.Range(-20, 20);
            float z = Random.Range(-20, 20);

            Vector3 speed = new Vector3(x, y, z);
            //speed = speed;

            myRB.velocity = speed;
        }

        UpdateShearUI();
    }

    protected virtual void UpdateShearUI()
    {
        UIManager ui = GameObject.FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.UpdateShearUI();
        }
    }

}
