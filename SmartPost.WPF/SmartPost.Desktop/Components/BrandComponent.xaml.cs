using SmartPost.Desktop.Windows.Brands;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for BrandComponent.xaml
/// </summary>
public partial class BrandComponent : UserControl
{
    public BrandComponent()
    {
        InitializeComponent();
    }

    private void store_Border_MouseEnter(object sender, MouseEventArgs e)
    {
        brand_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void store_Border_MouseLeave(object sender, MouseEventArgs e)
    {
        brand_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("transparent"));
    }

    private void update_btn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        BrandUpdateWindow updateWindow = new BrandUpdateWindow();
        updateWindow.ShowDialog();
    }

    private void delete_btn_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
