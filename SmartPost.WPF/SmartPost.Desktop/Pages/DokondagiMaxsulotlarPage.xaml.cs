using SmartPost.Desktop.Components;
using SmartPost.Desktop.Windows.Products;
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
            AddedProductComponent addedProduct = new AddedProductComponent();
            ProductComponent productComponent = new ProductComponent();

            productComponent.Product_Number.Content = count.ToString();
            addedProduct.product_Number.Content = count.ToString();

            stp_Added_Product.Children.Add(addedProduct);
            stp_Product.Children.Add(productComponent);

            count++;
        }

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {

    }

    private void add_product_btn_Click(object sender, RoutedEventArgs e)
    {
        AddProductStoreWindow addProductStoreWindow = new AddProductStoreWindow();
        addProductStoreWindow.ShowDialog();
    }
}
