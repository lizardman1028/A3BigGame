using UnityEngine;

/// <summary>
/// Shop item creator utility to help set up Stanford-themed shop items
/// This class helps create shop items with appropriate Stanford University theming
/// </summary>
public static class StanfordShopItemCreator
{
    /// <summary>
    /// Creates a collection of Stanford-themed shop items with balanced progression
    /// </summary>
    public static ShopItem[] CreateStanfordShopItems()
    {
        // Note: You'll need to assign the sprites in the Unity Inspector
        // These are templates for the shop items you should create as ScriptableObjects
        
        var shopItems = new ShopItem[]
        {
            // Early Game Items (1-100 clicks)
            CreateShopItemTemplate("Squirrel Friend", "A friendly campus squirrel helps gather acorns", 15f, 0.1f),
            CreateShopItemTemplate("Study Group", "Form a study group for passive knowledge gains", 50f, 0.5f),
            
            // Mid Game Items (100-1000 clicks)
            CreateShopItemTemplate("Dorm Room", "Your own dorm room generates campus reputation", 200f, 1f),
            CreateShopItemTemplate("Dining Hall", "Access to unlimited dining hall meals", 500f, 3f),
            CreateShopItemTemplate("Green Library Access", "24/7 library access for constant learning", 1000f, 8f),
            
            // Late Game Items (1000+ clicks)
            CreateShopItemTemplate("Professor Mehran", "Legendary CS professor mentorship", 2500f, 20f),
            CreateShopItemTemplate("Professor Chris Piech", "AI expertise and guidance", 5000f, 50f),
            CreateShopItemTemplate("Memorial Church", "Spiritual guidance and campus blessing", 10000f, 100f),
            CreateShopItemTemplate("Hoover Tower", "Commanding view of entire campus", 25000f, 250f),
            CreateShopItemTemplate("School of Engineering", "Ultimate engineering knowledge", 50000f, 500f),
            
            // End Game Items
            CreateShopItemTemplate("Stanford Tree Mascot", "Become the legendary Stanford Tree", 100000f, 1000f),
            CreateShopItemTemplate("Stanford Degree", "Complete your Stanford education", 500000f, 2500f)
        };
        
        return shopItems;
    }
    
    private static ShopItem CreateShopItemTemplate(string name, string description, float cost, float clicksPerSecond)
    {
        // This creates a template - you'll need to create actual ScriptableObject assets
        var item = ScriptableObject.CreateInstance<ShopItem>();
        item.itemName = name;
        item.cost = cost;
        item.clicksPerSecond = clicksPerSecond;
        // Note: Sprites need to be assigned in Unity Inspector
        return item;
    }
}