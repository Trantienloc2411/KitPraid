using FluentAssertions;
using ProductService.Domain.ValueObjects;

namespace ProductService.Domain.Test.ValueObjects
{
    public class PagRequestTests
    {
        [Test]
        public void Constructor_ShouldSetProperties_WhenValid()
        {
            var request = new PageRequest(2, 10);

            request.Page.Should().Be(2);
            request.Size.Should().Be(10);
        }

        [Test]
        public void Skip_ShouldReturnCorrectNumber()
        {
            var request = new PageRequest(3, 20);

            request.Skip.Should().Be((3 - 1) * 20); // 40
        }

        [Test]
        public void Constructor_ShouldThrow_WhenPageLessThan1()
        {
            Action act = () => new PageRequest(0, 10);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("*page*");
        }

        [Test]
        public void Constructor_ShouldThrow_WhenSizeLessThan1()
        {
            Action act = () => new PageRequest(1, 0);

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("*size*");
        }
    }
}