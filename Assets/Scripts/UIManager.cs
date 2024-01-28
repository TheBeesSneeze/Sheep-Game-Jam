using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int ShearAmountTotal;
        
    public List<GameObject> Images = new List<GameObject>();

    public void UpdateShearUI()
    {
        ShearAmountTotal -= 1;
        Debug.Log("-1");

        Images[0].SetActive(false);
        Images.RemoveAt(0);

        if (ShearAmountTotal < 1)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
    
}
