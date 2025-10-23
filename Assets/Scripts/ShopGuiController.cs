using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGuiController : MonoBehaviour {
  [SerializeField]
  private Transform shopItemRoot;
  
  [SerializeField]
  private GameObject shopItemGuiPrefab;
  
  private List<ShopItemGuiController> shopItemGuis = new List<ShopItemGuiController>();

  private void Start() {
    foreach (ShopItem shopItem in GlobalInjector.shop.shopItems) {
      var curShopItemGui = Instantiate(shopItemGuiPrefab, shopItemRoot).GetComponent<ShopItemGuiController>();
      curShopItemGui.SetShopItem(shopItem);
      shopItemGuis.Add(curShopItemGui);
    }
  }
}
