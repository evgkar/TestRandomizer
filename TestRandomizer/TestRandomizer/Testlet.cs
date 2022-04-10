using TestRandomizer.Model;

namespace TestRandomizer
{
    /// <summary>
    /// Represents a set of test items of 2 categories: Operational and Pretest
    /// Creates a randomly shuffled set of test items based on the initial set.
    /// </summary>
    public class Testlet
    {
        private const int LeadingPretestItemsCount = 2;
        private const int PretestItemsCount = 4;
        private const int OperationalItemsCount = 6;

        private readonly IRandomizer _randomizer;
        private readonly List<Item> _items;

        /// <summary>
        /// An identifier of the Testlet.
        /// </summary>
        public string TestletId { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="testletId">An identifier of Testlet</param>
        /// <param name="items">A list of <see cref="Item"/></param>
        public Testlet(string testletId, List<Item> items)
            : this(testletId, items, new Randomizer())
        {
        }

        internal Testlet(
            string testletId,
            List<Item> items,
            IRandomizer randomizer)
        {
            TestletId = testletId ?? throw new ArgumentNullException(nameof(testletId));
            _items = items ?? throw new ArgumentNullException(nameof(items));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            ValidateItems(_items);
        }

        /// <summary>
        /// Randomizes list of Items in the following way:
        /// Shuffles a list of items, places 2 Pretest items at the first positions,
        /// then shuffles rest items again.
        /// </summary>
        /// <returns>List of shuffled items.</returns>
        public List<Item> Randomize()
        {
            IEnumerable<Item> leadingPretestItems = _items
                .Where(item => item.ItemType == ItemType.Pretest)
                .OrderBy(_ => _randomizer.GetRandomValue())
                .Take(LeadingPretestItemsCount)
                .ToList();

            IEnumerable<Item> remainingItems = _items
                .Except(leadingPretestItems)
                .OrderBy(_ => _randomizer.GetRandomValue());

            return leadingPretestItems.Concat(remainingItems).ToList();
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

            if (items.Count(item => item.ItemType == ItemType.Operational) != OperationalItemsCount)
            {
                throw new ArgumentException(
                    $"Collection '{nameof(items)}' must contain " +
                    $"{OperationalItemsCount} operational items and " +
                    $"{PretestItemsCount} pretest items.",
                    nameof(items));
            }
        }
    }
}
