using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace ef_core_5_tests
{
    public class AutoMockAttribute : AutoDataAttribute
    {
        public AutoMockAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }

    public class UnitTest1
    {
        [Theory, AutoMock]
        public void Test1()
        {
            true.Should().Be(true);
        }
    }
}