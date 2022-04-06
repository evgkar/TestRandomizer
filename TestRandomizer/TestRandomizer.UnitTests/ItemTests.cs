using Shouldly;
using Xunit;

namespace TestRandomizer.UnitTests
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
                () => new Item(itemId, ItemTypeEnum.Operational));

            exception.ParamName.ShouldBe("itemId");
        }
    }
}
