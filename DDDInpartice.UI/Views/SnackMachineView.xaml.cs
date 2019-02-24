using System.Windows;
using DDDInPractice.Logic;
using DDDInPractice.UI.ViewModels;

namespace DDDInPractice.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SnackMachineView : Window
    {
        public SnackMachineView()
        {
            InitializeComponent();
            var snackMachine = new SnackMachineRepository().GetById(1);
            var viewModel = new SnackMachineViewModel(snackMachine);
            DataContext = viewModel;
        }
    }
}
