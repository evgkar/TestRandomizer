using NSubstitute;
using Shouldly;
using TestRandomizer.Model;
using TestRandomizer.UnitTests.TestData;
using Xunit;

namespace TestRandomizer.UnitTests
{
    public class TestletTests
    {
        private readonly string _testletId;
        private readonly List<Item> _items;
        private readonly IRandomizer _randomizer;

        public TestletTests()
        {
            _testletId = "testlet";
            _items = ItemsData.GenerateItems();
            _randomizer = Substitute.For<IRandomizer>();
        }

        [Fact]
        public void GivenNullTestletId_WhenConstructing_ThenThrows()
        {
            // Arrange
            string testletId = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Testlet(testletId, _items, _randomizer));

            exception.ParamName.ShouldBe("testletId");
        }

        [Fact]
        public void GivenNullItems_WhenConstructing_ThenThrows()
        {
            // Arrange
            List<Item> items = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Testlet(_testletId, items, _randomizer));

            exception.ParamName.ShouldBe("items");
        }

        [Fact]
        public void GivenNullRandomizer_WhenConstructing_ThenThrows()
        {
            // Arrange
            IRandomizer randomizer = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Testlet(_testletId, _items, randomizer));

            exception.ParamName.ShouldBe("randomizer");
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
            List<Item> items = ItemsData.GenerateItems(itemsCount);

            // Act && Assert
            var exception = Should.Throw<ArgumentException>(
                () => new Testlet(_testletId, items, _randomizer));

            exception.Message.ShouldStartWith(
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
            List<Item> items = ItemsData.GenerateItems(10, operationalItemsCount);

            // Act && Assert
            var exception = Should.Throw<ArgumentException>(
                () => new Testlet(_testletId, items, _randomizer));

            exception.Message.ShouldStartWith(
                "Collection 'items' must contain 6 operational items and 4 pretest items.");
            exception.ParamName.ShouldBe("items");
        }

        [Fact]
        public void
            GivenTestlet_WhenRandomizeIsCalled_ThenResultContainsSameNumberOfPretestAndOperationalItems()
        {
            // Arrange
            Random random = new();
            _randomizer.GetRandomValue().Returns(_ => random.Next());
            Testlet testlet = new(_testletId, _items, _randomizer);

            // Act
            List<Item> result = testlet.Randomize();

            // Assert
            result
                .Count(item => item.ItemType == ItemType.Pretest)
                .ShouldBe(_items.Count(item => item.ItemType == ItemType.Pretest));
            result
                .Count(item => item.ItemType == ItemType.Operational)
                .ShouldBe(_items.Count(item => item.ItemType == ItemType.Operational));
        }

        [Fact]
        public void GivenTestlet_WhenRandomizeIsCalled_ThenFirstTwoItemsArePretest()
        {
            // Arrange
            Random random = new();
            _randomizer.GetRandomValue().Returns(_ => random.Next());
            Testlet testlet = new(_testletId, _items, _randomizer);

            // Act
            List<Item> result = testlet.Randomize();

            // Assert
            result[0].ItemType.ShouldBe(ItemType.Pretest);
            result[1].ItemType.ShouldBe(ItemType.Pretest);
        }

        [Theory]
        [MemberData(nameof(GetSourceItemsAndRandomizedItems))]
        public void GivenTestlet_WhenRandomizeIsCalled_ThenReturnsRandomizedListOfItems(
            List<Item> items,
            int[] randomNumbers,
            List<Item> expectedResult)
        {
            // Arrange
            _randomizer.GetRandomValue().Returns(randomNumbers[0], randomNumbers[1..]);
            Testlet testlet = new(_testletId, items, _randomizer);

            // Act
            List<Item> actualResult = testlet.Randomize();

            // Assert
            actualResult.ShouldBe(expectedResult, ItemEqualityComparer.Comparer);
        }

        public static IEnumerable<object[]> GetSourceItemsAndRandomizedItems()
        {
            yield return new[]
            {
                ItemsData.GenerateItems(),
                (object) Enumerable.Range(0, 20).ToArray(),
                new List<Item>
                {
                    new ("7", ItemType.Pretest),
                    new ("8", ItemType.Pretest),
                    new ("1", ItemType.Operational),
                    new ("2", ItemType.Operational),
                    new ("3", ItemType.Operational),
                    new ("4", ItemType.Operational),
                    new ("5", ItemType.Operational),
                    new ("6", ItemType.Operational),
                    new ("9", ItemType.Pretest),
                    new ("10", ItemType.Pretest)
                }
            };

            yield return new[]
            {
                ItemsData.GenerateItems(),
                (object) Enumerable.Range(0, 20).Reverse().ToArray(),
                new List<Item>
                {
                    new ("10", ItemType.Pretest),
                    new ("9", ItemType.Pretest),
                    new ("8", ItemType.Pretest),
                    new ("7", ItemType.Pretest),
                    new ("6", ItemType.Operational),
                    new ("5", ItemType.Operational),
                    new ("4", ItemType.Operational),
                    new ("3", ItemType.Operational),
                    new ("2", ItemType.Operational),
                    new ("1", ItemType.Operational)
                }
            };

            yield return new[]
            {
                ItemsData.GenerateItems(),
                (object) new[] { 54, 85, 75, 55, 72, 51, 85, 68, 93, 19, 73, 29 },
                new List<Item>
                {
                    new ("7", ItemType.Pretest),
                    new ("10", ItemType.Pretest),
                    new ("6", ItemType.Operational),
                    new ("9", ItemType.Pretest),
                    new ("2", ItemType.Operational),
                    new ("4", ItemType.Operational),
                    new ("1", ItemType.Operational),
                    new ("8", ItemType.Pretest),
                    new ("3", ItemType.Operational),
                    new ("5", ItemType.Operational)
                }
            };

            yield return new[]
            {
                ItemsData.GenerateItems(),
                (object) new[] { 34, 37, 84, 98, 44, 53, 90, 33, 85, 28, 57, 88 },
                new List<Item>
                {
                    new ("7", ItemType.Pretest),
                    new ("8", ItemType.Pretest),
                    new ("6", ItemType.Operational),
                    new ("4", ItemType.Operational),
                    new ("1", ItemType.Operational),
                    new ("2", ItemType.Operational),
                    new ("9", ItemType.Pretest),
                    new ("5", ItemType.Operational),
                    new ("10", ItemType.Pretest),
                    new ("3", ItemType.Operational)
                }
            };
        }
    }
}
