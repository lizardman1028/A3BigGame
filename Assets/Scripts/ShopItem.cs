using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "A3/ShopItem", order = 1)]
public class ShopItem : ScriptableObject {
  [Header("Basic Info")]
  public string itemName;
  [TextArea(2, 4)]
  public string description;
  public Sprite icon;
  
  [Header("Game Stats")]
  public float cost;
  public float clicksPerSecond;
  
  [Header("Category")]
  public ShopItemCategory category = ShopItemCategory.DormRoom;
}

public enum ShopItemCategory
{
    DormRoom,
    Campus,
    Academic,
    Social,
    Premium
}
