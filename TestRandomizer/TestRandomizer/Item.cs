namespace TestRandomizer;

public class Item
{
    public Item(string itemId, ItemTypeEnum itemType)
    {
        ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
        ItemType = itemType;
    }

    public string ItemId { get; set; }
    public ItemTypeEnum ItemType { get; set; }
}
