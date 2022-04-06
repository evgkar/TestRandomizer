namespace TestRandomizer
{
    public class Testlet : ITestlet
    {
        private const uint PretestItemsCount = 4;
        private const uint OperationalItemsCount = 6;

        private readonly IShuffler _shuffler;
        private readonly List<Item> _items;

        public string TestletId { get; }

        public Testlet(string testletId, List<Item> items)
            : this(testletId, items, new Shuffler())
        {
        }

        internal Testlet(
            string testletId,
            List<Item> items,
            IShuffler shuffler)
        {
            TestletId = testletId ?? throw new ArgumentNullException(nameof(testletId));
            _items = items ?? throw new ArgumentNullException(nameof(items));
            _shuffler = shuffler ?? throw new ArgumentNullException(nameof(shuffler));

            ValidateItems(_items);
        }

        private void ValidateItems(List<Item> items)
        {
            uint totalItemsCount = PretestItemsCount + OperationalItemsCount;

            if (items.Count != totalItemsCount)
            {
                throw new ArgumentException(
                    $"Collection '{nameof(items)}' must contain " +
                    $"{totalItemsCount} elements.",
                    nameof(items));
            }

            if (items.Count(item => item.ItemType == ItemTypeEnum.Operational) != OperationalItemsCount)
            {
                throw new ArgumentException(
                    $"Collection '{nameof(items)}' must contain " +
                    $"{OperationalItemsCount} operational items and " +
                    $"{PretestItemsCount} pretest items.",
                    nameof(items));
            }
        }

        public List<Item> Randomize()
            => _shuffler.Shuffle(_items);
    }
}
