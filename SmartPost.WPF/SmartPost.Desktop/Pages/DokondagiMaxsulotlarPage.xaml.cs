using SmartPost.Desktop.Components;
using System.Windows;
using System.Windows.Controls;

namespace SmartPost.Desktop.Pages;

/// <summary>
/// Interaction logic for DokondagiMaxsulotlarPage.xaml
/// </summary>
public partial class DokondagiMaxsulotlarPage : Page
{
    public DokondagiMaxsulotlarPage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        int count = 1; 

        for (int i = 0; i < 50; i++)
        {
            ProductComponent productComponent = new ProductComponent();
            stp_Product.Children.Add(productComponent);
            productComponent.Product_Number.Content = count.ToString();
            count++;
        }

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {

    }
}
