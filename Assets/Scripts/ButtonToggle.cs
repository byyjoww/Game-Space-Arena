using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonToggle : MonoBehaviour
{
    [SerializeField] protected GameObject objectToToggle;

    public void Toggle()
    {
        if (objectToToggle.activeInHierarchy == false)
        {
            objectToToggle.SetActive(true);            
        }
        else if (objectToToggle.activeInHierarchy == true)
        {
            objectToToggle.SetActive(false);
        }
    }    
}