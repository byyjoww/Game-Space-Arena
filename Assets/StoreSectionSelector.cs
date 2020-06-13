using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSectionSelector : MonoBehaviour
{
    [SerializeField] private Transform sections;
    public void SelectSection(string sectionName)
    {
        foreach (Transform section in sections)
        {
            if (section.name == sectionName)
            {
                section.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}