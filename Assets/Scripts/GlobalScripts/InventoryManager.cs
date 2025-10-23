using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
  private Dictionary<ShopItem, int> inventory = new Dictionary<ShopItem, int>();
  
  [UsedImplicitly]
  [HideInInspector]
  public float Clicks = 0;

  void Awake() {
    Time.fixedDeltaTime = 1f;
  }
  
  public void FixedUpdate() {
    GlobalInjector.inventoryManager.Clicks += GlobalInjector.inventoryManager.GetTotalClicksPerSecond();
  }
  
  public void AddShopItemToInventory(ShopItem item) {
    inventory.TryAdd(item, 0);
    inventory[item]++;
  }

  public float GetTotalClicksPerSecond() {
    float clicksPerSecond = 0;
    foreach (var entry in inventory ) {
      clicksPerSecond += entry.Key.clicksPerSecond * entry.Value;
    } 
    return clicksPerSecond;
  }
  
}