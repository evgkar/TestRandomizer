using NSubstitute;
using Shouldly;
using TestRandomizer.UnitTests.TestData;
using Xunit;

namespace TestRandomizer.UnitTests
{
    public class ShufflerTests
    {
        private readonly IRandomizer _randomizer;
        private readonly IShuffler _shuffler;
        private readonly List<Item> _items;

        public ShufflerTests()
        {
            _randomizer = Substitute.For<IRandomizer>();
            _randomizer.GetRandomValue().Returns(1);
            _shuffler = new Shuffler(_randomizer);
            _items = ItemsData.GenerateItems();
        }

        [Fact]
        public void GivenNullRandomizer_WhenConstructing_ThenThrows()
        {
            // Arrange
            IRandomizer randomizer = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Shuffler(randomizer));

            exception.ParamName.ShouldBe("randomizer");
        }

        [Fact]
        public void GivenItems_WhenShuffleIsCalled_ThenShuffledListContainsSameNumberOfItems()
        {
            // Arrange
            Random random = new();
            _randomizer.GetRandomValue().Returns(_ => random.Next());

            // Act
            List<Item> result = _shuffler.Shuffle(_items);

            // Assert
            result.Count.ShouldBe(_items.Count);
        }

        [Fact]
        public void GivenItems_WhenShuffleIsCalled_ThenShuffledListContainsSameNumberOfPretestAndOperationalItems()
        {
            // Arrange
            Random random = new();
            _randomizer.GetRandomValue().Returns(_ => random.Next());

            // Act
            List<Item> result = _shuffler.Shuffle(_items);

            // Assert
            result.Count(i => i.ItemType == ItemTypeEnum.Pretest).ShouldBe(4);
            result.Count(i => i.ItemType == ItemTypeEnum.Operational).ShouldBe(6);
        }

        [Fact]
        public void GivenItems_WhenShuffleIsCalled_ThenShuffledListIsADifferentCollection()
        {
            // Arrange
            Random random = new();
            _randomizer.GetRandomValue().Returns(_ => random.Next());

            // Act
            List<Item> result = _shuffler.Shuffle(_items);

            // Assert
            result.ShouldNotBe(_items);
        }

        [Fact]
        public void GivenItems_WhenShuffleIsCalled_ThenPlaces2PretestItemsFirst()
        {
            // Act
            List<Item> result = _shuffler.Shuffle(_items);

            // Assert
            result[0].ShouldBe(_items[6]);
            result[1].ShouldBe(_items[7]);
            result[6].ShouldBe(_items[0]);
            result[7].ShouldBe(_items[1]);
        }
    }
}
