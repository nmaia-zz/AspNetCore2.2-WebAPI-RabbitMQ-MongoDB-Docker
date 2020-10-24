using System;
using Xunit;
using FluentAssertions;


namespace Demo.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var number = 123;

            number.Should().Be(123, "it's number!");
        }
    }
}
