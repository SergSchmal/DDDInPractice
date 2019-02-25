using System;
using DDDInPractice.Logic;
using DDDInPractice.Logic.SnackMachines;
using FluentAssertions;
using Xunit;

namespace DDDInPractice.Tests
{
    public class SnackPileTests
    {
        [Fact]
        public void QuantityLessAsZeroThrowException()
        {
            Action action = () => new SnackPile(null, -1, 0m);

            action.Should().Throw<InvalidOperationException>();
        }
        [Fact]

        public void PriceLessAsZeroThrowException()
        {
            Action action = () => new SnackPile(null, 1, -1m);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void PriceLessAsOneCentThrowException()
        {
            Action action = () => new SnackPile(null, 1, 0.005m);

            action.Should().Throw<InvalidOperationException>();
        }
    }
}