using SmartPost.WPF.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartPost.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void rbDashboard_Checked(object sender, RoutedEventArgs e)
        {
            Dashbaord dashboard = new Dashbaord();
            PageNavigator.Navigate (dashboard);


        }

        private void rbSklad_Checked(object sender, RoutedEventArgs e)
        {
            Sklad sklad = new Sklad();
            PageNavigator.Navigate (sklad);
        }

        private void rbDokonda_Checked(object sender, RoutedEventArgs e)
        {
            Dokonda dokonda = new Dokonda();
            PageNavigator.Navigate (dokonda);
        }

        private void rbHistory_Checked(object sender, RoutedEventArgs e)
        {
            History history = new History();
            PageNavigator.Navigate (history);
        }

        private void rbSettings_Checked(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            PageNavigator.Navigate (settings);
        }

        private void rbSignOut_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown ();
        }

        private void rbDokonda_Click(object sender, RoutedEventArgs e)
        {
            rbHistory.Visibility = Visibility.Hidden;

        }
    }
}