using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for AddedProductComponent.xaml
/// </summary>
public partial class AddedProductComponent : UserControl
{
    public AddedProductComponent()
    {
        InitializeComponent();
    }

    private void product_border_MouseEnter(object sender, MouseEventArgs e)
    {
        product_border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void product_border_MouseLeave(object sender, MouseEventArgs e)
    {
        product_border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
    }
}
