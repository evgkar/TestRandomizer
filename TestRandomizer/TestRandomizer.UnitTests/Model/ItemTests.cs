using Shouldly;
using TestRandomizer.Model;
using Xunit;

namespace TestRandomizer.UnitTests.Model
{
    public class ItemTests
    {
        [Fact]
        public void GivenNullItemId_WhenConstructing_ThenThrows()
        {
            // Arrange
            string itemId = null;

            // Act && Assert
            var exception = Should.Throw<ArgumentNullException>(
                () => new Item(itemId, ItemType.Operational));

            exception.ParamName.ShouldBe("itemId");
        }
    }
}
