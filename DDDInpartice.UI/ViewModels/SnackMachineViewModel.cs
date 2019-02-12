using DddInPractice.Logic;
using DddInPractice.UI.Common;
using DDDInPractice.Logic;
using NHibernate;

namespace DDDInPractice.UI.ViewModels
{
    public class SnackMachineViewModel : BaseViewModel
    {
        private readonly SnackMachine _snackMachine;

        private string _message = "";

        public SnackMachineViewModel()
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                _snackMachine = session.Get<SnackMachine>(1L);
            }

            InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
            InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
            ReturnMoneyCommand = new Command(ReturnMoney);
            BuySnackCommand = new Command(BuySnack);
        }

        public override string Caption => "Snack Machine";

        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => _snackMachine.MoneyInside + _snackMachine.MoneyInTransaction;

        public string Message
        {
            get => _message;
            private set
            {
                _message = value;
                Notify();
            }
        }

        public Command InsertCentCommand { get; }
        public Command InsertTenCentCommand { get; }
        public Command InsertQuarterCommand { get; }
        public Command InsertDollarCommand { get; }
        public Command InsertFiveDollarCommand { get; }
        public Command InsertTwentyDollarCommand { get; }
        public Command ReturnMoneyCommand { get; }
        public Command BuySnackCommand { get; }

        private void BuySnack()
        {
            _snackMachine.BuySnack();
            using (ISession session = SessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(_snackMachine);
                transaction.Commit();
            }
            NotifyClient("You have bought a snack");
        }

        private void ReturnMoney()
        {
            _snackMachine.ReturnMoney();
            NotifyClient("Money was returned");
        }

        private void InsertMoney(Money coinOrNote)
        {
            _snackMachine.InsertMoney(coinOrNote);
            NotifyClient("You have inserted: " + coinOrNote);
        }

        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyInTransaction));
            Notify(nameof(MoneyInside));
        }
    }
}