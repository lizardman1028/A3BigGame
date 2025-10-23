using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemGuiController : MonoBehaviour
{
    private ShopItem shopItem;
    
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private TextMeshProUGUI itemPrice;
    [SerializeField]
    private TextMeshProUGUI itemClicksPerSecond;
    [SerializeField]
    private Color defaultTextColor;
    [SerializeField]
    private Color tooExpensiveTextColor;
    
    public void SetShopItem(ShopItem shopItem) {
      this.shopItem = shopItem;
      button.onClick = new Button.ButtonClickedEvent();
      button.onClick.AddListener(
        () => {
          GlobalInjector.shop.BuyFromShop(this.shopItem);
        }
      );

      itemImage.sprite = this.shopItem.icon;
      itemName.text = this.shopItem.itemName;
      itemPrice.text = "$" + this.shopItem.cost.ToString("0.####");
      itemClicksPerSecond.text = "CPS:" + this.shopItem.clicksPerSecond.ToString("0.####");
    }

    private void Update() {
      if (GlobalInjector.inventoryManager.Clicks < shopItem.cost) {
        itemName.color = tooExpensiveTextColor;
        itemPrice.color = tooExpensiveTextColor;
        itemClicksPerSecond.color = tooExpensiveTextColor;
        return;
      }
      itemName.color = defaultTextColor;
      itemPrice.color = defaultTextColor;
      itemClicksPerSecond.color = defaultTextColor;
    }
}
