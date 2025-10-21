using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "A3/ShopItem", order = 1)]
public class ShopItem : ScriptableObject {
  public string itemName;
  public Sprite icon;
  public float cost;
  public float clicksPerSecond;
}
