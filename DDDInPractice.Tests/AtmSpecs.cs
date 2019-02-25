using DDDInPractice.Logic.Atms;
using FluentAssertions;
using Xunit;
using static DDDInPractice.Logic.SharedKernel.Money;

namespace DDDInPractice.Tests
{
    public class AtmSpecs
    {
        [Fact]
        public void TakeMoneyExchangesMoneyWIthCommission()
        {
            var atm = new Atm();
            atm.LoadMoney(Dollar);

            atm.TakeMoney(1m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void CommissionIsAtLeastOneCent()
        {
            var atm = new Atm();
            atm.LoadMoney(Cent);

            atm.TakeMoney(0.01m);

            atm.MoneyCharged.Should().Be(0.02m);
        }

        [Fact]
        public void CommissionIsRoundedUpToNextCent()
        {
            var atm = new Atm();
            atm.LoadMoney(Dollar + TenCent);

            atm.TakeMoney(1.1m);

            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}