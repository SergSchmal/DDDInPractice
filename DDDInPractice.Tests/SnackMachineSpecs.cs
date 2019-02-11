using System;
using DDDInPractice.Logic;
using FluentAssertions;
using Xunit;

using static DDDInPractice.Logic.Money;

namespace DDDInPractice.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void ReturnMoneyEmptiesMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void InsertedMoneyGoesToMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void CannotInsertMoreThenOneCoinOrNoteAtATime()
        {
            var snackMachine = new SnackMachine();
            var twoCent = Cent + Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MoneyInTransactionGoesToMoneyInsideAfterPurchase()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0.00m);
            snackMachine.MoneyInside.Amount.Should().Be(2);
        }

        [Fact]
        public void NewSnackMachineDoNotHasMoney()
        {
            var snackMachine = new SnackMachine();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
            snackMachine.MoneyInside.Amount.Should().Be(0m);
        }
    }
}