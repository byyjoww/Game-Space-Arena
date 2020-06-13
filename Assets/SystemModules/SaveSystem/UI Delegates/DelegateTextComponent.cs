using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class DelegateTextComponent : MonoBehaviour
{
    protected Profile profile;
    [SerializeField] protected TMP_Text component;
    [SerializeField] protected abstract string value { get; }

    private void Start()
    {
        profile = Database.i.profile;
        SetDelegate();
        SetValue();
    }
    public void OnValidate()
    {
        component = GetComponent<TMPro.TMP_Text>();
    }
    public void SetValue()
    {
        component.text = value;
    }
    public abstract void SetDelegate();

    public abstract void OnDestroy();
}
