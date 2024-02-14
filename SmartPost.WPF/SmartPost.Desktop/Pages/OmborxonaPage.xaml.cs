using SmartPost.Desktop.Components;
using System.Windows.Controls;

namespace SmartPost.Desktop.Pages;

/// <summary>
/// Interaction logic for OmborxonaPage.xaml
/// </summary>
public partial class OmborxonaPage : Page
{
    public OmborxonaPage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        for (int i = 0; i < 10; i++)
        {
            StorageComponent storage = new StorageComponent();
            wrpOmborxona.Children.Add(storage);
        }
    }
}
