using DddInPractice.UI.Common;
using DDDInPractice.Logic.Atms;
using DDDInPractice.UI.Common;

namespace DDDInPractice.UI.Atms
{
    public class AtmViewModel : BaseViewModel
    {
        private readonly Atm _atm;
        private string _message;
        private readonly AtmRepository _repository;
        private readonly PaymentGateway _paymentGateway;

        public AtmViewModel(Atm atm)
        {
            _atm = atm;
            _repository = new AtmRepository();
            _paymentGateway = new PaymentGateway();

            TakeMoneyCommand = new Command<decimal>(x => x > 0, TakeMoney);
        }

        public Command<decimal> TakeMoneyCommand { get; }

        public override string Caption => "ATM";

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public object MoneyInTransaction => _atm.MoneyCharged.ToString("C2");
        public object MoneyInside => _atm.MoneyInside;

        private void TakeMoney(decimal amount)
        {
            var error = _atm.CanTakeMoney(amount);
            if (error != string.Empty)
            {
                NotifyClient(error);
                return;
            }

            var amountWithCommission = _atm.CalculateAmountWithCommission(amount);
            _paymentGateway.ChargePayment(amountWithCommission);
            _atm.TakeMoney(amount);
            _repository.Save(_atm);

            NotifyClient("You have taken " + amount.ToString("C2"));
        }

        private void NotifyClient(string message)
        {
            Message = message;
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(MoneyInside));
        }
    }
}