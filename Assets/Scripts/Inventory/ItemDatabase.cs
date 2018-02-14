using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    private void Start() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/GameData/Items.json"));
        ConstructItemDatabase();
    }

    private void ConstructItemDatabase() {
        foreach (JsonData item in itemData) {
            database.Add(
                new Item(
                    (int)item["id"],
                    (string)item["title"],
                    (int)item["value"],
                    (int)item["stats"]["power"],
                    (int)item["stats"]["defence"],
                    (int)item["stats"]["vitality"],
                    (string)item["description"],
                    (bool)item["stackable"],
                    (int)item["rarity"],
                    (string)item["slug"])
                );
        }
    }

    public Item FetchItemById(int id) {
        foreach (var item in database) {
            if (item.Id == id) {
                return item;
            }
        }

        return null;
    }
}

public class Item {
    public int Id { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item() {
        Id = -1;
    }

    public Item(int id, string title, int value, int power, int defence, int vitality, string description, bool stackable, int rarity, string slug) {
        Id = id;
        Title = title;
        Value = value;
        Power = power;
        Defence = defence;
        Vitality = vitality;
        Description = description;
        Stackable = stackable;
        Rarity = rarity;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }
}