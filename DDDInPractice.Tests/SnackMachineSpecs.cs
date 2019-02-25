using System;
using System.Linq;
using DDDInPractice.Logic;
using DDDInPractice.Logic.SnackMachines;
using FluentAssertions;
using Xunit;

using static DDDInPractice.Logic.SharedKernel.Money;
using static DDDInPractice.Logic.SnackMachines.Snack;

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

            snackMachine.MoneyInTransaction.Should().Be(0m);
        }

        [Fact]
        public void InsertedMoneyGoesToMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Should().Be(1.01m);
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
        public void BuySnackTradesInsertedMoneyForASnack()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Chocolate, 10, 1m));
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Should().Be(0.00m);
            snackMachine.MoneyInside.Amount.Should().Be(1m);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [Fact]
        public void NewSnackMachineDoNotHasMoney()
        {
            var snackMachine = new SnackMachine();

            snackMachine.MoneyInTransaction.Should().Be(0m);
            snackMachine.MoneyInside.Amount.Should().Be(0m);
        }

        [Fact]
        public void CannotMakePurchaseWhenThereIsNoSnacks()
        {
            var snackMachine = new SnackMachine();

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CannotMakePurchaseIfNotEnoughMoneyInserted()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Chocolate, 1, 2m));
            snackMachine.InsertMoney(Dollar);

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SnackMachineReturnsMoneyWithHighestDenominationFirst()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadMoney(Dollar);

            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.InsertMoney(Quarter);
            snackMachine.ReturnMoney();

            snackMachine.MoneyInside.QuarterCount.Should().Be(4);
            snackMachine.MoneyInside.OneDollarCount.Should().Be(0);
        }

        [Fact]
        public void AfterPurchaseChangeIsReturned()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Chocolate, 1, 0.5m));
            snackMachine.LoadMoney(TenCent * 10);

            snackMachine.InsertMoney(Dollar);
            snackMachine.BuySnack(1);

            snackMachine.MoneyInside.Amount.Should().Be(1.5m);
            snackMachine.MoneyInTransaction.Should().Be(0m);
        }

        [Fact]
        public void CannotBuySnackIfNotEnoughChange()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(Chocolate, 1, 0.5m));
            snackMachine.InsertMoney(Dollar);

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }
    }
}