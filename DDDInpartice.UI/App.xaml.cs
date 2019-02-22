using System.Windows;
using DDDInPractice.Logic;

namespace DDDInPractice.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Initer.Init(@"Server=.\SQLEXPRESS;Database=DddInPractice;Trusted_Connection=true");
        }
    }
}
