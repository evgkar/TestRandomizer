using NSubstitute;
using Shouldly;
using Xunit;

namespace TestRandomizer.UnitTests
{
    public class TestletTests
    {
        private readonly string _testletId;
        private readonly List<Item> _items;
        private readonly IShuffler _shuffler;

        public TestletTests()
        {
            _testletId = "testlet";
            _items = GenerateItems();
            _shuffler = Substitute.For<IShuffler>();
        }

        [Fact]
        public void GivenNullTestletId_WhenConstructing_ThenThrows()
        {
            // Arrange
            string testletId = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Testlet(testletId, _items, _shuffler));

            exception.ParamName.ShouldBe("testletId");
        }

        [Fact]
        public void GivenNullItems_WhenConstructing_ThenThrows()
        {
            // Arrange
            List<Item> items = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Testlet(_testletId, items, _shuffler));

            exception.ParamName.ShouldBe("items");
        }

        [Fact]
        public void GivenNullShuffler_WhenConstructing_ThenThrows()
        {
            // Arrange
            IShuffler shuffler = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Testlet(_testletId, _items, shuffler));

            exception.ParamName.ShouldBe("shuffler");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(11)]
        [InlineData(100)]
        public void GivenCollectionOfItemsContainingNot10Elements_WhenConstructing_ThenThrows(
            int itemsCount)
        {
            // Arrange
            List<Item> items = GenerateItems(itemsCount);

            // Act && Assert
            var exception = Should.Throw<ArgumentException>(
                () => new Testlet(_testletId, items, _shuffler));

            exception.Message.ShouldBe(
                "Collection 'items' must contain 10 elements.");
            exception.ParamName.ShouldBe("items");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(10)]
        public void GivenCollectionOfItemsContainingNot6OperationalItems_WhenConstructing_ThenThrows(
            int operationalItemsCount)
        {
            // Arrange
            List<Item> items = GenerateItems(10, operationalItemsCount);

            // Act && Assert
            var exception = Should.Throw<ArgumentException>(
                () => new Testlet(_testletId, items, _shuffler));

            exception.Message.ShouldBe(
                "Collection 'items' must contain 6 operational items and 4 pretest items.");
            exception.ParamName.ShouldBe("items");
        }

        [Fact]
        public void GivenTestlet_WhenRandomizeIsCalled_ThenShufflesItems()
        {
            // Arrange
            ITestlet testlet = new Testlet(_testletId, _items, _shuffler);

            // Act
            testlet.Randomize();

            // Assert
            _shuffler.Received().Shuffle(_items);
        }

        [Fact]
        public void GivenTestlet_WhenRandomizeIsCalled_ThenReturnesShuffledItems()
        {
            // Arrange
            ITestlet testlet = new Testlet(_testletId, _items, _shuffler);
            List<Item> shuffledItems = GenerateItems(50, 30);
            _shuffler.Shuffle(_items).Returns(shuffledItems);

            // Act
            List<Item> result = testlet.Randomize();

            // Assert
            result.ShouldBe(shuffledItems);
        }

        private List<Item> GenerateItems(int count = 10, int operationalItemsCount = 6)
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
