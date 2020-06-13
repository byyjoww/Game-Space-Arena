using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCurrencyComponent : DelegateTextComponent
{
    protected override string value { get => profile.CheckItemAmount("hardCurrency").ToString(); }

    public override void SetDelegate()
    {
        Profile.OnItemChanged += SetValue;
    }

    public override void OnDestroy()
    {
        Profile.OnItemChanged -= SetValue;
    }
}
