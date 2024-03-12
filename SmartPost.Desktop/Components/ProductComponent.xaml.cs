using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPost.Desktop.Components;

/// <summary>
/// Interaction logic for ProductComponent.xaml
/// </summary>
public partial class ProductComponent : UserControl
{
    public ProductComponent()
    {
        InitializeComponent();
    }

    private void Border_MouseEnter(object sender, MouseEventArgs e)
    {
        Product_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#329DFF"));
    }

    private void Border_MouseLeave(object sender, MouseEventArgs e)
    {
        Product_Border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
    }
}
