using Microsoft.Extensions.DependencyInjection;
using SmartPost.Desktop.Components;
using SmartPost.Desktop.Windows;
using SmartPost.Desktop.Windows.Brands;
using SmartPost.Desktop.Windows.Categories;
using SmartPost.Service.Interfaces.Brands;
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

    private void CreateButtonBrand_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        
        //BrandCreateWindow brandCreateWindow = new BrandCreateWindow();
        //brandCreateWindow.ShowDialog();
    }

    private void CreateButtonCategory_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
        categoryCreateWindow.ShowDialog();
    }
}
