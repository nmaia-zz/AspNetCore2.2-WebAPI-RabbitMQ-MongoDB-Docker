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
            var number = 1234;

            number.Should().Be(123, "it's a number!");
        }
    }
}
