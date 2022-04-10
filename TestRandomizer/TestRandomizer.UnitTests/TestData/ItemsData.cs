using TestRandomizer.Model;

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
                    i.ToString(),
                    i <= operationalItemsCount
                        ? ItemType.Operational
                        : ItemType.Pretest));
            }

            return items;
        }
    }
}
