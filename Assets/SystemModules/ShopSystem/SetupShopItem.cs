using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SetupShopItem : MonoBehaviour
{
    [SerializeField] private Image imageComponent = default;
    [SerializeField] private TMP_Text nameTextComponent = default;
    [SerializeField] private TMP_Text costValueTextComponent = default;
    [SerializeField] private Image costTypeImageComponent = default;
    [SerializeField] private Button buttonComponent = default;
    [SerializeField] private GameObject soldFlagObject = null;

    public Button ButtonComponent => buttonComponent;

    public void Setup(ScriptableElement element)
    {
        imageComponent.sprite = element.itemSprite;
        nameTextComponent.text = element.itemName;
        costValueTextComponent.text = element.itemCost.ToString();
        ScriptableDatabase currencyDatabase = Database.i.Databases.First(x => x.sectionType == Shop.ShopSections.Currencies);
        costTypeImageComponent.sprite = currencyDatabase.databaseElements.First(x => x.costType == element.costType).itemSprite;
    }

    public void ToggleSoldFlag(bool status)
    {
        soldFlagObject.SetActive(status);
    }
}
