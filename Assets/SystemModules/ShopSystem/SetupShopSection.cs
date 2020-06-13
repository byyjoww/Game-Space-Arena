using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetupShopSection : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTextComponent;
    [SerializeField] private Button buttonComponent;

    public Button ButtonComponent => buttonComponent;

    public void Setup(string name)
    {
        nameTextComponent.text = name;
    }
}
