namespace TestRandomizer.UnitTests.TestData
{
    internal static class ItemsData
    {
        public static List<Item> GenerateItems(int count = 10, int operationalItemsCount = 6)
        {
            var items = new List<Item>(count);
            for (int i = 1; i <= count; i++)
            {
                items.Add(new Item(
                    $"Item{i}",
                    i <= operationalItemsCount
                        ? ItemTypeEnum.Operational
                        : ItemTypeEnum.Pretest));
            }

            return items;
        }
    }
}
