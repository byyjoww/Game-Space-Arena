using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SetupShipItem : MonoBehaviour
{
    [SerializeField] private Image imageComponent = default;
    [SerializeField] private TMP_Text nameTextComponent = default;
    [SerializeField] private Button buttonComponent = default;

    public Button ButtonComponent => buttonComponent;

    public void Setup(ScriptableElement element)
    {
        imageComponent.sprite = element.itemSprite;
        nameTextComponent.text = element.itemName;
    }
}
