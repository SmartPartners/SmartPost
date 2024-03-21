using SmartPost.Desktop.Pages;
using System.Windows;
using System.Windows.Input;

namespace SmartPost.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IServiceProvider services;
    public MainWindow(IServiceProvider services)
    {
        InitializeComponent();
        this.services = services;
    }

    private void btnMinus_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void btnMaximize_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
            this.WindowState = WindowState.Normal;
        else this.WindowState = WindowState.Maximized;
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    private void MouseDragable_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void rbOmbor_Click(object sender, RoutedEventArgs e)
    {
        OmborxonaPage omborxonaPage = new OmborxonaPage();
        PageNavigator.Content = omborxonaPage;
    }

    private void rbDokondagi_maxsulotlar_Click(object sender, RoutedEventArgs e)
    {
        DokondagiMaxsulotlarPage dokondagiMaxsulotlarPage = new DokondagiMaxsulotlarPage();
        PageNavigator.Content = dokondagiMaxsulotlarPage;
    }

    private void rbSotuv_tarixi_Click(object sender, RoutedEventArgs e)
    {
        SotuvTarixiPage sotuvTarixiPage = new SotuvTarixiPage();
        PageNavigator.Content = sotuvTarixiPage;
    }

    private void rbSotuv_tafsilotlari_Click(object sender, RoutedEventArgs e)
    {
        SotuvTafsilotlariPage sotuvTafsilotlariPage = new SotuvTafsilotlariPage();
        PageNavigator.Content = sotuvTafsilotlariPage;
    }

    private void rbSozlamalar_Click(object sender, RoutedEventArgs e)
    {
        SozlamalarPage sozlamalarPage = new SozlamalarPage();
        PageNavigator.Content= sozlamalarPage;  
    }

    private void rbDashboard_Click(object sender, RoutedEventArgs e)
    {
        DashboardPage dashboardPage = new DashboardPage();
        PageNavigator.Content = dashboardPage;
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("Log out");
    }
}