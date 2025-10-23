using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInjector : MonoBehaviour {
  public static InventoryManager inventoryManager;
  public static Shop shop;
  
  [SerializeField]
  private InventoryManager _inventoryManager;
  [SerializeField]
  private Shop _shop;

  private void Awake() {
    inventoryManager = _inventoryManager;
    shop = _shop;
  }
}
