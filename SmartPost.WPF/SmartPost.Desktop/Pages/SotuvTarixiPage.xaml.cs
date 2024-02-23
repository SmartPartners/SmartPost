using SmartPost.Desktop.Components;
using System.Windows;
using System.Windows.Controls;

namespace SmartPost.Desktop.Pages;

/// <summary>
/// Interaction logic for SotuvTarixiPage.xaml
/// </summary>
public partial class SotuvTarixiPage : Page
{
    public SotuvTarixiPage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 15; i++)
        {
            AddedProductComponent addedProductComponent = new AddedProductComponent();
            wrp_Sale_History.Children.Add(addedProductComponent);
        }
    }

    private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
