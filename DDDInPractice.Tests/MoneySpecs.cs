using System;
using DDDInPractice.Logic;
using DDDInPractice.Logic.SharedKernel;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;

namespace DDDInPractice.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void SumOfTwoMoneysProducesCorrectResult()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void TwoMoneyInstancesDoNotEqualIfContainDifferentMoneyAmounts()
        {
            var dollar = Money.Dollar;
            var hundredCents = new Money(100, 0, 0, 0, 0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Fact]
        public void TwoMoneyInstancesEqualIfContainTheSameMoneyAmount()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void CannotCreateMoneyWithNegativeValue(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            Action action = () => new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void AmountIsCalculatedCorrectly(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount, decimal expectedAmount)
        {
            Money money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void SubtractionOfTwoMoneysProducesCorrectResult()
        {
            var money1 = new Money(10, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            Money result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void CannotSubtractMoreThenExists()
        {
            var money1 = Money.TenCent;
            var money2 = Money.Cent;

            Action action = () =>
            {
                Money money = money2 - money1;
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(1, 0, 0, 0, 0, 0, "¢1")]
        [InlineData(0, 0, 0, 1, 0, 0, "$1,00")]
        [InlineData(1, 0, 0, 1, 0, 0, "$1,01")]
        [InlineData(0, 0, 2, 1, 0, 0, "$1,50")]
        public void ToStringShouldBeReturnAmount(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount, string amount)
        {
            var money = new Money(oneCentCount, tenCentCount, quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            money.ToString().Should().Be(amount);
        }
    }
}