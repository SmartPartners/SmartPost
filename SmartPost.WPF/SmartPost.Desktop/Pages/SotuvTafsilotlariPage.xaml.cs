using SmartPost.Desktop.Components;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPost.Desktop.Pages;

/// <summary>
/// Interaction logic for SotuvTafsilotlariPage.xaml
/// </summary>
public partial class SotuvTafsilotlariPage : Page
{
    public SotuvTafsilotlariPage()
    {
        InitializeComponent();
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        for (int i = 0; i < 15; i++)
        {
            AddedProductComponent addedProductComponent = new AddedProductComponent();
            wrpTopSale.Children.Add(addedProductComponent);
        }
    }
}
