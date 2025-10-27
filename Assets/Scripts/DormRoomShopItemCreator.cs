using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Utility script to automatically create dorm room shop items
/// This will create the 4 shop items you mentioned: sofa, lamp, teddy bear, chair
/// </summary>
public class DormRoomShopItemCreator : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Sprites - Drag your sprites here")]
    [SerializeField] private Sprite sofaSprite;
    [SerializeField] private Sprite lampSprite;
    [SerializeField] private Sprite teddyBearSprite;
    [SerializeField] private Sprite chairSprite;
    
    [Header("Create Shop Items")]
    [SerializeField] private bool createShopItems = false;
    
    [ContextMenu("Create Dorm Room Shop Items")]
    public void CreateDormRoomShopItems()
    {
        CreateShopItem("Comfy Sofa", sofaSprite, 25f, 0.3f, "SofaShopItem");
        CreateShopItem("Study Lamp", lampSprite, 15f, 0.2f, "LampShopItem");
        CreateShopItem("Stanford Teddy Bear", teddyBearSprite, 35f, 0.4f, "TeddyBearShopItem");
        CreateShopItem("Desk Chair", chairSprite, 20f, 0.25f, "ChairShopItem");
        
        Debug.Log("Created 4 dorm room shop items!");
        AssetDatabase.Refresh();
    }
    
    private void CreateShopItem(string itemName, Sprite icon, float cost, float clicksPerSecond, string fileName)
    {
        // Create the ScriptableObject
        ShopItem shopItem = ScriptableObject.CreateInstance<ShopItem>();
        
        // Set the properties
        shopItem.itemName = itemName;
        shopItem.icon = icon;
        shopItem.cost = cost;
        shopItem.clicksPerSecond = clicksPerSecond;
        
        // Save it as an asset
        string path = $"Assets/Scripts/ShopItems/{fileName}.asset";
        AssetDatabase.CreateAsset(shopItem, path);
        
        Debug.Log($"Created shop item: {itemName} at {path}");
    }
    
    private void OnValidate()
    {
        if (createShopItems)
        {
            createShopItems = false;
            CreateDormRoomShopItems();
        }
    }
#endif
}