using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using UnityEngine.U2D;

[Serializable]
[CreateAssetMenu(menuName = "Game Data/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventoryItem> items;

    public List<InventoryItem> GetItems()
    {
        string json = Resources.Load<TextAsset>("JSON/ItemsInventory").text;
        return JsonConvert.DeserializeObject<List<InventoryItem>>(json);
    }
}

#region INVENTORY MANAGEMENT
[Serializable]
public class InventoryItem
{
    public string name;
    public int quantity;
    public string description;
    public string itemType;
    public string spriteAtlasName;
    public string spriteName;
    [NonSerialized]
    private Sprite _itemSprite;
    public Sprite GetSprite()
    {
        if (_itemSprite == null)
        {
            Debug.Log("Sprite Atlas: " + spriteAtlasName + "  Sprite: " + spriteName);
            SpriteAtlas sa = Resources.Load<SpriteAtlas>("Sprites/Inventory/" + spriteAtlasName);
            _itemSprite = sa.GetSprite(spriteName);
        }
        return _itemSprite;
    }

    public InventoryItem(string name, int quantity, string description, string itemType, string spriteAtlasName, string spriteName)
    {
        this.name = name;
        this.quantity = quantity;
        this.description = description;
        this.itemType = itemType;
        this.spriteAtlasName = spriteAtlasName;
        this.spriteName = spriteName;
    }
}

#endregion