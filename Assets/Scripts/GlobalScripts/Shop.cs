using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Shop : MonoBehaviour
{
  public List<ShopItem>  shopItems;

  public int ShopItemToIndex(ShopItem shopItem) {
    return shopItems.IndexOf(shopItem);
  }
  
  public bool BuyFromShop(int itemIndex) {
    InventoryManager inventoryManager = GlobalInjector.inventoryManager;

    if (itemIndex >= shopItems.Count || inventoryManager.Clicks < shopItems[itemIndex].cost) {
      return false;
    }
    
    inventoryManager.Clicks -= shopItems[itemIndex].cost;
    inventoryManager.AddShopItemToInventory(shopItems[itemIndex]);
    
    // Play unlock sound when successfully purchasing an item
    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayUnlockSound();
    }
    
    return true;
  }

  public bool BuyFromShop(ShopItem shopItem) {
    return BuyFromShop(ShopItemToIndex(shopItem));
  }
}
