using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Database", menuName = "Database")]
public class ScriptableDatabase : ScriptableObject
{
    public Shop.ShopSections sectionType = default;
    public List<ScriptableElement> databaseElements;
}
