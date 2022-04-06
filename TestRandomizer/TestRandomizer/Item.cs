namespace TestRandomizer;

public class Item
{
    public Item(string itemId, ItemTypeEnum itemType)
    {
        ItemId = itemId;
        ItemType = itemType;
    }

    public string ItemId { get; set; }
    public ItemTypeEnum ItemType { get; set; }
}
