namespace TestRandomizer.Model;

public class Item
{
    public Item(string itemId, ItemType itemType)
    {
        ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
        ItemType = itemType;
    }

    public string ItemId { get; set; }
    public ItemType ItemType { get; set; }
}
