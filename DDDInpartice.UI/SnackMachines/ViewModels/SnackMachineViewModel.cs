using System.Collections.Generic;
using System.Linq;
using DddInPractice.UI.Common;
using DDDInPractice.Logic.SharedKernel;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.UI.Common;

namespace DDDInPractice.UI.SnackMachines.ViewModels
{
    public class SnackMachineViewModel : BaseViewModel
    {
        private readonly SnackMachine _snackMachine;
        private readonly SnackMachineRepository _repository;

        private string _message = "";

        public SnackMachineViewModel(SnackMachine snackMachine)
        {
            _snackMachine = snackMachine;
            _repository = new SnackMachineRepository();


            InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
            InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
            ReturnMoneyCommand = new Command(ReturnMoney);
            BuySnackCommand = new Command<string>(BuySnack);
        }

        public IReadOnlyList<SnackPileViewModel> Piles
        {
            get { return _snackMachine.GetAllSnackPiles().Select(x => new SnackPileViewModel(x)).ToList(); } 
        }

        public override string Caption => "Snack Machine";

        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();

        public Money MoneyInside => _snackMachine.MoneyInside;

        public string Message
        {
            get => _message;
            private set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public Command InsertCentCommand { get; }
        public Command InsertTenCentCommand { get; }
        public Command InsertQuarterCommand { get; }
        public Command InsertDollarCommand { get; }
        public Command InsertFiveDollarCommand { get; }
        public Command InsertTwentyDollarCommand { get; }
        public Command ReturnMoneyCommand { get; }
        public Command<string> BuySnackCommand { get; }

        private void BuySnack(string positionString)
        {
            int position = int.Parse(positionString);

            string error = _snackMachine.CanBuySnack(position);
            if (error != string.Empty)
            {
                NotifyClient(error);
                return;
            }
            
            _snackMachine.BuySnack(position);
            _repository.Save(_snackMachine);
            NotifyClient("You have bought a snack");
        }

        private void ReturnMoney()
        {
            if (_snackMachine.MoneyInTransaction == 0)
            {
                NotifyClient("You have no money inserted");
                return;
            }
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
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(MoneyInside));
            OnPropertyChanged(nameof(Piles));
        }
    }
}