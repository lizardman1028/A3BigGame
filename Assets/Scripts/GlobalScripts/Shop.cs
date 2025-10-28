using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Shop : MonoBehaviour {
  [SerializeField]
  private GameObject bouncerPrefab;
  
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
    
    var instantiatedThing =Instantiate(bouncerPrefab, Vector2.zero, Quaternion.identity);
    instantiatedThing.GetComponent<SpriteRenderer>().sprite = shopItems[itemIndex].icon;
    
    return true;
  }

  public bool BuyFromShop(ShopItem shopItem) {
    return BuyFromShop(ShopItemToIndex(shopItem));
  }
}
