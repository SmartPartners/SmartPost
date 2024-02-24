using SmartPost.Desktop.Components;
using SmartPost.Desktop.Windows;
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
        for (int i = 0; i < 50; i++)
        {
            StorageComponent storage = new StorageComponent();
            wrpOmborxona.Children.Add(storage);

            CategoryComponent category = new CategoryComponent();
            wrpCategory.Children.Add(category);

            BrandComponent brand = new BrandComponent();
            wrpBrand.Children.Add(brand);
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {

    }

    private void CreateButtonProducts_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        AddProductWindow addProductWindow = new AddProductWindow();
        addProductWindow.ShowDialog();
    }
}
