using TestRandomizer.Model;

namespace TestRandomizer.UnitTests.TestData
{
    internal class ItemEqualityComparer : IEqualityComparer<Item>
    {
        public static IEqualityComparer<Item> Comparer { get; }
            = new ItemEqualityComparer();

        public bool Equals(Item x, Item y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.ItemId == y.ItemId && x.ItemType == y.ItemType;
        }

        public int GetHashCode(Item obj)
        {
            return HashCode.Combine(obj.ItemId, (int)obj.ItemType);
        }
    }
}
