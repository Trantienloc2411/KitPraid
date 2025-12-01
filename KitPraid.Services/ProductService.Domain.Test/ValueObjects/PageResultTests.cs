using FluentAssertions;
using ProductService.Domain.ValueObjects;

namespace ProductService.Domain.Test.ValueObjects
{
    public class PageResultTests
    {
        [Test]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var items = new List<string> { "A", "B" };
            var pageResult = new PageResult<string>(items, totalCount: 10, page: 2, size: 3);

            pageResult.Items.Should().BeEquivalentTo(items);
            pageResult.TotalCount.Should().Be(10);
            pageResult.Page.Should().Be(2);
            pageResult.Size.Should().Be(3);
        }

        [TestCase(1, 10, 3, true)]
        [TestCase(4, 10, 3, false)]
        public void HasNextPage_ShouldReturnCorrectValue(int page, int totalCount, int size, bool expected)
        {
            var items = new List<int>();
            var pageResult = new PageResult<int>(items, totalCount, page, size);

            pageResult.HasNextPage.Should().Be(expected);
        }

        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(5, true)]
        public void HasPreviousPage_ShouldReturnCorrectValue(int page, bool expected)
        {
            var items = new List<int>();
            var pageResult = new PageResult<int>(items, totalCount: 10, page: page, size: 3);

            pageResult.HasPreviousPage.Should().Be(expected);
        }

        [Test]
        public void TotalPages_ShouldBeCalculatedCorrectly_AndHasNextPageWorks()
        {
            var items = new List<int> { 1, 2, 3 };
            var totalCount = 10;
            var size = 3;
            var page = 4;

            var pageResult = new PageResult<int>(items, totalCount, page, size);

            // TotalPages = ceil(10 / 3) = 4
            pageResult.HasNextPage.Should().BeFalse();    // Page == TotalPages
            pageResult.HasPreviousPage.Should().BeTrue(); // Page > 1
        }

        [Test]
        public void Items_CanBeEmpty()
        {
            var pageResult = new PageResult<string>(new List<string>(), 0, 1, 5);

            pageResult.Items.Should().BeEmpty();
            pageResult.TotalCount.Should().Be(0);
            pageResult.HasNextPage.Should().BeFalse();
            pageResult.HasPreviousPage.Should().BeFalse();
        }
    }
}