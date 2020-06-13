using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCurrencyComponent : DelegateTextComponent
{
    protected override string value { get => profile.CheckItemAmount("softCurrency").ToString(); }

    public override void SetDelegate()
    {
        Profile.OnItemChanged += SetValue;
    }

    public override void OnDestroy()
    {
        Profile.OnItemChanged -= SetValue;
    }
}
