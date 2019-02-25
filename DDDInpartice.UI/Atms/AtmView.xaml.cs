using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DDDInPractice.Logic.Atms;

namespace DDDInPractice.UI.Atms
{
    /// <summary>
    /// Interaction logic for AtmView.xaml
    /// </summary>
    public partial class AtmView : Window
    {
        public AtmView()
        {
            InitializeComponent();
            Atm atm = new AtmRepository().GetById(1);
            var viewModel = new AtmViewModel(atm);
            DataContext = viewModel;
        }
    }
}
