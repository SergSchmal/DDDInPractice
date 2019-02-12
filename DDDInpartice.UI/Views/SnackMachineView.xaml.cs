using System.Windows;
using DDDInPractice.UI.ViewModels;

namespace DDDInPractice.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SnackMachineView : Window
    {
        private readonly SnackMachineViewModel _viewModel = new SnackMachineViewModel();

        public SnackMachineView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
